using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour {

    public GameObject menu;

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        if (!button)
        {
            Debug.Log("SetBrainState has to be attached to a button");
            return;
        }
        button.onClick.AddListener(OnClick);

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnClick()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else menu.SetActive(true);
    }
}
