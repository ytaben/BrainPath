using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //Make sure GameController is singleton and easily accessible
    private static GameController instance;
    public static GameController getInstance() { if (instance) { return instance; } else { return new GameController(); } }
    void Awake()
    {
        instance = this;
        brainAnimator = GameObject.Find("Brain").GetComponent<Animator>(); if (!brainAnimator) Debug.Log("null animator");
        gameTimeText = GameObject.Find("GameTimeText").GetComponent<Text>();
        allBrainNodes = GameObject.FindGameObjectsWithTag("BrainNode");
    }

    //References to the game object and scripts of the active brain node
    public GameObject activeNode { get; set; }
    private BrainNode activeNodeScript;
    public GameObject[] allBrainNodes;

    //Brain animator controller
    Animator brainAnimator;
    BrainNode.AnimationChoice currentAnimationState; //Current state is used to go back after hovering mouse over an element

    public GameObject startNode; //Set start node in editor for every level
    private int gameTime = 0;
    public int TimeLimit;
    public int BaseLevelScore;
    private Text gameTimeText;

    public string victoryMessage;
    public string defeatMessage;

    public Text messageText;
    public GameObject messagePanel;

    public int currentStage = 0;
    public int winningStage;

    //Fields to display final score
    public GameObject finalScorePanel;
    public Text finalResultsText;
    public Text baseScoreText;
    public Text timeLeftScoreText;
    public Text totalScoreText;

    // Use this for initialization

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject brainNode in GameObject.FindGameObjectsWithTag("BrainNode"))
        {
            brainNode.SetActive(false);
        }
        SetBrainPartsUnexplored();
        activeNode = startNode; //Set member references 
        activeNode.SetActive(true);
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        activeNodeScript.isExplored = true;
        activeNodeScript.isActive = true;
        currentAnimationState = activeNodeScript.BrainState;
        SetCurrentAnimation();
        activeNodeScript.Refresh();
        activeNodeScript.ExploreOutboundObjects();
        UpdateTimeText();
        GameObject.Find("TimeLimitText").GetComponent<Text>().text = "Timelimit: " + TimeLimit.ToString() + " ms";
    }

    //This function is used to notify GameController of a transition to another node
    public void Transition(GameObject destination, int cost)
    {
        BrainNode oldNodeScript = activeNodeScript;
        oldNodeScript.isActive = false;

        activeNode = destination;
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        activeNodeScript.isActive = true;
        activeNodeScript.isNew = false;
        currentAnimationState = activeNodeScript.BrainState;
        SetCurrentAnimation();
        activeNodeScript.ExploreOutboundObjects();

        //Refresh both new and old nodes
        oldNodeScript.Refresh();
        activeNodeScript.Refresh();

        gameTime += cost;
        UpdateTimeText();
    }

    public void UpdateTimeText()
    {
        gameTimeText.text = "Time: " + gameTime.ToString() + " ms";
        if (gameTime > TimeLimit / 2) { gameTimeText.color = Color.yellow; }
        if (gameTime > TimeLimit * 3 / 4) { gameTimeText.color = Color.red; }
    }

    public void RefreshActive()
    {
        activeNodeScript.Refresh();
    }

    public void DisplayMessage(string message, Color? color = null)
    {
        messagePanel.SetActive(true);
        messageText.color = color ?? Color.black;
        messageText.text = message;
    }

    public void EndGame()
    {
        bool isWin = currentStage == winningStage;

        finalScorePanel.SetActive(true);

        finalResultsText.text = isWin ? victoryMessage : defeatMessage;
        finalResultsText.color = isWin ? Color.green : Color.red;

        int timeLeftScore = TimeLimit - gameTime;
        timeLeftScoreText.text = Mathf.Clamp(timeLeftScore, 0, float.MaxValue).ToString();
        if (timeLeftScore < 1) { timeLeftScoreText.color = Color.red; }
        else
        timeLeftScoreText.color = timeLeftScore / TimeLimit > 0.75 ? Color.green : Color.yellow;

        baseScoreText.text = isWin ? BaseLevelScore.ToString() : "0";
        baseScoreText.color = isWin ? Color.green : Color.red;

        int totalScore = timeLeftScore + BaseLevelScore;

        totalScoreText.text = totalScore.ToString();
        totalScoreText.color = isWin ? Color.green : Color.red;
    }
    //Reinitialize the game field by disabling all brain nodes, then reactivating only the current one and
    //All the outbound nodes
    void ReinitializeField()
    {
        foreach (GameObject brainNode in GameObject.FindGameObjectsWithTag("BrainNode"))
        {
            brainNode.SetActive(false);
            brainNode.GetComponent<BrainNode>().isActive = false;
        }
        activeNode.SetActive(true);
        activeNodeScript.isActive = true;
        foreach (GameObject brainNode in activeNodeScript.outboundNodes.Keys)
        {
            brainNode.SetActive(true);
        }
    }

    public void ResetBrainAnimation()
    {
        brainAnimator.SetBool("IsUpsideDown", false);
        brainAnimator.SetBool("IsSeparated", false);
    }
    public void SetBrainAnimation(BrainNode.AnimationChoice animation)
    {
        ResetBrainAnimation();
        switch (animation)
        {
            case BrainNode.AnimationChoice.Split: brainAnimator.SetBool("IsSeparated", true); break;
            case BrainNode.AnimationChoice.UpsideDown: brainAnimator.SetBool("IsUpsideDown", true); break;
        }
    }
    public void SetCurrentAnimation() { SetBrainAnimation(currentAnimationState); }

    public void IncreaseTime(int time)
    {
        gameTime += time;
        UpdateTimeText();
    }
    public void IncrementStage()
    {
        currentStage++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetBrainPartsUnexplored()
    {
        MaterialController materialController = MaterialController.getInstance();
        foreach (GameObject brainPart in GameObject.FindGameObjectsWithTag("BrainPart")){
            Material[] mats = brainPart.GetComponent<Renderer>().materials;
            mats[0] = materialController.undiscoveredMaterial;
            brainPart.GetComponent<Renderer>().materials = mats;
        }
    }
}
