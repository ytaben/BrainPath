using UnityEngine;
using System.Collections;

public class BrainNode : MonoBehaviour {
    public bool isActive;

    public GameObject[] outboundNodes;

    private CanvasGroup canvasGroup;
    // Use this for initialization
    void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            canvasGroup.alpha = Mathf.PingPong(Time.time, 1);
        }
        else
            canvasGroup.alpha = 1;
    }
}
