using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainNode : MonoBehaviour
{
    public bool isActive; //Determine whether this node is currently selected

    public GameObject[] outboundNodes; //List of nodes that are connected to this one

    // private CanvasGroup canvasGroup; //Useful to control alpha of the element
    private Button button;
    private GameController gameController; //Reference to gameController (singleton)
    // Use this for initialization
    void Start()
    {
        //canvasGroup = GetComponent<CanvasGroup>(); //Initialize member variables
        gameController = GameController.getInstance();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickBrainNode); //Add an onClick listener 
    }

    // Update is called once per frame
    void Update()
    {
        //If this node is active, pulse by changing alpha
        if (isActive)
        {
            button.colors.normalColor = Color.green;
            //canvasGroup.alpha = Mathf.PingPong(Time.time, 1);

        }
        else
            button.colors.normalColor = Color.black;
            //canvasGroup.alpha = 1;
    }

    //On Click - notify gameController using the Transition method
    void OnClickBrainNode()
    {
        gameController.Transition(gameObject);
    }
}
