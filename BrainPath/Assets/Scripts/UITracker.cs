using UnityEngine;
using System.Collections;

public class UITracker : MonoBehaviour {

    public GameObject tracked;

    private RectTransform rect;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
        offset = rect.position - tracked.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        rect.position = new Vector3(tracked.transform.position.x, tracked.transform.position.y, tracked.transform.position.z);
	}
}
