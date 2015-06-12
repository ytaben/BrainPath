using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualCortexButton : MonoBehaviour {
    bool anim;
	// Use this for initialization
	void Start () {
        anim = false;
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick() {
        //Animator brain = GameObject.Find("Brain").GetComponent<Animator>();
        //anim = !anim;
        //brain.SetBool("IsSeparated", anim);
    }
}
