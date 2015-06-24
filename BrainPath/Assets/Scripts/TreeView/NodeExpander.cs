using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This script should be attached to a button responsible for expanding/collapsing a given node.
/// A public reference is used to determine which node this button belongs to
/// </summary>
[RequireComponent (typeof (Button))]
public class NodeExpander : MonoBehaviour {

    public TreeNode node;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	void OnClick ()
    {
        node.Toggle();
    }
}
