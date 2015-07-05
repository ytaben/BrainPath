using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Path node is responsible for the UI elements in the bottom panel
[RequireComponent(typeof(Button))]
public class PathNode : MonoBehaviour {

    public GameObject previousArrow, nextArrow; //References to previous and next arrows
    public GameObject previousNode, nextNode; //References to the previous and next PathNodes

    
    public Sprite yellowArrow, greenArrow; //References to the yellow and green arrow images
    private static Sprite _yellowArrow, _greenArrow; 

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        //This is a hack to allow specifying images only in one object
        if (yellowArrow) { _yellowArrow = yellowArrow; } 
        if (_greenArrow) { _greenArrow = greenArrow; }
    }

	// Use this for initialization
	void Start () {
        button.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MarkCorrect()
    {
        ColorBlock colors = button.colors;
        colors.normalColor = Color.green;
        colors.disabledColor = Color.green;
        button.colors = colors;

        if (nextArrow) { nextArrow.GetComponent<Image>().sprite = yellowArrow; }
        if (previousArrow) { previousArrow.GetComponent<Image>().sprite = greenArrow; }

        if (nextNode)
        {
            nextNode.SetActive(true);
            nextNode.GetComponentInChildren<Text>().text = "??????";
            Button nextButton = nextNode.GetComponent<Button>();
            colors = nextButton.colors;
            colors.normalColor = Color.yellow;
            colors.disabledColor = Color.yellow;
            nextButton.colors = colors;
        }
    }
}
