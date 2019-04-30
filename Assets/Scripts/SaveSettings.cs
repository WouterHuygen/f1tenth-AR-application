using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{

    public Button saveButton;

    public InputField serverAddressInputField;
    public InputField serverPortInputField;
    public InputField serverTopicInputField;

    //The save location of the XML
    private string xmlFilePath;

    // position values to work with the xml file.
    private string posX = "";
    private string posY = "";
    private string posZ = "";

    // rotation values to work with the xml file.
    private string rotW = ""; // Default always 1 for normal rotations
    private string rotX = "";
    private string rotY = "";
    private string rotZ = "";

    // server variables to work with the xml file
    private string serverAddress = "";
    private string serverPort = "";
    private string serverTopic = "";

    void Start()
    {
        // Set xml fiel location and create a new xml file if there isn't one
        xmlFilePath = Application.persistentDataPath + @"/AR-F1TENTH-SETTINGS.xml";
        if (!File.Exists(xmlFilePath))
        {
            CreateXML(xmlFilePath);
            Debug.Log("File created at " + xmlFilePath);
        }
        else
        {
            Debug.Log("File exists at " + xmlFilePath);
        }

        //Calls the TaskOnClick method when you click the Button
        saveButton.onClick.AddListener(TaskOnClick);

        
    }

    void TaskOnClick()
    {
        if (serverAddressInputField.text != null)
        {
            serverAddress = serverAddressInputField.text;
            serverPort = serverPortInputField.text;
            serverTopic = serverTopicInputField.text;
        }
        WriteToXml(xmlFilePath);
    }

    void CreateXML(string xmlFilePath)
    {
        try
        {
            File.Create(xmlFilePath).Dispose(); // Break the stream with file immediately after file creation
            Debug.Log("File created at: " + xmlFilePath);
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

    void WriteToXml(string xmlFilePath)
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
                    elmServerAddress.InnerText = serverAddress;

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
                    elmPosX.InnerText = posX;

                    XmlElement elmPosY = xmlDoc.CreateElement("y");
                    elmPosY.InnerText = posY;

                    XmlElement elmPosZ = xmlDoc.CreateElement("z");
                    elmPosZ.InnerText = posZ;

                    // create rotation element w/ values
                    XmlElement elmRotation = xmlDoc.CreateElement("rotation");

                    XmlElement elmRotW = xmlDoc.CreateElement("w");
                    elmRotW.InnerText = rotW;

                    XmlElement elmRotX = xmlDoc.CreateElement("x");
                    elmRotX.InnerText = rotX;

                    XmlElement elmRotY = xmlDoc.CreateElement("y");
                    elmRotY.InnerText = rotY;

                    XmlElement elmRotZ = xmlDoc.CreateElement("z");
                    elmRotZ.InnerText = rotZ;

                    // append childs
                    elmRoot.AppendChild(elmOriginOffset);
                    elmRoot.AppendChild(elmZeroMq);

                    elmZeroMq.AppendChild(elmServerAddress);
                    elmZeroMq.AppendChild(elmServerPort);
                    elmZeroMq.AppendChild(elmServerTopic);

                    elmOriginOffset.AppendChild(elmPosition);
                    elmOriginOffset.AppendChild(elmRotation);

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
