using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayersController : MonoBehaviourPunCallbacks
{
    public List<Player> players = new List<Player>();

    /* Переменные первого игрока */
    public UILabel firstPlayerNickName;
    public UILabel firstPlayerScoreLabel;

    /* Переменные второго игрока */
    public UILabel secondPlayerNickName;
    public UILabel secondPlayerScoreLabel;

    private void Start()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Player newPlayer = new Player(player.NickName);
            AddPlayer(newPlayer);
        }

        if (PhotonNetwork.IsMasterClient == true)
        {
            SetPlayerData(1);
        }
        else
        {
            SetPlayerData(1);
            SetPlayerData(2);
        }
    }

    private void AddPlayer(Player newPlayer)
    {
        players.Add(newPlayer);
    }

    private void SetPlayerData(int playerNumber)
    {
        if (playerNumber == 1)
        {
            firstPlayerNickName.text = players[0].playerName;
            firstPlayerScoreLabel.text = players[0].playerScore.ToString();
        }
        else if (playerNumber == 2)
        {
            secondPlayerNickName.text = players[1].playerName;
            secondPlayerScoreLabel.text = players[1].playerScore.ToString();
        }
        else
           Debug.LogError("Ошибка в передаче данных игроков. Найден лишний игрок");
    }

    /* Вызывается только у мастера, как добавление второго игрока в список */
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Player secondPlayer = new Player(PhotonNetwork.PlayerList[1].NickName);
        AddPlayer(secondPlayer);
        SetPlayerData(2);
    }
}
