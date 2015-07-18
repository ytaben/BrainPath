using UnityEngine;
using System.Collections;

public class UITracker : MonoBehaviour {

    public GameObject tracked;

    private RectTransform rect;
	// Use this for initialization
	void Start () {
	}
	
	public void SetPosition(Vector3 position)
    {
        if (!rect) { rect = GetComponent<RectTransform>(); }
        rect.transform.position = position;
    }
}
