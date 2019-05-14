using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModalPanel : MonoBehaviour
{
    public Text alert;
    public Button okButton;
    public GameObject modalPanelObject;

    // To make sure there is only 1 active modal panel for the scene
    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There can only be one active ModalPanel script on the GameObject.");
        }

        return modalPanel;
    }

    public void SetAlertMessage(string alertMessage)
    {
        modalPanelObject.SetActive(true);

        okButton.onClick.AddListener(ClosePanel);

        this.alert.text = alertMessage;
    }

    void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}
