using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class LoadNetworkSettings : MonoBehaviour
{
    public Text serverAddressText;
    public Text serverPortText;
    public Text serverTopicText;

    private string serverAddress;
    private string serverPort;
    private string serverTopic;

    //The save location of the XML
    private string xmlFilePath;

    private void Start()
    {
        xmlFilePath = Application.persistentDataPath + @"/AR-F1TENTH-SETTINGS.xml";
        GetZeroMqSettings(xmlFilePath);
    }

    public void GetZeroMqSettings(string xmlFilePath)
    {
        
        XmlDocument xmlDoc = new XmlDocument();

        if (File.Exists(xmlFilePath))
        {
            xmlDoc.Load(xmlFilePath);

            XmlNodeList zeroMqList = xmlDoc.GetElementsByTagName("ZeroMq");

            foreach (XmlNode zeroMqInfo in zeroMqList)
            {
                XmlNodeList zeroMqContent = zeroMqInfo.ChildNodes;

                    if (zeroMqInfo.Name == "serverAddress")
                    {
                        serverAddress = zeroMqInfo.InnerText;
                    }
                    if (zeroMqInfo.Name == "serverPort")
                    {
                        serverPort = zeroMqInfo.InnerText;
                    }
                    if (zeroMqInfo.Name == "serverTopic")
                    {
                        serverTopic = zeroMqInfo.InnerText;
                    }
            }
            Debug.Log("Server address: " + serverAddress);
            // Write settings to UI input text
            serverAddressText.text = serverAddress;
            serverPortText.text = serverPort;
            serverTopicText.text = serverTopic;
        }

        
    }
}
