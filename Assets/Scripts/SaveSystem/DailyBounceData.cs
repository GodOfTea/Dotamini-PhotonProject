using System;

[Serializable]
public class DailyBounceData
{
    public int dailyBounceCount;


    public DailyBounceData(DailyBonus dailyBonus)
    {
        dailyBounceCount = dailyBonus.dailyBonusCount;
    }
}
