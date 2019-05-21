using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditCurrentSettings : MonoBehaviour
{
    [Header("Config Name Text Field")]
    public Text configNameText;

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

    [Header("Navigation")]
    public Button editConfigButton;
    public string sceneToLoadAfterConfigEdit;

    [Header("Modal Panel")]
    public ModalPanel modalPanel;

    private List<InputField> inputFieldsList = new List<InputField>();

    void Start()
    {
        LoadConfigNameFromSettingsManager();
        LoadZeroMqSettingsFromSettingsManager();
        LoadOriginOffsetSettingsFromSettingsManager();
        LoadOcclusionSettingsFromSettingsManager();

        //Calls the TaskOnClick method when you click the Button
        editConfigButton.onClick.AddListener(TaskOnClick);

        // Adds all input fields to a list
        MakeListOfInputFields();
    }

    private void TaskOnClick()
    {

        EditConfig();
    }

    private void EditConfig()
    {
        if (InputIsEmpty())
        {
            modalPanel.SetAlertMessage("Please fill in all configuration fields");
        }
        // FIX WHEN we have a list of config names
        //else if (SettingsManager.Instance.XmlConfigFilePaths.Contains(Application.persistentDataPath + "/" + configNameInputField.text + ".xml"))
        //{
        //    modalPanel.SetAlertMessage("There is already a config file called: " + configNameInputField.text);
        //}
        else
        {
            SetZeroMqSettings();
            SetOriginOffsetSettings();
            SetOcclusionSettings();

            SettingsManager.Instance.WriteToXml(SettingsManager.Instance.ConfigFilePath);

            Debug.Log("File edited at " + SettingsManager.Instance.ConfigFilePath);

            // Set the scene back to the settings selector scene
            SceneManager.LoadScene(sceneToLoadAfterConfigEdit, LoadSceneMode.Single);
        }

    }

    private void SetZeroMqSettings()
    {
        SettingsManager.Instance.ServerIp = serverIpInputField.text;
        SettingsManager.Instance.ServerPort = serverPortInputField.text;
        SettingsManager.Instance.ServerTopic = serverTopicInputField.text;
    }

    private void SetOriginOffsetSettings()
    {
        SettingsManager.Instance.PosX = float.Parse(positionXInputField.text);
        SettingsManager.Instance.PosY = float.Parse(positionYInputField.text);
        SettingsManager.Instance.PosZ = float.Parse(positionZInputField.text);

        SettingsManager.Instance.RotX = float.Parse(rotationXInputField.text);
        SettingsManager.Instance.RotY = float.Parse(rotationYInputField.text);
        SettingsManager.Instance.RotZ = float.Parse(rotationZInputField.text);
        SettingsManager.Instance.RotW = float.Parse(rotationWInputField.text);
    }

    private void SetOcclusionSettings()
    {
        SettingsManager.Instance.IsOccluded = occlusionToggle.isOn;
    }

    private void LoadZeroMqSettingsFromSettingsManager()
    {
        serverIpInputField.text = SettingsManager.Instance.ServerIp;
        serverPortInputField.text = SettingsManager.Instance.ServerPort;
        serverTopicInputField.text = SettingsManager.Instance.ServerTopic;
    }

    private void LoadOriginOffsetSettingsFromSettingsManager()
    {
        positionXInputField.text = SettingsManager.Instance.PosX.ToString();
        positionYInputField.text = SettingsManager.Instance.PosY.ToString();
        positionZInputField.text = SettingsManager.Instance.PosZ.ToString();

        rotationXInputField.text = SettingsManager.Instance.RotX.ToString();
        rotationYInputField.text = SettingsManager.Instance.RotY.ToString();
        rotationZInputField.text = SettingsManager.Instance.RotZ.ToString();
        rotationWInputField.text = SettingsManager.Instance.RotW.ToString();
    }

    private void LoadOcclusionSettingsFromSettingsManager()
    {
        occlusionToggle.isOn = SettingsManager.Instance.IsOccluded;
    }

    private void LoadConfigNameFromSettingsManager()
    {
        configNameText.text = SettingsManager.Instance.ConfigFileName;
    }

    private bool InputIsEmpty()
    {
        bool IsEmpty = false;

        foreach (InputField inputField in inputFieldsList)
        {
            if (inputField.text == "")
            {
                IsEmpty = true;
                break;
            }
        }

        return IsEmpty;
    }

    private void MakeListOfInputFields()
    {
        inputFieldsList.Add(serverIpInputField);
        inputFieldsList.Add(serverPortInputField);
        inputFieldsList.Add(serverTopicInputField);

        inputFieldsList.Add(positionXInputField);
        inputFieldsList.Add(positionYInputField);
        inputFieldsList.Add(positionZInputField);

        inputFieldsList.Add(rotationXInputField);
        inputFieldsList.Add(rotationYInputField);
        inputFieldsList.Add(rotationZInputField);
        inputFieldsList.Add(rotationWInputField);
    }
}
