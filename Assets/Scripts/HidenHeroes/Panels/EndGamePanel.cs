using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private DailyBonus dailyBonus;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private UILabel gameResult;
    [SerializeField] private LevelSystem levelSystem;

    private Dictionary<int, string> endGameResult = new Dictionary<int, string>
    {
        {1, "Win"  },
        {2, "Draw" },
        {3, "Lose" }
    };


    private void Start()
    {
        levelSystem = GameObject.FindGameObjectWithTag("LevelSystem").GetComponent<LevelSystem>();
        dailyBonus = GameObject.FindGameObjectWithTag("DailyBonus").GetComponent<DailyBonus>();
        endGamePanel.SetActive(false);
    }

    public void SetResult(int resultIndex)
    {
        gameResult.text = endGameResult[resultIndex];

        endGamePanel.SetActive(true);

        var exp = 0;
        if (resultIndex == 1)
        {
            if (dailyBonus.DailyBonusCompleted() == true)
                exp = 20;
            else exp = 10;
        }
        else if (resultIndex == 2) exp = 5;
        else exp = 1;

        levelSystem.AddExp(exp);
        Debug.LogFormat("I get {0} points", exp);
    }
}
