using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagerInitializer : MonoBehaviour
{
    void Start()
    {
        SettingsManager.Instance.IsInitialized = true;
    }
}
