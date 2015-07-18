using UnityEngine;
using System.Collections;

public class UITracker : MonoBehaviour {

    public GameObject tracked;

    private RectTransform rect;
	// Use this for initialization
	void Start () {
        rect = GetComponent<RectTransform>();
	}
	
	public void SetPosition(Vector3 position)
    {
        rect.transform.position = position;
    }
}
