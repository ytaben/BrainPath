using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalPanel : MonoBehaviour {

    public Text titleText;
    public Text messageText;
    public Button okButton;
    public Button cancelButton;
    public GameObject modalPanelObject;
    public GameObject brainBlock; //An object to block clicks on the brain


    //Make modal panel singleton
    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
        }
        if (!modalPanel) Debug.Log("Modal Panel Instance Not found");

        return modalPanel;
    }

    /// <summary>
    /// Prompt a modal panel with given title, messages and events
    /// </summary>
    /// <param name="title">Title to be displayed</param>
    /// <param name="message">Message to be displayed</param>
    /// <param name="okAction">Action to perform when Ok is clicked</param>
    /// <param name="cancelAction">Action to perform when Cancel is clicked</param>
    public void Prompt(string title, string message, UnityAction okAction, UnityAction cancelAction)
    {
        titleText.text = title;
        messageText.text = message;

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(okAction);
        okButton.onClick.AddListener(() => { modalPanelObject.SetActive(false); brainBlock.SetActive(false); });
        
        //Hack to proceed in tutorials
        GameObject cancelBlock = GameObject.Find("cancelBlock");
        if (cancelBlock) okButton.onClick.AddListener(() => cancelBlock.SetActive(false));

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelAction);
        cancelButton.onClick.AddListener(() => { modalPanelObject.SetActive(false); brainBlock.SetActive(false); });

        modalPanelObject.SetActive(true);
        brainBlock.SetActive(true);
    }
}
