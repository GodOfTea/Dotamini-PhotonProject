using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Save
{ 
    public static void SaveLvlData(LevelSystem levelSystem)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/lvlData.gft";

        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData(levelSystem);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveTimeData(TimeManager timeManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/timeData.gft";

        FileStream stream = new FileStream(path, FileMode.Create);
        TimeData data = new TimeData(timeManager);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveDailyBounceData(DailyBonus dailyBonus)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/dailyBounce.gft";

        FileStream stream = new FileStream(path, FileMode.Create);
        DailyBounceData data = new DailyBounceData(dailyBonus);
        formatter.Serialize(stream, data);

        stream.Close();
    }
}
