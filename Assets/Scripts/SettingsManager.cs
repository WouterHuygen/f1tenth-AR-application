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
    private string xmlFilePath;

    // position values to work with the xml file.

    public float posX { get; set; }
    public float posY { get; set; }
    public float posZ { get; set; }

    // rotation values to work with the xml file.
    public float rotW { get; set; }
    public float rotX { get; set; }
    public float rotY { get; set; }
    public float rotZ { get; set; }

    // server variables to work with the xml file
    public string serverIp { get; set; }
    public string serverPort { get; set; }
    public string serverTopic { get; set; }

    // Other settings variables to work with the xml file
    public bool IsOccluded { get; set; }

    private void Start()
    {  
        InitXmlFile();
    }



    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void InitXmlFile()
    {
        // Set xml file location and create a new xml file if there isn't one
        xmlFilePath = Application.persistentDataPath + @"/AR-F1TENTH-SETTINGS.xml";

        if (!File.Exists(xmlFilePath))
        {
            CreateXml();
            Debug.Log("XML settings file created at " + xmlFilePath);
        }
        else
        {
            LoadZeroMqSettingsFromXml();
            LoadOriginOffsetSettingsFromXml();
            LoadOtherSettingsFromXml();
            Debug.Log("XML settings loaded from " + xmlFilePath);
        }
    }

    private void CreateXml()
    {
        try
        {
            File.Create(xmlFilePath).Dispose(); // Break the stream with file immediately after file creation
            try
            {
                using (StreamWriter sW = new StreamWriter(xmlFilePath))
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

    private void LoadZeroMqSettingsFromXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(xmlFilePath);

        XmlNodeList zeroMqSettingsList = xmlDoc.GetElementsByTagName("zeroMq");

        foreach (XmlNode zeroMqSetting in zeroMqSettingsList)
        {
            XmlNodeList settingContent = zeroMqSetting.ChildNodes;

            foreach (XmlNode settingItem in settingContent)
            {

                if (settingItem.Name == "serverAddress")
                {
                    serverIp = settingItem.InnerText;
                }
                else if (settingItem.Name == "serverPort")
                {
                    serverPort = settingItem.InnerText;
                }
                else if (settingItem.Name == "serverTopic")
                {
                    serverTopic = settingItem.InnerText;
                }
            }
        }
        
    }

    private void LoadOriginOffsetSettingsFromXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(xmlFilePath);

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
                            posX = float.Parse(positionItem.InnerText);
                        }
                        else if (positionItem.Name == "y")
                        {
                            posY = float.Parse(positionItem.InnerText);
                        }
                        else if (positionItem.Name == "z")
                        {
                            posZ = float.Parse(positionItem.InnerText);
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
                            rotX = float.Parse(rotationItem.InnerText);
                        }
                        else if (rotationItem.Name == "y")
                        {
                            rotY = float.Parse(rotationItem.InnerText);
                        }
                        else if (rotationItem.Name == "z")
                        {
                            rotZ = float.Parse(rotationItem.InnerText);
                        }
                        else if (rotationItem.Name == "w")
                        {
                            rotW = float.Parse(rotationItem.InnerText);
                        }
                    }
                }
            }
        }
    }

    private void LoadOtherSettingsFromXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        xmlDoc.Load(xmlFilePath);

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

    public void SaveXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(xmlFilePath))
        {
            try
            {
                xmlDoc.Load(xmlFilePath);
                try
                {
                    XmlElement elmRoot = xmlDoc.DocumentElement;

                    // cleanup existing elements inside Root
                    elmRoot.RemoveAll();


                    // create zeroMQ element
                    XmlElement elmZeroMq = xmlDoc.CreateElement("zeroMq");

                    // create server address element for ZeroMQ
                    XmlElement elmServerAddress = xmlDoc.CreateElement("serverAddress");
                    elmServerAddress.InnerText = serverIp;

                    // create server port element for ZeroMQ
                    XmlElement elmServerPort = xmlDoc.CreateElement("serverPort");
                    elmServerPort.InnerText = serverPort;

                    // create server topic element for ZeroMQ
                    XmlElement elmServerTopic = xmlDoc.CreateElement("serverTopic");
                    elmServerTopic.InnerText = serverTopic;


                    // create origin position offset element
                    XmlElement elmOriginOffset = xmlDoc.CreateElement("originOffset");

                    // create position element w/ values
                    XmlElement elmPosition = xmlDoc.CreateElement("position");

                    XmlElement elmPosX = xmlDoc.CreateElement("x");
                    elmPosX.InnerText = posX.ToString();

                    XmlElement elmPosY = xmlDoc.CreateElement("y");
                    elmPosY.InnerText = posY.ToString();

                    XmlElement elmPosZ = xmlDoc.CreateElement("z");
                    elmPosZ.InnerText = posZ.ToString();

                    // create rotation element w/ values
                    XmlElement elmRotation = xmlDoc.CreateElement("rotation");

                    XmlElement elmRotW = xmlDoc.CreateElement("w");
                    elmRotW.InnerText = rotW.ToString();

                    XmlElement elmRotX = xmlDoc.CreateElement("x");
                    elmRotX.InnerText = rotX.ToString();

                    XmlElement elmRotY = xmlDoc.CreateElement("y");
                    elmRotY.InnerText = rotY.ToString();

                    XmlElement elmRotZ = xmlDoc.CreateElement("z");
                    elmRotZ.InnerText = rotZ.ToString();


                    // create other settings element
                    XmlElement elmOtherSettings = xmlDoc.CreateElement("otherSettings");

                    XmlElement elmOcclusion = xmlDoc.CreateElement("IsOccluded");
                    elmOcclusion.InnerText = IsOccluded.ToString();

                    // append childs
                    elmRoot.AppendChild(elmOriginOffset);
                    elmRoot.AppendChild(elmZeroMq);
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
                    xmlDoc.Save(xmlFilePath);
                }
                catch (IOException ex)
                {
                    Debug.Log("Error writing element to XML : " + ex.TargetSite);
                }
            }
            catch (IOException ex)
            {
                Debug.Log("Error loading xml file : " + ex.TargetSite);
            }
        }
    }



}
