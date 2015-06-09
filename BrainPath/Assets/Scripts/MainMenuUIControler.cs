using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuUIControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("TutorialButton").GetComponent<Button>().onClick.AddListener(OnClickTutorial);
        GameObject.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnClickExit);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickTutorial()
    {
        Application.LoadLevel("Tutorial");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
