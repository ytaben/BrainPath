using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

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

    public GameObject startNode; //Set start node in editor for every level
    // Use this for initialization
    void Start()
    {
        activeNode = startNode; //Set member references 
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        ReinitializeField(); //Initialize the field when the game starts
    }

    //This function is used to notify GameController of a transition to another node
    public void Transition(GameObject destination)
    {
        activeNode = destination;
        activeNodeScript = activeNode.GetComponent<BrainNode>();
        ReinitializeField(); //Reinitialize field after every transition
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
    // Update is called once per frame
    void Update()
    {

    }

}
