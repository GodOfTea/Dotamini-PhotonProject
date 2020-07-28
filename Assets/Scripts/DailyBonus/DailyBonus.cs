using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonus : MonoBehaviour
{
    [SerializeField] private UI2DSprite[] dailyBonusBoxes = new UI2DSprite[3];
    public int dailyBonusCount;

    [SerializeField] private Sprite emptyBox;
    [SerializeField] private Sprite completedBox;

    public static DailyBonus Instance { get; private set; }


    private void Awake()
    {
        CheckInstance();
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

    private void Start()
    {
        InitBonusBars();

        var data = Load.LoadDailyBounceData();

        if (data != null)
            dailyBonusCount = data.dailyBounceCount;

        for (int i = 0; i < dailyBonusCount; i++)
            dailyBonusBoxes[i].sprite2D = completedBox;
    }

    public bool DailyBonusCompleted()
    {
        InitBonusBars();

        if (dailyBonusCount < 3)
        {
            dailyBonusBoxes[dailyBonusCount++].sprite2D = completedBox;

            Save.SaveDailyBounceData(this);
            Debug.Log("DailyBounceSave");

            return true;
        }
        else
            return false;
    }

    /* Вызывается ивентом из TimeManager */
    public void RefreshDailyBonus()
    {
        dailyBonusCount = 0;
        foreach (var box in dailyBonusBoxes)
            box.sprite2D = emptyBox;

        Save.SaveDailyBounceData(this);
    }

    private void InitBonusBars()
    {
        dailyBonusBoxes = GameObject.FindGameObjectWithTag("BonusBars").GetComponent<BonusBars>().bonusBars;
    }
}
