using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialButton : MonoBehaviour {
    public int correctStage;
    private TutorialController tutorialController;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
        tutorialController = TutorialController.getInstance();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick() {
        if (tutorialController.currentStage == correctStage) tutorialController.IncrementStage();
    }
}
