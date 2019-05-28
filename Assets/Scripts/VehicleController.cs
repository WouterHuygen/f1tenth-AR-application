
using NetMQ;
using NetMQ.Sockets;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using F1Tenth;
using System;
using System.Linq;
using UnityEngine.UI;
using Vuforia;
using Assets.Scripts;

/// <summary>
/// VSMS server address: tcp://143.129.39.59:5555
/// NetMQ topic: pose
/// The device running the app or it's acces point will need to be connected through the UA VPN to acces the VSMS
/// </summary>

public class VehicleController : MonoBehaviour
{
    // These variables are for setting the NetMQ settings manually through the unity editor. 
    //[Header("NetMQ configuration")]
    //[Tooltip("ZeroMQ adress where the client should connect to")]
    //public string address;
    //[Tooltip("ZeroMQ topic where the client should subscribe to")]
    //public string topic = "pose";

    [Header("Vehicle configuration")]
    [Tooltip("GameObject representing the autonomous car")]
    public GameObject vehicle;
    [Tooltip("GameObject representing the occlusion mask for a physical autonomous car")]
    public GameObject occlusionMask;
    
    [Header("AR configuration")]
    [Tooltip("First image target to be tracked")]
    public ImageTargetBehaviour targetOne;
    [Tooltip("Second image target to be tracked")]
    public ImageTargetBehaviour targetTwo;
    [Tooltip("Thirth image target to be tracked")]
    public ImageTargetBehaviour targetThree;
    [Tooltip("Fourth image target to be tracked")]
    public ImageTargetBehaviour targetFour;

    [Tooltip("Debug Text")]
    public Text debugText;

    // NetMQ listener
    private NetMqListener _netMqListener;
    // Proto Object
    private F1Tenth.Pose pose;

    // NetMQ config variables
    private string serverAddress;
    private string serverIp;
    private string serverPort;
    private string serverTopic;

    public List<int> vehicleIds { get; private set; }

    Dictionary<long, GameObject> vehicleDictionary =
           new Dictionary<long, GameObject>();

    private GameObject[] vehicleArray = new GameObject[0];
    private int newVehicleArrayLength = 0;

    // Origin Offset to set the starting point for the cars
    private UnityEngine.Vector3 originOffsetPosition;
    private UnityEngine.Quaternion originOffsetRotation;

    public bool IsSetup { get; private set; }


    private void Start()
    {
        GetNetMqSettings();
        SetupOriginOffset();

        _netMqListener = new NetMqListener(HandleNetMqMessage, serverAddress, serverTopic);
        _netMqListener.Start();

        //vehicleIds = new List<int>();
    }

    private void Update()
    {
        _netMqListener.Update();

        if (targetOne.CurrentStatus == TrackableBehaviour.Status.TRACKED && targetTwo.CurrentStatus == TrackableBehaviour.Status.TRACKED && IsSetup == false)
        {
            IsSetup = true;
        }
        else if (IsSetup == true)
        {
            //MoveObjectTo(vehicleArray[(long)pose.Id], (PoseConverter.ToUnityVector3(pose.Position)) + originOffsetPosition);

            //RotateObjectTo(vehicleArray[(long)pose.Id], (PoseConverter.ToUnityQuaternion(pose.Rotation)) * originOffsetRotation);

            MoveObjectTo(vehicleDictionary[(long)pose.Id], (PoseConverter.ToUnityVector3(pose.Position)) + originOffsetPosition);

            // Rotation offset not yet working because making a quaternion offset is not tangible
            RotateObjectTo(vehicleDictionary[(long)pose.Id], (PoseConverter.ToUnityQuaternion(pose.Rotation)) ); // * originOffsetRotation
        }
    }
    
    private void OnDestroy()
    {
        _netMqListener.Stop();
    }


    // This method gets called everytime there is a new message from the NetMQ listener
    private void HandleNetMqMessage(byte[] message)
    {
        var text = SendReceiveConstants.DefaultEncoding.GetString(message);

        if (text == serverTopic)
        {
            Debug.Log(text);
        }
        else if (text != serverTopic)
        {
            pose = F1Tenth.Pose.Parser.ParseFrom(message);

            Debug.Log(pose);

            if (!vehicleDictionary.ContainsKey((long)pose.Id))
            {
                // Check if occlusion is ON
                if (SettingsManager.Instance.IsOccluded == true)
                {
                    // Check if the vehicle is a physical f1tenth
                    if (pose.IsPhysical == true)
                    {
                        CreateNewPhysicalVehicleMask((long)pose.Id);
                    }
                    else if (pose.IsPhysical == false)
                    {
                        CreateNewVirtualVehicle((long)pose.Id);
                    }
                }
                else if (SettingsManager.Instance.IsOccluded == false && pose.IsPhysical == false)
                {
                    CreateNewVirtualVehicle((long)pose.Id);
                }
            }



            //if (!vehicleIds.Contains((int)pose.Id))
            //{
            //    vehicleIds.Add((int)pose.Id);

            //    foreach (int id in vehicleIds)
            //    {
            //        newVehicleArrayLength = Math.Max(newVehicleArrayLength, id);
            //    }

            //    Array.Resize(ref vehicleArray, newVehicleArrayLength + 1);

            //    if (SettingsManager.Instance.IsOccluded == true)
            //    {
            //        if (pose.IsPhysical == true)
            //        {
            //            CreateNewPhysicalVehicleMask((int)pose.Id);
            //        }
            //        else if (pose.IsPhysical == false)
            //        {
            //            CreateNewVirtualVehicle((int)pose.Id);
            //        }
            //    }
            //    else if (SettingsManager.Instance.IsOccluded == false && pose.IsPhysical == false)
            //    {
            //        CreateNewVirtualVehicle((int)pose.Id);
            //    }
            //}

        }

    }

    private void MoveObjectTo(GameObject objectToMove, UnityEngine.Vector3 newPosition)
    {
        UnityEngine.Vector3 nVector = newPosition;
        objectToMove.transform.position = nVector;
    }

    // Rotation should happen with EulerAngles instead of Quaternions
    private void RotateObjectTo(GameObject objectToRotate, UnityEngine.Quaternion newRotation)
    {
        UnityEngine.Quaternion _newRotation = newRotation;
        objectToRotate.transform.localRotation = _newRotation;
    }

    private void CreateNewVirtualVehicle(long Id)
    {
        GameObject _vehicle = (GameObject)Instantiate(vehicle);
        vehicleDictionary.Add(Id, _vehicle);
        //vehicleArray[Id] = _vehicle;

    }

    private void CreateNewPhysicalVehicleMask(long Id)
    {
        GameObject _mask = (GameObject)Instantiate(occlusionMask);
        vehicleDictionary.Add(Id, _mask);
        //vehicleArray[Id] = _mask;
    }

    private void GetNetMqSettings()
    {
        serverIp = SettingsManager.Instance.ServerIp;
        serverPort = SettingsManager.Instance.ServerPort;
        serverTopic = SettingsManager.Instance.ServerTopic;
        serverAddress = "tcp://" + serverIp + ":" + serverPort;
    }

    private void SetupOriginOffset()
    {
        originOffsetPosition = new UnityEngine.Vector3(
            SettingsManager.Instance.PosX,
            SettingsManager.Instance.PosY,
            SettingsManager.Instance.PosZ);

        originOffsetRotation = new UnityEngine.Quaternion(
            SettingsManager.Instance.RotW,
            SettingsManager.Instance.RotX,
            SettingsManager.Instance.RotY,
            SettingsManager.Instance.RotZ);
    }

}
