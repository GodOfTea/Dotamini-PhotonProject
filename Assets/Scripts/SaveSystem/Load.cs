using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Load
{ 
    public static GameData LoadLvlData()
    {
        string path = Application.persistentDataPath + "/lvlData.gft";

        if (File.Exists(path) == true)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }

    public static TimeData LoadTimeData()
    {
        string path = Application.persistentDataPath + "/timeData.gft";

        if (File.Exists(path) == true)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            TimeData data = formatter.Deserialize(stream) as TimeData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }

    public static DailyBounceData LoadDailyBounceData()
    {
        string path = Application.persistentDataPath + "/dailyBounce.gft";

        if (File.Exists(path) == true)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);
            DailyBounceData data = formatter.Deserialize(stream) as DailyBounceData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + path);
            return null;
        }
    }
}
