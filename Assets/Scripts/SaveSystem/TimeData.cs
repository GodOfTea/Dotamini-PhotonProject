using System;

[Serializable]
public class TimeData
{
    public string lastEnter;


    public TimeData(TimeManager timeManager)
    {
        lastEnter = timeManager.GetLastEnter();
    }
}
