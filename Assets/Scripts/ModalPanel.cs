using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Reference: https://unity3d.com/learn/tutorials/modules/intermediate/live-training-archive/modal-window
public class ModalPanel : MonoBehaviour {

    public Text question;
    public Image iconImage;
    public Button button1;
    public Button button2;
    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;


    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
            {
                Debug.LogError("No active ModalPanel script");
            }
        }

        return modalPanel;
    }

    public void Choice(string question, string button1text, string button2text, UnityAction button1Event, UnityAction button2Event)
    {
        modalPanelObject.SetActive(true);

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(button1Event);
        button1.onClick.AddListener(ClosePanel);

        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(button2Event);
        button2.onClick.AddListener(ClosePanel);

        this.question.text = question;
        button1.GetComponentInChildren<Text>().text = button1text;
        button2.GetComponentInChildren<Text>().text = button2text;

        this.iconImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
    }

    void ClosePanel ()
    {
        modalPanelObject.SetActive(false);
    }
}
