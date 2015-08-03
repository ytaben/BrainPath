using UnityEngine;
using System.Collections;

public class LabelUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateLabels()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("BrainPart"))
        {
            BrainNode3D brainNode = gameObject.GetComponent<BrainNode3D>();
            if (brainNode) { brainNode.UpdateLabel(); }
        }
    }
}
