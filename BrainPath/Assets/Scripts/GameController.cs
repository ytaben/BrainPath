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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
