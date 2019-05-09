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
        
    }

    private void CreateConfigButtons()
    {
        for (int i = 0; i < SettingsManager.Instance.XmlConfigFiles.Count; i++)
        {
            Button _configButton = (Button)Instantiate(configButtonPrefab);
            // WIP
            _configButton.transform.SetParent(content.transform, false);
        }
    }
}
