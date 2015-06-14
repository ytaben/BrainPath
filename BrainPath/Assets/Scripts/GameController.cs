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
    }

    //References to the game object and scripts of the active brain node
    private GameObject activeNode;
    private BrainNode activeNodeScript;

    //Brain animator controller
    Animator brainAnimator;
    BrainNode.AnimationChoice currentAnimationState; //Current state is used to go back after hovering mouse over an element

    public GameObject startNode; //Set start node in editor for every level
    private int gameTime = 0;
    public int TimeLimit;
    private Text gameTimeText;
    // Use this for initialization

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        foreach (GameObject brainNode in GameObject.FindGameObjectsWithTag("BrainNode"))
        {
            brainNode.SetActive(false);
        }    
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
    }

    //This function is used to notify GameController of a transition to another node
    public void Transition(GameObject destination, int cost)
    {
        activeNode = destination;
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        gameTime += cost;
        UpdateTimeText();
    }

    public void UpdateTimeText()
    {
        gameTimeText.text = "Time: " + gameTime.ToString() + " ms";
        if (gameTime > TimeLimit / 2) { gameTimeText.color = Color.yellow; }
        if (gameTime > TimeLimit / 4) { gameTimeText.color = Color.red; }
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
            case BrainNode.AnimationChoice.UpsideDown: brainAnimator.SetBool("IsUpsideDown", true); Debug.Log("Got here"); break;
        }
    }
    public void SetCurrentAnimation() { SetBrainAnimation(currentAnimationState); }

    // Update is called once per frame
    void Update()
    {

    }

}
