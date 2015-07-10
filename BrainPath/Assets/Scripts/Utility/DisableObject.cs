using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisableObject : MonoBehaviour {

    public GameObject canvas; //The parent canvas that holds this window
	// Use this for initialization
	void Start () {
        //Attach OnClick delegate to the button's onClick
        Button button = GetComponent<Button>();
        if (button) { button.onClick.AddListener(OnClick); }
        else Debug.Log("ActionButton must be attached to a button!");

        if (!canvas) Debug.Log("No canvas specified for close button!");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //Close this window
    public void OnClick()
    {
        canvas.SetActive(false);
    }
}
