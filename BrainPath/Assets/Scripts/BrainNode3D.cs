using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainNode3D : MonoBehaviour {

    public BrainNode brainNode; //Reference to the actual brain node that controlls this script

    MaterialController materialController;
	// Use this for initialization
	void Start () {
        materialController = MaterialController.getInstance();
	}
    void OnMouseOver()
    {
        brainNode.OnPointerEnter();
    }

    void OnMouseExit()
    {
        brainNode.OnPointerExit();
    }


}
