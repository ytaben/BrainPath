using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Make UI element pulse by changing its alpha
/// This script is active as long as the object containing it is active
/// </summary>
public class PulseUI : MonoBehaviour {

    public GameObject[] elements;
    private bool isPulsing;

	// Use this for initialization
	void Start () {
        //Canvas group controlls alpha, so we need it attached to every pulsing element
        foreach (GameObject element in elements)
        {
            if (!element.GetComponent<CanvasGroup>())
            {
                element.AddComponent<CanvasGroup>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject element in elements)
        {
            CanvasGroup cg = element.GetComponent<CanvasGroup>();
            cg.alpha = Mathf.PingPong(cg.alpha, Time.time);
        }
	}


    //This may be somewhat redundant, but we can make it just in case
    void OnEnable()
    {
        isPulsing = true;
    }

    void OnDisable()
    {
        isPulsing = false;
    }
}
