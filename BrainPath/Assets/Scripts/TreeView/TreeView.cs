using UnityEngine;
using System.Collections;

public class TreeView : MonoBehaviour {

    public GameObject[] nodes; //Highest level nodes

    public float initialHorizontal, initialVertical;

	// Use this for initialization
	void Start () {
        RefreshTree();
	}

    public void RefreshTree()
    {
        float horizontalOffset = initialHorizontal;
        float verticalOffset = initialVertical;
        foreach (GameObject node in nodes)
        {
            Vector2 position = GetComponent<RectTransform>().position;
            position.x += horizontalOffset;
            position.y -= verticalOffset;
            node.GetComponent<RectTransform>().position = position;
            verticalOffset += node.GetComponent<TreeNode>().DrawChildren();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
