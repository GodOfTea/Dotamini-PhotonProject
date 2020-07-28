using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public event EventHandler OnExpChanged;
    public event EventHandler OnLvlChanged;

    public static LevelSystem Instance { get; private set; }

    private int level;
    private int exp;
    private int expForNextLevel = 100;


    private void Awake()
    {
        CheckInstance();
        InitLevelSystem();
    }

    private void CheckInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    private void InitLevelSystem()
    {
        var data = Load.LoadLvlData();
        if (data != null)
        {
            level = data.level;
            exp = data.exp;
        }
        else
        {
            level = 1;
            exp = 0;
        }
    }


    public void AddExp(int value)
    {
        exp += value;

        if (exp >= expForNextLevel)
        {
            level++;
            exp -= expForNextLevel;
            OnLvlChanged?.Invoke(this, EventArgs.Empty);
        }
        OnExpChanged?.Invoke(this, EventArgs.Empty);

        Save.SaveLvlData(this);
    }

    public int GetLevelValue()
    {
        return level;
    }

    public int GetExpValue()
    {
        return exp;
    }

    public int GetExpForNextLevelValue()
    {
        return expForNextLevel;
    }
}
