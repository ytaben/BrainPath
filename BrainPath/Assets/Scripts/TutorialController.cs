using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour
{

    private static TutorialController instance;

    public static TutorialController getInstance() { return instance; }
    void Awake()
    {
        instance = this;
    }

    public int totalStages;
    public int currentStage = 0;
    public GameObject[] helpScreens;
    // Use this for initialization
    void Start()
    {
        helpScreens[currentStage].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementStage()
    {
        helpScreens[currentStage].SetActive(false);
        currentStage++;
        if (currentStage <= totalStages)
        {
            helpScreens[currentStage].SetActive(true);
        }
    }
}
