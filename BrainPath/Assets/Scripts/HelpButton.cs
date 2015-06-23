using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour {

    public GameObject helpMenu;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
    public void OnClick()
    {
        helpMenu.SetActive(true);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
