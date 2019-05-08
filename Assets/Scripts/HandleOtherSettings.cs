using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleOtherSettings : MonoBehaviour
{
    public Toggle occlusionToggle;

    public Button saveButton;

    private void Start()
    {
        GetOtherSettings();

        //Calls the TaskOnClick method when you click the Button
        saveButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SaveOtherSettings();
    }

    private void GetOtherSettings()
    {
        occlusionToggle.isOn = SettingsManager.Instance.IsOccluded;
    }

    private void SaveOtherSettings()
    {
        SettingsManager.Instance.IsOccluded = occlusionToggle.isOn;
        

        SettingsManager.Instance.SaveXml();
    }
}
