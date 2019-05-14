using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddNewSettings : MonoBehaviour
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

        CreateNewConfig();
    }

    private void CreateNewConfig()
    {
        if (InputIsEmpty())
        {
            modalPanel.SetAlertMessage("Please fill in all configuration fields");
        }
        else
        {
            SetConfigFilePath();
            SaveZeroMqSettings();
            SaveOriginOffsetSettings();
            SaveOcclusionSettings();

            SettingsManager.Instance.CreateBasicXmlFile(SettingsManager.Instance.XmlFilePath);
            SettingsManager.Instance.SaveXmlAt(SettingsManager.Instance.XmlFilePath);

            Debug.Log("New config file created at " + SettingsManager.Instance.XmlFilePath);

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
        SettingsManager.Instance.XmlFilePath = Application.persistentDataPath + "/" + SettingsManager.Instance.ConfigFileName + ".xml";
    }

    private void SaveZeroMqSettings()
    {
        SettingsManager.Instance.ServerIp = serverIpInputField.text;
        SettingsManager.Instance.ServerPort = serverPortInputField.text;
        SettingsManager.Instance.ServerTopic = serverTopicInputField.text;
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
    }

    private void SaveOcclusionSettings()
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
