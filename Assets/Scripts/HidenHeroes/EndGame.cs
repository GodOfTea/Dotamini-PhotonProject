using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private EndGamePanel endGamePanel;
    [SerializeField] private PlayersController playersController;

    public bool isGameEnd;


    private void Start()
    {
        isGameEnd = false;
    }

    private void DefineWinner(List<Player> players)
    {
        if (players[0].playerScore > players[1].playerScore)
        {
            players[0].isPlayerWin = true;
            players[1].isPlayerWin = false;
        }
        else if (players[0].playerScore < players[1].playerScore)
        {
            players[0].isPlayerWin = false;
            players[1].isPlayerWin = true;
        }
        else
        {
            players[0].isPlayerWin = false;
            players[1].isPlayerWin = false;
        }
    }

    public void StartEndGameProcess()
    {
        var players = playersController.players;

        DefineWinner(players);

        isGameEnd = true;
        var resultIndex = 0;

        if (players[0].isPlayerWin == true)
        {
            resultIndex = PhotonNetwork.IsMasterClient ?
                1 : 3;
        }
        else if (players[1].isPlayerWin == true)
        {
            resultIndex = PhotonNetwork.IsMasterClient ?
                3 : 1;
        }
        else
        {
            resultIndex = 2;
        }

        endGamePanel.SetResult(resultIndex);
    }
}
