using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UILabel inputLabel;
    [SerializeField] private UILabel lobbyNameInput;

    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private GameObject connectionButton;


    private void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    /*UI*/
    public void SetNickNameOrDefault()
    {
        if (inputLabel.text != "" && inputLabel.text != "Enter nickname")
            PhotonNetwork.NickName = inputLabel.text;
        else
            PhotonNetwork.NickName = "Player" + Random.Range(1, 100);
    }

    /* callbacks */
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined or Create Room");
        PhotonNetwork.LoadLevel("HidenHeroesGame");
    }

    /* кнопки */
    public void CreateRoom()
    {
        SetNickNameOrDefault();

        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = 2});
    }

    public void JoinRandomRoom()
    {
        SetNickNameOrDefault();

        if (PhotonNetwork.CountOfRooms == 0)
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
        else
            PhotonNetwork.JoinRandomRoom();
    }

    public void StartLobby()
    {
        lobbyPanel.SetActive(true);
        connectionButton.SetActive(false);
    }

    public void CheckLobbyName()
    {
        var lobbyName = lobbyNameInput.text;

        if (lobbyName != "" && lobbyName != "Lobby name is ...")
        {
            connectionButton.SetActive(true);
        }
    }

    public void ConnectionToLobby()
    {
        var lobbyName = lobbyNameInput.text;

        SetNickNameOrDefault();
        lobbyPanel.SetActive(false);
        TypedLobby typedLobby = new TypedLobby(lobbyName, LobbyType.Default);
        PhotonNetwork.JoinOrCreateRoom(lobbyName, new RoomOptions { MaxPlayers = 2 }, typedLobby);
        
    }
}
