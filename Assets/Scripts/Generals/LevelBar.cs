using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
    [SerializeField] private LevelSystem levelSystem;

    [SerializeField] private UI2DSprite bar;
    [SerializeField] private UILabel expNumber;
    [SerializeField] private UILabel lvlNumber;

    private int level;
    private int exp;
    private int expForNextLevel;

    private bool isAnimated;


    private void Start()
    {
        levelSystem = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>();

        level = levelSystem.GetLevelValue();
        exp = levelSystem.GetExpValue();
        expForNextLevel = levelSystem.GetExpForNextLevelValue();
        bar.fillAmount = (float)exp / expForNextLevel;

        expNumber.text = string.Format("{0}/{1}", exp, expForNextLevel);
        lvlNumber.text = level.ToString();

        levelSystem.OnExpChanged += LevelSystem_OnExpChanged;
        levelSystem.OnLvlChanged += LevelSystem_OnLvlChanged;
    }

    private void Update()
    {
        if (isAnimated == true)
        {
            if (level < levelSystem.GetLevelValue())
                ChangeValues();
            else
            {
                if (exp < levelSystem.GetExpValue())
                    ChangeValues();
                else
                    isAnimated = false;
            }
        }
    }

    public void ChangeValues()
    {
        exp++;

        if (exp >= expForNextLevel)
        {
            level++;
            exp -= expForNextLevel;
        }

        expNumber.text = string.Format("{0}/{1}", exp, expForNextLevel);
        bar.fillAmount = (float)exp / expForNextLevel;
    }

    private void LevelSystem_OnLvlChanged(object sender, System.EventArgs e)
    {
        var lvl = levelSystem.GetLevelValue().ToString();
        isAnimated = true;
        lvlNumber.text = lvl;
    }

    private void LevelSystem_OnExpChanged(object sender, System.EventArgs e)
    {
        isAnimated = true;
    }
}
