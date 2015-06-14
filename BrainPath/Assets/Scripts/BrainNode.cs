﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BrainNode : MonoBehaviour
{
    public bool isActive; //Determine whether this node is currently selected
    public bool isExplored;
    public bool isNew;

    public System.Collections.Generic.Dictionary<GameObject, int> outboundNodes; //HashTable of nodes that are connected to this one
    [Serializable]
    public struct OutboundEdge
    {
        public GameObject destination;
        public int cost;
    };
    public OutboundEdge[] outboundEdges; //An array of struct to conviniently display in editor

    public GameObject[] brainParts; //List of brain parts that correspond to this node

    public Canvas nodeCanvas;


    //A drop down menu to choose which animation should be triggered upon being active or hovering over this node
    public enum AnimationChoice {Normal, Split, UpsideDown}
    public AnimationChoice BrainState;

    // private CanvasGroup canvasGroup; //Useful to control alpha of the element
    private Button button;
    private GameController gameController; //Reference to gameController (singleton)
    private MaterialController materialController;
    // Use this for initialization
    void Start()
    {
        //canvasGroup = GetComponent<CanvasGroup>(); //Initialize member variables
        gameController = GameController.getInstance();
        materialController = MaterialController.getInstance();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBrainNode); //Add an onClick listener 
        outboundNodes = new System.Collections.Generic.Dictionary<GameObject, int>();
        foreach (OutboundEdge edge in outboundEdges)
        {
            outboundNodes[edge.destination] = edge.cost;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Refresh()
    {
        ColorBlock buttonColors;
        if (!isExplored)
        {
            buttonColors = button.colors;
            buttonColors.normalColor = Color.green;
            button.colors = buttonColors;
            SetMaterial(materialController.undiscoveredMaterial);
            //canvasGroup.alpha = 1;
        }
        if (isNew)
        {
            SetMaterial(materialController.outboundMaterial);
        }
        //If this node is active, pulse by changing alpha
        if (isActive)
        {
            buttonColors = button.colors;
            buttonColors.normalColor = Color.green;
            button.colors = buttonColors;
            SetMaterial(materialController.currentMaterial);
            //canvasGroup.alpha = Mathf.PingPong(Time.time, 1);
        }
    }

    public void OnMouseEnter()
    {

    }
    public void OnMouseExit()
    {

    }

    private void SetMaterial(Material material)
    {
        foreach (GameObject brainPart in brainParts)
        {
            Material[] mats = brainPart.GetComponent<Renderer>().materials;
            mats[0] = material;
            brainPart.GetComponent<Renderer>().materials = mats;
        }
    }
    //On Click - notify gameController using the Transition method
    void OnClickBrainNode()
    {
        if (isActive) { nodeCanvas.gameObject.SetActive(enabled); return; }
        gameController.Transition(gameObject, 0); //TODO: SET APPROPRIATE COST
    }
}
