using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetBrainState : MonoBehaviour {

    public BrainNode.AnimationChoice brainState;
    private GameController gameController;
    // Use this for initialization
    void Start () {
        Button button = GetComponent<Button>();
        if (!button)
        {
            Debug.Log("SetBrainState has to be attached to a button");
            return;
        }
        button.onClick.AddListener(OnClick);

        gameController = GameController.getInstance();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnClick()
    {
        gameController.SetBrainAnimation(brainState);
    }

}
