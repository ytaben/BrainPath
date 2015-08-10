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

    public static string currentLevel; //store the name of the currently played level

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
            FileStream file;
            try {
                 file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            }
            catch (FileNotFoundException) { return; }
            levels = (List<Level>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void UpdateLevel(string name, int score, bool isWin)
    {
        Level level;

        if (levelsDict.TryGetValue(name, out level)) //We already played this level
        {
            level.maxScore = score > level.maxScore ? score : level.maxScore;
            level.isComplete = level.isComplete || isWin;
        }

        else //This is the first time
        {
            level.name = name;
            level.maxScore = score;
            level.isComplete = isWin;
        }

        levelsDict[name] = level;

        SaveLevels();
    }

    public static void NotifyLevelComplete(int score, bool isWin)
    {
        if (currentLevel == "") return;

        UpdateLevel(currentLevel, score, isWin);
        currentLevel = "";
    }
	
}
