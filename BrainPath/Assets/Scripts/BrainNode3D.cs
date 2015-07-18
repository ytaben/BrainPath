using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainNode3D : MonoBehaviour {

    public BrainNode brainNode; //Reference to the actual brain node that controlls this script
    public GameObject labelPrefab; //Prefab for the animated label

    private GameObject label; //Track if we already have a label created
    Text labelText;
    UITracker labelPositioner;
    LabelAnimator labelAnimator;

    private bool needLabelDespawn;
    private bool needLabelSpawn;

    MaterialController materialController;
	// Use this for initialization
	void Start () {
        materialController = MaterialController.getInstance();
	}

    void Update()
    {
        if (needLabelDespawn && !labelAnimator.Busy()) { labelAnimator.Collapse(); needLabelDespawn = false; }
        if (needLabelSpawn && !labelAnimator.Busy()) { labelAnimator.Expand(); needLabelSpawn = false; }
    }

    void OnMouseOver()
    {
        if (brainNode &&  brainNode.isExplored)
        {
            brainNode.OnPointerEnter();

            SpawnLabel();
            needLabelDespawn = false;
        }
    }

    void OnMouseExit()
    {
        if (brainNode && brainNode.isExplored)
        {
            brainNode.OnPointerExit();
            DespawnLabel();
            needLabelSpawn = false;
        }
    }

    void OnMouseDown()
    {
        if (brainNode &&  brainNode.isExplored)
        {
            brainNode.OnClickBrainNode();
        }
    }

    private void SpawnLabel()
    {
        if (!label)
        {
            label = Instantiate(labelPrefab) as GameObject;
            label.transform.SetParent(GameObject.Find("Canvas").transform);
        }
        if (!labelText) labelText = label.GetComponentInChildren<Text>();
        labelText.text = brainNode.name;

        if (!labelPositioner) labelPositioner = label.GetComponent<UITracker>();
        labelPositioner.SetPosition(Input.mousePosition);

        if (!labelAnimator) labelAnimator = label.GetComponent<LabelAnimator>();
        labelAnimator.labelText = brainNode.name;
        if (labelAnimator.Busy()) needLabelSpawn = true;
        else labelAnimator.Expand();
    }

    private void DespawnLabel()
    {
        if (label)
        {
            if (labelAnimator.Busy()) needLabelDespawn = true;
            else labelAnimator.Collapse();
        }
    }


}
