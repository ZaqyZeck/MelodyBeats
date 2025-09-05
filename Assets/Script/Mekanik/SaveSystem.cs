using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveScoresData(List<Score> scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.dt";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoresData scoresData = new ScoresData(scores);

        formatter.Serialize(stream, scoresData);
        stream.Close();
    }

    public static void SaveScoresDataDua(List<Score> scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scoreDua.dt";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoresData scoresData = new ScoresData(scores);

        formatter.Serialize(stream, scoresData);
        stream.Close();
    }

    public static void DeleteScoresData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.dt";
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoresData scoresData = new ScoresData();

        formatter.Serialize(stream, scoresData);
        stream.Close();
    }

    public static ScoresData LoadScores()
    {
        string path = Application.persistentDataPath + "/score.dt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoresData scoresData = formatter.Deserialize(stream) as ScoresData;

            stream.Close();
            return scoresData;
        }
        else
        {
            Debug.LogError("not found in " + path);
            return null;
        }
    }

    public static ScoresData LoadScoresDua()
    {
        string path = Application.persistentDataPath + "/scoreDua.dt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoresData scoresData = formatter.Deserialize(stream) as ScoresData;

            stream.Close();
            return scoresData;
        }
        else
        {
            Debug.LogError("not found in " + path);
            return null;
        }
    }
}
