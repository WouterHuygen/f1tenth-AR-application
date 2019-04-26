using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{

    public Button saveButton;

    private string xmlLocation; //The save location of the XML

    // position values to work with the xml file.
    private string posX = "0";
    private string posY = "0";
    private string posZ = "0";

    // rotation values to work with the xml file.
    private string rotW = "1"; // Default always 1 for normal rotations
    private string rotX = "0";
    private string rotY = "0";
    private string rotZ = "0";

    void Start()
    {
        // Set xml fiel location and create a new xml file if there isn't one
        xmlLocation = Application.persistentDataPath + @"/AR-F1TENTH-SETTINGS.xml";
        if (!File.Exists(xmlLocation))
        {
            CreateXML(xmlLocation);
        }

        //Calls the TaskOnClick method when you click the Button
        saveButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        WriteToXml(xmlLocation);
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
            xmlDoc.Load(xmlFilePath);

            XmlElement elmRoot = xmlDoc.DocumentElement;

            elmRoot.RemoveAll(); // cleanup existing elements inside Root

            // create origin position offset element
            XmlElement elmOriginOffset = xmlDoc.CreateElement("originOffset");

            // create transform element
            XmlElement elmTransform = xmlDoc.CreateElement("transform");

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

            elmOriginOffset.AppendChild(elmTransform);

            elmTransform.AppendChild(elmPosition);
            elmTransform.AppendChild(elmRotation);

            elmPosition.AppendChild(elmPosX);
            elmPosition.AppendChild(elmPosY);
            elmPosition.AppendChild(elmPosZ);

            elmRotation.AppendChild(elmRotW);
            elmRotation.AppendChild(elmRotX);
            elmRotation.AppendChild(elmRotY);
            elmRotation.AppendChild(elmRotZ);


            xmlDoc.Save(xmlFilePath); // save file
        }
    }
}
