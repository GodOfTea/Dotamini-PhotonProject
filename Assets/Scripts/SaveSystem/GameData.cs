using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int exp;


    public GameData (LevelSystem levelSystem)
    {
        level = levelSystem.GetLevelValue();
        exp = levelSystem.GetExpValue();
    }
}
