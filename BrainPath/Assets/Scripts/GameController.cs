using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //Make sure GameController is singleton and easily accessible
    private static GameController instance;
    public GameController getInstance() { if (instance) { return instance; } else { return new GameController(); } }
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
