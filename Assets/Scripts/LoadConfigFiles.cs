using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadConfigFiles : MonoBehaviour
{
    public GameObject content;

    public Button configButtonPrefab;

    public string sceneToLoadAfterConfigSwitch;

    private void Start()
    {
        SettingsManager.Instance.CheckForExistingConfigFiles();
        CreateConfigButtons();
    }

    private void CreateConfigButtons()
    {
        for (int i = 0; i < SettingsManager.Instance.XmlConfigFilePaths.Count; i++)
        {
            Button _configButton = (Button)Instantiate(configButtonPrefab);
            _configButton.GetComponentInChildren<Text>().text = SettingsManager.Instance.XmlConfigFileNames[i];

            // This temporary string is needed because of the way delegates work with the singleton
            string tempFilePath = SettingsManager.Instance.XmlConfigFilePaths[i];

            _configButton.transform.SetParent(content.transform, false);
            _configButton.onClick.AddListener(delegate { SwitchConfig(tempFilePath); });
            Debug.Log(SettingsManager.Instance.XmlConfigFilePaths[i]);
        }
    }

    private void SwitchConfig(string configPath)
    {
        SettingsManager.Instance.LoadXmlFileToSettingsManager(configPath);
        Debug.Log(configPath + " loaded.");
        // Set the scene back to the settings selector scene
        SceneManager.LoadScene(sceneToLoadAfterConfigSwitch, LoadSceneMode.Single);
    }
}
