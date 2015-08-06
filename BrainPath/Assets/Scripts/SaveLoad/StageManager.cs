using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public string name;
    public LevelManager[] levels; //Levels that belong to this stage

    public Text nameText;
    public Text levelsCompleteText;
    public Text totalScoreText;

    public void UpdateView()
    {
        int levelsComplete = 0;
        int totalScore = 0;

        foreach (LevelManager manager in levels)
        {
            Level level = SaveLoad.levelsDict[manager.name];

            if (level.isComplete) levelsComplete += 1;

            if (level.maxScore > 0) totalScore += level.maxScore;
        }

        levelsCompleteText.text = "Levels Completed: " + levelsComplete.ToString() + "/" + levels.Length.ToString();
        totalScoreText.text = "Total Score: " + totalScore.ToString();
    }

    public void OnEnable()
    {
        UpdateView();
    }
}
