using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainNode3D : MonoBehaviour {

    public BrainNode brainNode; //Reference to the actual brain node that controlls this script
    public GameObject labelPrefab; //Prefab for the animated label
    public Transform labelPosition;

    public bool labelNormal;
    public bool labelSplit;
    public bool labelUpsideDown;

    private GameObject label; //Track if we already have a label created
    Text labelText;
    UITracker labelPositioner;
    LabelAnimator labelAnimator;

    private bool needLabelDespawn;
    private bool needLabelSpawn;

    private GameController gameController;

    MaterialController materialController;
	// Use this for initialization
	void Start () {
        materialController = MaterialController.getInstance();
        gameController = GameController.getInstance();
	}

    void Update()
    {
        if (needLabelDespawn && !labelAnimator.Busy()) { labelAnimator.Collapse(); needLabelDespawn = false; }
        if (needLabelSpawn && !labelAnimator.Busy()) { labelAnimator.Expand(); needLabelSpawn = false; }
    }

    void OnMouseOver()
    {
        if (brainNode && brainNode.isExplored)
        {
            brainNode.OnPointerEnter();
        }
    }

    void OnMouseExit()
    {
        if (brainNode && brainNode.isExplored)
        {
            brainNode.OnPointerExit();
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
            label.transform.SetAsFirstSibling();
        }
        if (!labelText) labelText = label.GetComponentInChildren<Text>();
        labelText.text = brainNode.name;

        if (!labelPositioner) labelPositioner = label.GetComponent<UITracker>();
        labelPositioner.SetPosition(Camera.main.WorldToScreenPoint(labelPosition.position));

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

    public void UpdateLabel()
    {
        if (!labelPosition) return;
        if (!brainNode.isExplored) return;
        
        if (gameController.currentAnimationState == BrainNode.AnimationChoice.Normal)
        {
            if (labelNormal) SpawnLabel();
            else DespawnLabel();
        }
        if (gameController.currentAnimationState == BrainNode.AnimationChoice.Split)
        {
            if (labelSplit) { SpawnLabel(); Debug.Log("Got here"); }
            else DespawnLabel();
        }
        if (gameController.currentAnimationState == BrainNode.AnimationChoice.UpsideDown)
        {
            if (labelUpsideDown) SpawnLabel();
            else DespawnLabel();
        }
    }


}
