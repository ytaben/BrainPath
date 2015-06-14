using UnityEngine;
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
    void Awake()
    {
        
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBrainNode); //Add an onClick listener 
        outboundNodes = new System.Collections.Generic.Dictionary<GameObject, int>();
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
            if (!brainNode.destination.activeSelf) { 
            brainNode.destination.SetActive(true);
            BrainNode script = brainNode.destination.GetComponent<BrainNode>();
            script.isNew = true; script.isExplored = true; script.Refresh();
                Refresh();
        }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetColor(Color.white);
        SetMaterial(materialController.highlitedMaterial);
        gameController.SetBrainAnimation(BrainState);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameController.SetCurrentAnimation();
        Refresh();
    }
    
    private void SetColor(Color color)
    {
        ColorBlock buttonColors;
        buttonColors = button.colors;
        buttonColors.normalColor = color;
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
    void OnClickBrainNode()
    {
        Refresh();
        if (isActive) { nodeCanvas.gameObject.SetActive(enabled); Debug.Log("gotHere"); return; }
        Dictionary<GameObject, int> costs = FindCost(gameObject);
        gameController.Transition(gameObject, costs[gameController.activeNode]); //TODO: SET APPROPRIATE COST
    }

    public static Dictionary<GameObject, int> FindCost(GameObject source)
    {
        ArrayList nodes = new ArrayList();
        Dictionary<GameObject, int> distance = new Dictionary<GameObject, int>();
        Dictionary<GameObject, bool> marked = new Dictionary<GameObject, bool>();
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag("BrainNode");
        foreach (GameObject node in allNodes) { distance[node] = -1; marked[node] = false;} //Initialize the "graph"

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
                    distance[outboundNode] = distance[node] + 1;
                    marked[outboundNode] = true;
                    nodes.Add(outboundNode);
                }
                nodes.Add(node);
            }
        }
        return distance;
    }
    
}
