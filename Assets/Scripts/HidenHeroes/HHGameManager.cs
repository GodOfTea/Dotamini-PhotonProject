using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Text;
using Boo.Lang;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HHGameManager : MonoBehaviourPunCallbacks
{
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    /* текущий игрок */
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogFormat("Player {0} enter", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left", otherPlayer.NickName);
    }

    /* Для общей практики. Не используется */
    #region Сериализация компомнента Player (не используется)
    public static object DeserializePlayer(byte[] data)
    {
        char[] nickName = new char[data.Length - 5];

        int lastIndex = 0;
        for (int i = 0; i < data.Length - 5; i++)
        {
            nickName[i] = BitConverter.ToChar(data, i);
            lastIndex = i;
        }

        Player result = new Player(nickName.ToString());
        result.playerScore = BitConverter.ToInt32(data, ++lastIndex);
        result.isPlayerWin = BitConverter.ToBoolean(data, data.Length - 1);

        return result;
    }

    public static byte[] SerializePlayer(object obj)
    {
        Player player = (Player)obj;

        char[] nickName = player.playerName.ToCharArray();
        int size = nickName.Length + 4 + 1; /* 4 - int, 1 - bool */

        byte[] result = new byte[size];

        int i = 0;
        foreach (char c in nickName)
            BitConverter.GetBytes(c).CopyTo(result, i++);

        BitConverter.GetBytes(player.playerScore).CopyTo(result, i);
        BitConverter.GetBytes(player.isPlayerWin).CopyTo(result, ++i);

        return result;
    }
    #endregion
}
