using UnityEngine;
using System.Collections;

public class MaterialController : MonoBehaviour {
    public Material undiscoveredMaterial, currentMaterial, outboundMaterial;
    // Use this for initialization
    private static MaterialController instance;
    public static MaterialController getInstance()
    {
        return instance ? instance : new MaterialController();
    }
    void Awake()
    {
        instance = this;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
