using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class XmlConfigFile : MonoBehaviour
{

    // The name of the current config file
    private string _configFileName;


    // Properties
    public string XmlFilePath { get; }

    public string ConfigFileName
    {
        get { return _configFileName; }
        set
        {
            if (value.Contains(" "))
            {
                value.Replace(" ", "_");
            }
            _configFileName = value;
        }
    }

    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }

    public float RotW { get; set; }
    public float RotX { get; set; }
    public float RotY { get; set; }
    public float RotZ { get; set; }

    public string ServerIp { get; set; }
    public string ServerPort { get; set; }
    public string ServerTopic { get; set; }

    public bool IsOccluded { get; set; }



    public XmlConfigFile(string xmlFilePath, string configFileName, float posX, float posY, float posZ, float rotW, float rotX, float rotY, float rotZ, string serverIp, string serverPort, string serverTopic, bool IsOccluded)
    {
        XmlFilePath = xmlFilePath;
        _configFileName = configFileName;

        PosX = posX;
        PosY = posY;
        PosZ = posZ;

        RotW = rotW;
        RotX = rotX;
        RotY = rotY;
        RotZ = rotZ;

        ServerIp = serverIp;
        ServerPort = serverPort;
        ServerTopic = serverTopic;

        this.IsOccluded = IsOccluded;


        
    }


    
}
