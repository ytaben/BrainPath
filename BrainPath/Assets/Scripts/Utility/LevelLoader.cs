using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public string levelName;
	// Use this for initialization
	void Start () {
        //Attach onClick to the button
        Button button = GetComponent<Button>();
        if (button) { button.onClick.AddListener(OnClick); }
        else Debug.Log("LevelLoader script must be attached to a button!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        SaveLoad.currentLevel = levelName;
        Application.LoadLevel(levelName);
    }
}
