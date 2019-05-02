
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

public class NetMqVehicleController : MonoBehaviour
{
    [Header("NetMQ configuration")]
    [Tooltip("ZeroMQ adress where the client should connect to")]
    public string address = "";
    [Tooltip("ZeroMQ topic where the client should subscribe to")]
    public string topic = "pose";

    [Header("Vehicle configuration")]
    [Tooltip("GameObject representing the autonomous car")]
    public GameObject vehicle;
    [Tooltip("GameObject representing the occlusion mask for a physical autonomous car")]
    public GameObject occlusionMask;
    [Tooltip("The value the received coördinates are normalized with")]
    public float normalizeVectorFromServer = 1F;
    [Tooltip("The offset for the 0 coordinate")]
    public UnityEngine.Vector3 nullPointOffset = new UnityEngine.Vector3(2.815F, 0F, -0.22F);
    [Tooltip("Text Object to display")]
    public Text poseText;

    [Header("AR configuration")]
    [Tooltip("First image target to be tracked")]
    public ImageTargetBehaviour targetOne;
    [Tooltip("Second image target to be tracked")]
    public ImageTargetBehaviour targetTwo;
    [Tooltip("Thirth image target to be tracked")]
    public ImageTargetBehaviour targetThree;
    [Tooltip("Fourth image target to be tracked")]
    public ImageTargetBehaviour targetFour;

    private UnityEngine.Vector3 newVehiclePos;
    private NetMqListener _netMqListener;
    private F1Tenth.Pose pose;
    private UnityEngine.Vector3 startPos;
    private GameObject cube;

    public List<int> vehicleIds { get; private set; }

    private GameObject[] vehicleArray = new GameObject[0];
    private int newVehicleArrayLength = 0;

    public bool IsSetup { get; private set; }

    private void MoveObjectTo(GameObject obj, UnityEngine.Vector3 newPosition)
    {
        UnityEngine.Vector3 nVector = newPosition;
        obj.transform.position = nVector;
    }

    private void RotateObjectTo(GameObject obj, UnityEngine.Quaternion newRotation)
    {
        UnityEngine.Quaternion _newRotation = newRotation;
        obj.transform.localRotation = _newRotation;
    }

    private void HandleMessage(byte[] message)
    {
        var text = SendReceiveConstants.DefaultEncoding.GetString(message);
        
        if (text == topic)
        {
            Debug.Log(text);
        }
        else if (text != topic)
        {
            pose = F1Tenth.Pose.Parser.ParseFrom(message);
            Debug.Log(pose);
            if (!vehicleIds.Contains((int)pose.Id))
            {
                vehicleIds.Add((int)pose.Id);

                foreach (int id in vehicleIds)
                {
                    newVehicleArrayLength = Math.Max(newVehicleArrayLength, id);
                }

                Array.Resize(ref vehicleArray, newVehicleArrayLength + 1);


                if (pose.IsPhysical == true)
                {
                    CreateNewPhysicalVehicleMask((int)pose.Id);
                }
                else if (pose.IsPhysical == false)
                {
                    CreateNewVirtualVehicle((int)pose.Id);
                }

            }
            
        }

    }

    void Start()
    {
        _netMqListener = new NetMqListener(HandleMessage, address, topic);
        _netMqListener.Start();
        vehicleIds = new List<int>();
    }

    void Update()
    {
        _netMqListener.Update();

        if (targetOne.CurrentStatus == TrackableBehaviour.Status.TRACKED && targetTwo.CurrentStatus == TrackableBehaviour.Status.TRACKED && IsSetup == false)
        {
            IsSetup = true;

            
        }
        else if (IsSetup == true)
        {

            MoveObjectTo(vehicleArray[(int)pose.Id], (Converter.ToUnityVector3(pose.Position) / normalizeVectorFromServer));

            RotateObjectTo(vehicleArray[(int)pose.Id], (Converter.ToUnityQuaternion(pose.Rotation)));
            
        }
    }

    private void OnDestroy()
    {
        _netMqListener.Stop();
    }


    private void CreateNewVirtualVehicle(int Id)
    {
        GameObject _vehicle = (GameObject)Instantiate(vehicle);
        vehicleArray[Id] = _vehicle;

    }

    private void CreateNewPhysicalVehicleMask(int Id)
    {
        GameObject _mask = (GameObject)Instantiate(occlusionMask);
        vehicleArray[Id] = _mask;
    }
}
