using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class HandleZeroMqSettings : MonoBehaviour
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
        SaveZeroMqSettings();
    }

    private void GetZeroMqSettings()
    {
        serverAdressInputField.text = SettingsManager.Instance.ServerIp;
        serverPortInputField.text = SettingsManager.Instance.ServerPort;
        serverTopicInputField.text = SettingsManager.Instance.ServerTopic;
    }

    private void SaveZeroMqSettings()
    {
        SettingsManager.Instance.ServerIp = serverAdressInputField.text;
        SettingsManager.Instance.ServerPort = serverPortInputField.text;
        SettingsManager.Instance.ServerTopic = serverTopicInputField.text;

        SettingsManager.Instance.SaveXml();
    }
}
