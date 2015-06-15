using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{

    public int correctState; //Number of the correct stage when this button should be pressed
    public int actionCost; //Cost of this action in milliseconds
    public bool isCorrect; //Specify whether this button is a correct answer and can advance the game
    public string earlyMessage; //Message to display if the button was presesd too early in the game
    public string correctMessage; //Message to display if the button was presesd at the right time
    public string LateMessage; //Message to display if the button was presesd too late in the game

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        //Keep an instance of GameController handy
        gameController = GameController.getInstance();

        //Attach OnClick delegate to the button's onClick
        Button button = GetComponent<Button>();
        if (button) { button.onClick.AddListener(OnClick); }
        else Debug.Log("ActionButton must be attached to a button!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    Use game controller to check the state then display the correct message.
    If this button can be used to advance the game, and pressed on the correct stage of the game - call gameController's method
    to advance the game.
    Also, gameController is always notified of the action to increase amount of time elapsed 
    */
    public void OnClick()
    {

    }
}
