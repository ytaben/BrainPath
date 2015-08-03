using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class SaveLoad  {
    //Use list for serializing stuff. Use dictionary for runtime access
    public static List<Level> levels;
    public static Dictionary<string, Level> levelsDict;

    private const string savePath = "/GameStage.save";

    public static void Initialize()
    {
        levelsDict = new Dictionary<string, Level>();

        LoadLevels();
    }
    public static void SaveLevels()
    {
        levels.Clear();
        foreach (Level level in levelsDict.Values)
        {
            levels.Add(level);
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savePath);
        bf.Serialize(file, levels);
        file.Close();
    }

    public static void LoadLevels()
    {
        if (File.Exists(Application.persistentDataPath + savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.levels = (List<Level>)bf.Deserialize(file);
            file.Close();
        }
    }
	
}
