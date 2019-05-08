using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleOriginOffsetSettings : MonoBehaviour
{
    [Header("Position Input Fields")]
    public InputField positionXInputField;
    public InputField positionYInputField;
    public InputField positionZInputField;

    [Header("Rotation Input Fields")]
    public InputField rotationXInputField;
    public InputField rotationYInputField;
    public InputField rotationZInputField;
    public InputField rotationWInputField;

    [Header("Other")]
    public Button saveButton;

    private void Start()
    {
        GetOriginOffsetSettings();

        //Calls the TaskOnClick method when you click the Button
        saveButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SaveOriginOffsetSettings();
    }

    private void GetOriginOffsetSettings()
    {
        positionXInputField.text = SettingsManager.Instance.posX.ToString();
        positionYInputField.text = SettingsManager.Instance.posY.ToString();
        positionZInputField.text = SettingsManager.Instance.posZ.ToString();

        rotationXInputField.text = SettingsManager.Instance.rotX.ToString();
        rotationYInputField.text = SettingsManager.Instance.rotY.ToString();
        rotationZInputField.text = SettingsManager.Instance.rotZ.ToString();
        rotationWInputField.text = SettingsManager.Instance.rotW.ToString();
    }

    private void SaveOriginOffsetSettings()
    {
        SettingsManager.Instance.posX = float.Parse(positionXInputField.text);
        SettingsManager.Instance.posY = float.Parse(positionYInputField.text);
        SettingsManager.Instance.posZ = float.Parse(positionZInputField.text);

        SettingsManager.Instance.rotX = float.Parse(rotationXInputField.text);
        SettingsManager.Instance.rotY = float.Parse(rotationYInputField.text);
        SettingsManager.Instance.rotZ = float.Parse(rotationZInputField.text);
        SettingsManager.Instance.rotW = float.Parse(rotationWInputField.text);

        SettingsManager.Instance.SaveXml();
    }
}
