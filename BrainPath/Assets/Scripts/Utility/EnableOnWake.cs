using UnityEngine;
using System.Collections;

public class EnableOnWake : MonoBehaviour {

    public GameObject[] gameObjects;

	// Use this for initialization
	void Start () {
	    foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
