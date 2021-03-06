﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BrainNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isActive; //Determine whether this node is currently selected
    public bool isExplored;
    public bool isNew;
    private bool isVisited;

    public PathNode pathNode; //Reference to the path node on the bottom of the UI;

    public string name; //Name of the given node

    public System.Collections.Generic.Dictionary<GameObject, int> outboundNodes; //HashTable of nodes that are connected to this one
    [Serializable]
    public struct OutboundEdge
    {
        public GameObject destination;
        public int cost;
    };
    public OutboundEdge[] outboundEdges; //An array of struct to conviniently display in editor

    public GameObject[] brainParts; //List of brain parts that correspond to this node

    public GameObject nodeMenu;


    //A drop down menu to choose which animation should be triggered upon being active or hovering over this node
    public enum AnimationChoice { Normal, Split, UpsideDown }
    public AnimationChoice BrainState;

    // private CanvasGroup canvasGroup; //Useful to control alpha of the element
    private Button button;
    private GameController gameController; //Reference to gameController (singleton)
    private MaterialController materialController;
    private TreeNode treeNode;
    // Use this for initialization
    void Awake()
    {

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBrainNode); //Add an onClick listener 
        outboundNodes = new System.Collections.Generic.Dictionary<GameObject, int>();
        treeNode = GetComponent<TreeNode>();
        foreach (OutboundEdge edge in outboundEdges) { outboundNodes[edge.destination] = edge.cost; }
    }

    void Start()
    {
        gameController = GameController.getInstance();
        materialController = MaterialController.getInstance();
        //canvasGroup = GetComponent<CanvasGroup>(); //Initialize member variables

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Refresh()
    {

        if (!isExplored)
        {
            SetColor(Color.black);
            SetMaterial(materialController.undiscoveredMaterial);
            //canvasGroup.alpha = 1;
            return;
        }
        SetColor(Color.white);
        SetMaterial(materialController.normalMaterial);
        if (isNew)
        {
            SetColor(Color.yellow);
            SetMaterial(materialController.outboundMaterial);
        }

        if (isActive)
        {
            SetColor(Color.green);
            SetMaterial(materialController.currentMaterial);
            //canvasGroup.alpha = Mathf.PingPong(Time.time, 1);
            return;
        }
    }

    public void ExploreOutboundObjects()
    {
        foreach (BrainNode.OutboundEdge brainNode in outboundEdges)
        {
            if (true)
            {//!brainNode.destination.activeSelf) { 
                brainNode.destination.SetActive(true);
                BrainNode script = brainNode.destination.GetComponent<BrainNode>();
                script.isNew = true; script.isExplored = true; script.Refresh();
                Refresh();
            }
        }
    }

    public void MarkCorrectPathNode()
    {
        if (pathNode) pathNode.MarkCorrect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnter();
        gameController.SetBrainAnimation(BrainState);
    }

    public void OnPointerEnter()
    {
        if (isActive) return;
        SetColor(Color.yellow);
        SetMaterial(materialController.highlitedMaterial);
        GetComponent<Text>().fontStyle = FontStyle.Bold;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameController.SetCurrentAnimation();
        OnPointerExit();
    }

    public void OnPointerExit()
    {

        GetComponent<Text>().fontStyle = FontStyle.Normal;
        Refresh();
        gameController.RefreshActive();
    }

    private void SetColor(Color color)
    {
        ColorBlock buttonColors;
        buttonColors = button.colors;
        buttonColors.normalColor = color;
        buttonColors.highlightedColor = color;
        button.colors = buttonColors;
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
    public void OnClickBrainNode()
    {


        //if (isActive) { nodeMenu.gameObject.SetActive(enabled); return; } //We open menus automatically now
        Dictionary<GameObject, int> costs = FindCost(gameObject);
        int cost = costs[gameController.activeNode];
        //If cost is greater than 0 - pop a window asking for confirmation
        if (cost > 0)
        {
            //Prompt a modal panel to confirm navigation
            ModalPanel modalPanel = ModalPanel.Instance();
            if (!modalPanel) Debug.Log("Modal Panel not found");
            modalPanel.Prompt("Navigation Confirmation", "Confirm Navigation.\nCost: " + cost.ToString() + "ms",
                () => { gameController.Transition(gameObject, cost); ExploreNode(); }, () => { });
        }
        else { gameController.Transition(gameObject, cost); ExploreNode(); }//TODO: SET APPROPRIATE COST
    }

    public void ExploreNode()
    {
        if (!isVisited)
        {
            isVisited = true;
            foreach (GameObject child in outboundNodes.Keys)
            {
                BrainNode childScript = child.GetComponent<BrainNode>();
                if (!childScript.isExplored)
                {
                    treeNode.AddChild(child);
                    childScript.isExplored = true;
                    childScript.Refresh();
                }
            }
            treeNode.Expand();
        }
        Refresh();
    }

    public static Dictionary<GameObject, int> FindCost(GameObject source)
    {
        ArrayList nodes = new ArrayList();
        Dictionary<GameObject, int> distance = new Dictionary<GameObject, int>();
        Dictionary<GameObject, bool> marked = new Dictionary<GameObject, bool>();
        GameObject[] allNodes = GameController.getInstance().allBrainNodes;
        foreach (GameObject node in allNodes) { distance[node] = -1; marked[node] = false; } //Initialize the "graph"

        distance[source] = 0;
        marked[source] = true;
        nodes.Add(source);
        while (nodes.Count > 0)
        {
            GameObject node = (GameObject)nodes[0];
            nodes.Remove(node);
            foreach (GameObject outboundNode in node.GetComponent<BrainNode>().outboundNodes.Keys)
            {
                BrainNode outboundBrainNode = outboundNode.GetComponent<BrainNode>();
                if (!marked[outboundNode])
                {
                    distance[outboundNode] = distance[node] + node.GetComponent<BrainNode>().outboundNodes[outboundNode];
                    marked[outboundNode] = true;
                    nodes.Add(outboundNode);
                }
            }
        }
        return distance;
    }

}
