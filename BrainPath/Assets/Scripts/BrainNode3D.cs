using UnityEngine;
using System.Collections;

public class BrainNode3D : MonoBehaviour {

    MaterialController materialController;
	// Use this for initialization
	void Start () {
        materialController = MaterialController.getInstance();
	}
    //void OnMouseOver()
    //{
    //    Material[] mats = GetComponent<Renderer>().materials;
    //    mats[0] = materialController.currentMaterial;
    //    GetComponent<Renderer>().materials = mats;
    //}
    //void OnMouseExit()
    //{
    //    Material[] mats = GetComponent<Renderer>().materials;
    //    mats[0] = materialController.undiscoveredMaterial   ;
    //    GetComponent<Renderer>().materials = mats;
    //}


}
