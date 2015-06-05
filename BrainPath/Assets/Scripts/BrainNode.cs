using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainNode : MonoBehaviour {
    public bool isActive;

    public GameObject[] outboundNodes;

    private CanvasGroup canvasGroup;
    private GameController gameController;
    // Use this for initialization
    void Start () {
        canvasGroup = GetComponent<CanvasGroup>();
        gameController = GameController.getInstance();
        GetComponent<Button>().onClick.AddListener(OnClickBrainNode);
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            canvasGroup.alpha = Mathf.PingPong(Time.time, 1);
        }
        else
            canvasGroup.alpha = 1;
    }

    void OnClickBrainNode()
    {

    }
}
