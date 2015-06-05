using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //Make sure GameController is singleton and easily accessible
    private static GameController instance;
    public static GameController getInstance() { if (instance) { return instance; } else { return new GameController(); } }
    void Awake()
    {
        instance = this;
    }

    //References to the game object and scripts of the active brain node
    private GameObject activeNode;
    private BrainNode activeNodeScript;

    public GameObject startNode;
	// Use this for initialization
	void Start () {
        activeNode = startNode;
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        activeNodeScript.isActive = true;
        ReinitializeField();
	}

    public void Transition(GameObject destination)
    {
        activeNode = destination;
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        ReinitializeField();
    }

    void ReinitializeField()
    {
        foreach (GameObject brainNode in GameObject.FindGameObjectsWithTag("BrainNode"))
        {
            brainNode.SetActive(false);
            brainNode.GetComponent<BrainNode>().isActive = false;
        }
        activeNode.SetActive(true);
        activeNodeScript.isActive = true;
        foreach (GameObject brainNode in activeNodeScript.outboundNodes)
        {
            brainNode.SetActive(true);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

}
