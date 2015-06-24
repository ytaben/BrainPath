using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreeNode : MonoBehaviour {

    public GameObject[] children;
    public GameObject nextNode;

    public int horizontalOffset;

    private bool isExpanded = false;
    private float offset;

    private RectTransform rectTransform;
    private RectTransform iconTransform;

	// Use this for initialization
	void Start () {
        rectTransform = GetComponent<RectTransform>();
        offset = rectTransform.sizeDelta.y;

        iconTransform = (RectTransform)rectTransform.FindChild("ArrowIcon");
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}

    void OnClick()
    {
        if (isExpanded)
        {
            isExpanded = false;
            foreach (GameObject child in children)
            {
                child.SetActive(false);
            }
        }
        else
        {
            isExpanded = true;
            float verticalOffset = (float)(offset * 1.5);
            foreach (GameObject child in children)
            {
                child.SetActive(true);
                Vector2 position = child.GetComponent<RectTransform>().position;
                position.x = rectTransform.position.x + horizontalOffset;
                position.y = rectTransform.position.y - verticalOffset;
                child.GetComponent<RectTransform>().position = position;
                verticalOffset += offset;
            }
        }
        SetIconRotation();
    }

    public void SetIconRotation()
    {
        if (isExpanded)
        {
            iconTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            iconTransform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
