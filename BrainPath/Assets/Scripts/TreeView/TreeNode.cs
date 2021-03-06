﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TreeNode : MonoBehaviour {

    public List<GameObject> children;
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

    public void ActivateChildren()
    {
        if (isExpanded)
        {
            foreach (GameObject child in children)
            {
                TreeNode childNode = child.GetComponent<TreeNode>();
                child.SetActive(true);
                if (childNode.isExpanded) {  childNode.ActivateChildren(); }
            }
        }
    }

    void OnClick()
    {

    }

    public void Toggle()
    {
        if (isExpanded)
        {
            Collapse();
        }
        else
        {
            Expand();
        }
        
    }

    public void Expand()
    {
        isExpanded = true;
        foreach (GameObject child in children)
        {
            child.SetActive(true);
            //child.GetComponent<TreeNode>().ActivateChildren();
        }
        ActivateChildren();
        SetIconRotation();
        tree.GetComponent<TreeView>().RefreshTree();
    }

    

    public void Collapse()
    {
        isExpanded = false;
        foreach (GameObject child in children)
        {
            child.GetComponent<TreeNode>().DeactivateChildren();
        }
    SetIconRotation();
    tree.GetComponent<TreeView>().RefreshTree();
}

    public void AddChild(GameObject child)
    {
        children.Add(child);
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
        float verticalOffset = (float)(offset + 3);
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
