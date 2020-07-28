using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGamePanel : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] private GameObject startGamePanel;

    public UnityEvent startGame;

    private void Start()
    {
        startGamePanel.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && 
            PhotonNetwork.IsMasterClient == true)
        {
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };

            PhotonNetwork.RaiseEvent(1, null, options, sendOptions); /* отключение startGamePanel у всех игроков */
            startGame.Invoke(); /* старт игры */
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        switch (photonEvent.Code)
        {
            case 1:
                startGamePanel.SetActive(false);
                break;
        }
    }
}
