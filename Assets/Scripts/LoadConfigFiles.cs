using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadConfigFiles : MonoBehaviour
{
    public GameObject content;

    public Button configButtonPrefab;

    private void Start()
    {
        SettingsManager.Instance.CheckForConfigFiles();
        CreateConfigButtons();
    }

    private void CreateConfigButtons()
    {
        for (int i = 0; i < SettingsManager.Instance.XmlConfigFilePaths.Count; i++)
        {
            Button _configButton = (Button)Instantiate(configButtonPrefab);
            _configButton.GetComponentInChildren<Text>().text = SettingsManager.Instance.XmlConfigFilePaths[i];
            _configButton.transform.SetParent(content.transform, false);


        }
    }
}
