using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Level level;

    public string name;
    public Sprite Picture;
    public int maxScore;

    public Text nameText;
    public Image image;
	
}

[System.Serializable]
public struct Level
{
    public string name;
    public int maxScore;
    public bool isComplete;
    public bool isUnlocked;
}
