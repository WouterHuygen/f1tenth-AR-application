using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SettingsManager : Singleton<SettingsManager>
{
    // Prevent non-singleton constructor use.
    protected SettingsManager() { }

    //The save location of the XML
    public string ConfigFilePath { get; set; }

    // The name of the current config file
    public string ConfigFileName { get; set; }

    // List of all XML config file paths
    private List<string>xmlConfigFilePaths = new List<string>();
    // Property for the config file path list
    public List<string> XmlConfigFilePaths  { get { return xmlConfigFilePaths; } private set { } }

    // List of all XML config files
    private List<string>xmlConfigFileNames = new List<string>();
    // Property for the config file list
    public List<string> XmlConfigFileNames { get { return xmlConfigFileNames; } private set { } }

    // position values to work with the xml file.

    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }

    // rotation values to work with the xml file.
    public float RotW { get; set; }
    public float RotX { get; set; }
    public float RotY { get; set; }
    public float RotZ { get; set; }

    // server variables to work with the xml file
    public string ServerIp { get; set; }
    public string ServerPort { get; set; }
    public string ServerTopic { get; set; }

    // Other settings variables to work with the xml file
    public bool IsOccluded { get; set; }

    // A boolean to check if the SceneManager was initialized
    public bool IsInitialized { get; set; }

    private void Start()
    {
        SetupSettingsManager();
    }

    private void SetupSettingsManager()
    {
        CheckForConfigFiles();
        InitXmlFile();
    }

    private void InitXmlFile()
    {
        // Set xml file location to the standard setting file
        ConfigFilePath = Application.persistentDataPath + @"/thebeacon_config_1.xml";

        // Create a new xml file if there isn't one
        if (!File.Exists(ConfigFilePath))
        {
            CreateBasicXmlFile(ConfigFilePath);
            WriteToXml(ConfigFilePath);
            Debug.Log("XML settings file created at " + ConfigFilePath);
        }
        // Else load the excisting file
        else
        {
            LoadXmlFile(ConfigFilePath);
            Debug.Log("XML settings loaded from " + ConfigFilePath);
        }
    }

    public void CreateBasicXmlFile(string filePath)
    {
        try
        {
            File.Create(filePath).Dispose(); // Break the stream with file immediately after file creation
            try
            {
                using (StreamWriter sW = new StreamWriter(filePath))
                { // Initializing XML file
                    sW.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sW.WriteLine("<dataRoot>");
                    sW.WriteLine("</dataRoot>");
                }
            }
            catch (IOException ex)
            {
                Debug.Log("Error setting up root element for XML : " + ex.TargetSite);
            }
        }
        catch (IOException ex)
        {
            Debug.Log("Error creating xml file : " + ex.TargetSite);
        }
    }

    public void LoadXmlFile(string filePath)
    {
        ConfigFileName = LoadConfigFileNameFromXml(filePath);
        LoadZeroMqConfigFromXml(filePath);
        LoadOriginOffsetConfigFromXml(filePath);
        LoadOcclusionConfigFromXml(filePath);
    }

    private string LoadConfigFileNameFromXml(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(filePath);

        XmlNodeList elemList = xmlDoc.GetElementsByTagName("configFileName");

        string configName = elemList[0].InnerText;

        return configName;
    }

    private void LoadZeroMqConfigFromXml(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(filePath);

        XmlNodeList zeroMqSettingsList = xmlDoc.GetElementsByTagName("zeroMq");

        foreach (XmlNode zeroMqSetting in zeroMqSettingsList)
        {
            XmlNodeList settingContent = zeroMqSetting.ChildNodes;

            foreach (XmlNode settingItem in settingContent)
            {

                if (settingItem.Name == "serverAddress")
                {
                    ServerIp = settingItem.InnerText;
                }
                else if (settingItem.Name == "serverPort")
                {
                    ServerPort = settingItem.InnerText;
                }
                else if (settingItem.Name == "serverTopic")
                {
                    ServerTopic = settingItem.InnerText;
                }
            }
        }
    }

    private void LoadOriginOffsetConfigFromXml(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(filePath);

        XmlNodeList originOffsetSettingsList = xmlDoc.GetElementsByTagName("originOffset");

        foreach (XmlNode OriginOffsetSetting in originOffsetSettingsList)
        {
            XmlNodeList settingContent = OriginOffsetSetting.ChildNodes;

            foreach (XmlNode settingItem in settingContent)
            {

                if (settingItem.Name == "position")
                {
                    XmlNodeList positionContent = settingItem.ChildNodes;

                    foreach (XmlNode positionItem in positionContent)
                    {
                        if (positionItem.Name == "x")
                        {
                            PosX = float.Parse(positionItem.InnerText);
                        }
                        else if (positionItem.Name == "y")
                        {
                            PosY = float.Parse(positionItem.InnerText);
                        }
                        else if (positionItem.Name == "z")
                        {
                            PosZ = float.Parse(positionItem.InnerText);
                        }
                    }
                }
                else if (settingItem.Name == "rotation")
                {
                    XmlNodeList rotationContent = settingItem.ChildNodes;

                    foreach (XmlNode rotationItem in rotationContent)
                    {
                        if (rotationItem.Name == "x")
                        {
                            RotX = float.Parse(rotationItem.InnerText);
                        }
                        else if (rotationItem.Name == "y")
                        {
                            RotY = float.Parse(rotationItem.InnerText);
                        }
                        else if (rotationItem.Name == "z")
                        {
                            RotZ = float.Parse(rotationItem.InnerText);
                        }
                        else if (rotationItem.Name == "w")
                        {
                            RotW = float.Parse(rotationItem.InnerText);
                        }
                    }
                }
            }
        }
    }

    private void LoadOcclusionConfigFromXml(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(filePath);

        XmlNodeList otherSettingsList = xmlDoc.GetElementsByTagName("otherSettings");

        foreach (XmlNode otherSetting in otherSettingsList)
        {
            XmlNodeList settingContent = otherSetting.ChildNodes;

            foreach (XmlNode settingItem in settingContent)
            {

                if (settingItem.Name == "IsOccluded")
                {
                    string isOccluded = IsOccluded.ToString();
                    isOccluded = settingItem.InnerText;
                }
            }
        }
    }


    public void WriteToXml(string filePath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(filePath))
        {
            try
            {
                xmlDoc.Load(filePath);
                try
                {
                    XmlElement elmRoot = xmlDoc.DocumentElement;

                    // cleanup existing elements inside Root
                    elmRoot.RemoveAll();

                    // create config file name
                    XmlElement elmConfigFileName = xmlDoc.CreateElement("configFileName");
                    elmConfigFileName.InnerText = ConfigFileName;

                    // create zeroMQ element
                    XmlElement elmZeroMq = xmlDoc.CreateElement("zeroMq");

                    // create server address element for ZeroMQ
                    XmlElement elmServerAddress = xmlDoc.CreateElement("serverAddress");
                    elmServerAddress.InnerText = ServerIp;

                    // create server port element for ZeroMQ
                    XmlElement elmServerPort = xmlDoc.CreateElement("serverPort");
                    elmServerPort.InnerText = ServerPort;

                    // create server topic element for ZeroMQ
                    XmlElement elmServerTopic = xmlDoc.CreateElement("serverTopic");
                    elmServerTopic.InnerText = ServerTopic;


                    // create origin position offset element
                    XmlElement elmOriginOffset = xmlDoc.CreateElement("originOffset");

                    // create position element w/ values
                    XmlElement elmPosition = xmlDoc.CreateElement("position");

                    XmlElement elmPosX = xmlDoc.CreateElement("x");
                    elmPosX.InnerText = PosX.ToString();

                    XmlElement elmPosY = xmlDoc.CreateElement("y");
                    elmPosY.InnerText = PosY.ToString();

                    XmlElement elmPosZ = xmlDoc.CreateElement("z");
                    elmPosZ.InnerText = PosZ.ToString();

                    // create rotation element w/ values
                    XmlElement elmRotation = xmlDoc.CreateElement("rotation");

                    XmlElement elmRotW = xmlDoc.CreateElement("w");
                    elmRotW.InnerText = RotW.ToString();

                    XmlElement elmRotX = xmlDoc.CreateElement("x");
                    elmRotX.InnerText = RotX.ToString();

                    XmlElement elmRotY = xmlDoc.CreateElement("y");
                    elmRotY.InnerText = RotY.ToString();

                    XmlElement elmRotZ = xmlDoc.CreateElement("z");
                    elmRotZ.InnerText = RotZ.ToString();


                    // create other settings element
                    XmlElement elmOtherSettings = xmlDoc.CreateElement("otherSettings");

                    XmlElement elmOcclusion = xmlDoc.CreateElement("IsOccluded");
                    elmOcclusion.InnerText = IsOccluded.ToString();

                    // append children
                    elmRoot.AppendChild(elmConfigFileName);
                    elmRoot.AppendChild(elmZeroMq);
                    elmRoot.AppendChild(elmOriginOffset);
                    elmRoot.AppendChild(elmOtherSettings);

                    elmZeroMq.AppendChild(elmServerAddress);
                    elmZeroMq.AppendChild(elmServerPort);
                    elmZeroMq.AppendChild(elmServerTopic);

                    elmOriginOffset.AppendChild(elmPosition);
                    elmOriginOffset.AppendChild(elmRotation);

                    elmOtherSettings.AppendChild(elmOcclusion);

                    elmPosition.AppendChild(elmPosX);
                    elmPosition.AppendChild(elmPosY);
                    elmPosition.AppendChild(elmPosZ);

                    elmRotation.AppendChild(elmRotW);
                    elmRotation.AppendChild(elmRotX);
                    elmRotation.AppendChild(elmRotY);
                    elmRotation.AppendChild(elmRotZ);

                    // save file
                    xmlDoc.Save(filePath);
                }
                catch (IOException ex)
                {
                    Debug.Log("Error writing element to XML : " + ex.TargetSite);
                }
            }
            catch (IOException ex)
            {
                Debug.Log("Error, no xml file exists at this location : " + ex.TargetSite);
            }
        }
    }

    // Checks for excisting config files in the persistent data path of the application 
    public void CheckForConfigFiles()
    {
        // Adds all config file paths and names to a list
        foreach (string file in System.IO.Directory.GetFiles(Application.persistentDataPath))
        {
            if (!xmlConfigFilePaths.Contains(file) && file.EndsWith(".xml") || file.EndsWith(".XML"))
            {
                xmlConfigFilePaths.Add(file);
                xmlConfigFileNames.Add(LoadConfigFileNameFromXml(file));
            }
        }
    }
}
