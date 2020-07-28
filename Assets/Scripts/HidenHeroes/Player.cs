using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public string playerName;

    public int playerScore;

    public bool isPlayerWin;

    public Player(string nickName)
    {
        playerName = nickName;
        isPlayerWin = false;
        playerScore = 0;
    }
}
