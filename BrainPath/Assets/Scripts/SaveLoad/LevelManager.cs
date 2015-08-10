using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelManager : MonoBehaviour {
    Level level;

    public string name;
    public string sceneName;
    public Sprite Picture;
    public int maxScore;

    public Text nameText;
    public Text maxScoreText;
    public Image image;

    void Start()
    { GetComponent<Button>().onClick.AddListener(OnClick); }
	
    void OnClick()
    {
        SaveLoad.currentLevel = name;
        Application.LoadLevel(sceneName);
    }

    public void UpdateView()
    {
        nameText.text = name;
        if (SaveLoad.levelsDict.TryGetValue(name, out level))
        {
            if (level.isComplete)
            {
                maxScoreText.text = "Max Score: " + level.maxScore.ToString();
            }
            else
                maxScoreText.text = "Max Score: 0";
        }
    }

    public void OnEnable()
    {
        UpdateView();
    }
}

[System.Serializable]
public struct Level
{
    public string name;
    public int maxScore;
    public bool isComplete;
    public bool isUnlocked;
}
