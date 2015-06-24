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

    public GameObject tree;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        iconTransform = (RectTransform)rectTransform.FindChild("ArrowIcon");
        offset = rectTransform.sizeDelta.y;
    }
	// Use this for initialization
	void Start () {
        
        

	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
    public void DeactivateChildren()
    {
        foreach (GameObject child in children)
        {
            child.GetComponent<TreeNode>().DeactivateChildren();
            
        }
        gameObject.SetActive(false);
    }

    void OnClick()
    {
        if (isExpanded)
        {
            isExpanded = false;
            foreach (GameObject child in children)
            {
                child.GetComponent<TreeNode>().DeactivateChildren();
            }
            
        }
        else
        {
            isExpanded = true;
            foreach (GameObject child in children)
            {
                child.SetActive(true);
            }
            //float verticalOffset = (float)(offset * 1.5);
            //foreach (GameObject child in children)
            //{
            //    child.SetActive(true);
            //    Vector2 position = child.GetComponent<RectTransform>().position;
            //    position.x = rectTransform.position.x + horizontalOffset;
            //    position.y = rectTransform.position.y - verticalOffset;
            //    child.GetComponent<RectTransform>().position = position;
            //    verticalOffset += offset;
            //}
        }
        SetIconRotation();
        tree.GetComponent<TreeView>().RefreshTree();
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
    
    public float DrawChildren()
    {
        if (!isActiveAndEnabled) { return 0; }
        float verticalOffset = (float)(offset * 1.5);
        if (isExpanded) { 
        
        foreach (GameObject child in children)
        {
            Vector2 position = GetComponent<RectTransform>().position;
            position.x = rectTransform.position.x + horizontalOffset;
            position.y = rectTransform.position.y - verticalOffset;
                child.GetComponent<RectTransform>().position = position;
                verticalOffset += child.GetComponent<TreeNode>().DrawChildren();
         }
            
        }
        return verticalOffset;
        return 0; //remove this
    }
}
