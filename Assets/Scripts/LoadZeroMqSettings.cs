using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class LoadZeroMqSettings : MonoBehaviour
{
    public InputField serverAdressInputField;
    public InputField serverPortInputField;
    public InputField serverTopicInputField;

    public Button saveButton;

    private void Start()
    {
        GetZeroMqSettings();

        //Calls the TaskOnClick method when you click the Button
        saveButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SettingsManager.Instance.serverIp = serverAdressInputField.text;
        SettingsManager.Instance.serverPort = serverPortInputField.text;
        SettingsManager.Instance.serverTopic = serverTopicInputField.text;

        SettingsManager.Instance.SaveXml();
    }

    private void GetZeroMqSettings()
    {
        serverAdressInputField.text = SettingsManager.Instance.serverIp;
        serverPortInputField.text = SettingsManager.Instance.serverPort;
        serverTopicInputField.text = SettingsManager.Instance.serverTopic;
    }
}
