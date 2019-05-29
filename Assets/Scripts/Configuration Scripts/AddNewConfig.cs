using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddNewConfig : MonoBehaviour
{
    [Header("Config Name Inputfield")]
    public InputField configNameInputField;

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
    public Button createConfigButton;
    public string sceneToLoadAfterConfigCreation;

    [Header("Modal Panel")]
    public ModalPanel modalPanel;

    private List<InputField> inputFieldsList = new List<InputField>();

    void Start()
    {
        //Calls the TaskOnClick method when you click the Button
        createConfigButton.onClick.AddListener(TaskOnClick);

        // Adds all input fields to a list
        MakeListOfInputFields();
    }

    private void TaskOnClick()
    {

        CreateConfig();
    }

    private void CreateConfig()
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
            SetConfigFilePath();
            SetZeroMqSettings();
            SetOriginOffsetSettings();
            SetOcclusionSettings();

            SettingsManager.Instance.InitNewXmlFile(SettingsManager.Instance.ConfigFilePath);
            SettingsManager.Instance.WriteToXml(SettingsManager.Instance.ConfigFilePath);

            Debug.Log("New config file created at " + SettingsManager.Instance.ConfigFilePath);

            // Set the scene back to the settings selector scene
            SceneManager.LoadScene(sceneToLoadAfterConfigCreation, LoadSceneMode.Single);
        }
        

        //if (!SettingsManager.Instance.XmlConfigFiles.Contains(SettingsManager.Instance.XmlFilePath))
        //{
        //    SettingsManager.Instance.CreateBasicXmlFile(SettingsManager.Instance.XmlFilePath);
        //    SettingsManager.Instance.SaveXmlAt(SettingsManager.Instance.XmlFilePath);
        //}
        //else
        //{
        //    Debug.Log("There is already a config file called: " + SettingsManager.Instance.ConfigFileName);
        //}

    }

    private void SetConfigFilePath()
    {
        SettingsManager.Instance.ConfigFileName = configNameInputField.text;
        SettingsManager.Instance.ConfigFilePath = Application.persistentDataPath + "/" + SettingsManager.Instance.ConfigFileName + ".xml";
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
        inputFieldsList.Add(configNameInputField);

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
