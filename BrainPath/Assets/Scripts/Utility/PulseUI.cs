using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Make UI element pulse by changing its alpha
/// This script is active as long as the object containing it is active
/// </summary>
public class PulseUI : MonoBehaviour
{

    public GameObject[] elements;

    //Stop pulsing after this time, unless it's zero or negative
    public float timeout;

    private float stopTime; //Time when we must stop pulsing
    private bool isPulsing;

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {
        if (timeout > 0 && Time.time > stopTime) isPulsing = false; //Stop if we have a timeout and it has passed


        foreach (GameObject element in elements)
        {
            CanvasGroup cg = element.GetComponent<CanvasGroup>();
            if (isPulsing)
            {
                cg.alpha = Mathf.PingPong(Time.time, 1);
            }
            else cg.alpha = 1;
        }

    }


    //This may be somewhat redundant, but we can make it just in case
    void OnEnable()
    {
        isPulsing = true;
        stopTime = Time.time + timeout;
    }

    void OnDisable()
    {
        isPulsing = false;
    }
}
