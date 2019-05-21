using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [Header("ZeroMQ Server Input Fields")]
    public InputField serverIpInputField;
    public InputField serverPortInputField;
    public InputField serverTopicInputField;

    [Header("Position Input Fields")]
    public InputField positionXInputField;
    public InputField positionYInputField;
    public InputField positionZInputField;

    [Header("Rotation Input Fields")]
    public InputField rotationXInputField;
    public InputField rotationYInputField;
    public InputField rotationZInputField;
    public InputField rotationWInputField;

    [Header("Occlusion Checkbox")]
    public Toggle occlusionToggle;


    public virtual void SetConfigFilePath()
    {

    }

    protected void SaveSettings()
    {
        SaveZeroMqSettings();
        SaveOriginOffsetSettings();
        SaveOcclusionSettings();
    }

    protected void SaveZeroMqSettings()
    {
        SettingsManager.Instance.ServerIp = serverIpInputField.text;
        SettingsManager.Instance.ServerPort = serverPortInputField.text;
        SettingsManager.Instance.ServerTopic = serverTopicInputField.text;
    }

    protected void SaveOriginOffsetSettings()
    {
        SettingsManager.Instance.PosX = float.Parse(positionXInputField.text);
        SettingsManager.Instance.PosY = float.Parse(positionYInputField.text);
        SettingsManager.Instance.PosZ = float.Parse(positionZInputField.text);

        SettingsManager.Instance.RotX = float.Parse(rotationXInputField.text);
        SettingsManager.Instance.RotY = float.Parse(rotationYInputField.text);
        SettingsManager.Instance.RotZ = float.Parse(rotationZInputField.text);
        SettingsManager.Instance.RotW = float.Parse(rotationWInputField.text);
    }

    protected void SaveOcclusionSettings()
    {
        SettingsManager.Instance.IsOccluded = occlusionToggle.isOn;
    }
}
