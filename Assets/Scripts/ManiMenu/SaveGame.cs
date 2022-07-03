using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    public static void SaveGameData(CheckGameStatus status)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData.7SINS";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(status);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log(path);
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/gameData.7SINS";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save File not found in" + path);
            return null;
        }
    }
    public static void ClearFile()
    {
        string path = Application.persistentDataPath + "/gameData.7SINS";
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
        Directory.CreateDirectory(path);
    }
    public static bool haveFile()
    {
        string path = Application.persistentDataPath + "/gameData.7SINS";
        if (File.Exists(path))
        {
            return true;
        }
        return false;
    }
}

