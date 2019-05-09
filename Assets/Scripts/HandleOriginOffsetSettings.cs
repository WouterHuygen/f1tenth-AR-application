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
        positionXInputField.text = SettingsManager.Instance.PosX.ToString();
        positionYInputField.text = SettingsManager.Instance.PosY.ToString();
        positionZInputField.text = SettingsManager.Instance.PosZ.ToString();

        rotationXInputField.text = SettingsManager.Instance.RotX.ToString();
        rotationYInputField.text = SettingsManager.Instance.RotY.ToString();
        rotationZInputField.text = SettingsManager.Instance.RotZ.ToString();
        rotationWInputField.text = SettingsManager.Instance.RotW.ToString();
    }

    private void SaveOriginOffsetSettings()
    {
        SettingsManager.Instance.PosX = float.Parse(positionXInputField.text);
        SettingsManager.Instance.PosY = float.Parse(positionYInputField.text);
        SettingsManager.Instance.PosZ = float.Parse(positionZInputField.text);

        SettingsManager.Instance.RotX = float.Parse(rotationXInputField.text);
        SettingsManager.Instance.RotY = float.Parse(rotationYInputField.text);
        SettingsManager.Instance.RotZ = float.Parse(rotationZInputField.text);
        SettingsManager.Instance.RotW = float.Parse(rotationWInputField.text);

        SettingsManager.Instance.SaveXml();
    }
}
