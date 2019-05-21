using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingCreatePanel : SettingPanel
{
    [Header("Config Name Inputfield")]
    public InputField configNameInputField;

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
            SaveZeroMqSettings();
            SaveOriginOffsetSettings();
            SaveOcclusionSettings();

            SettingsManager.Instance.CreateBasicXmlFile(SettingsManager.Instance.ConfigFilePath);
            SettingsManager.Instance.WriteToXml(SettingsManager.Instance.ConfigFilePath);

            Debug.Log("New config file created at " + SettingsManager.Instance.ConfigFilePath);

            // Set the scene back to the settings selector scene
            SceneManager.LoadScene(sceneToLoadAfterConfigCreation, LoadSceneMode.Single);
        }
    }

    public override void SetConfigFilePath()
    {
        SettingsManager.Instance.ConfigFileName = configNameInputField.text;
        SettingsManager.Instance.ConfigFilePath = Application.persistentDataPath + "/" + SettingsManager.Instance.ConfigFileName + ".xml";
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
