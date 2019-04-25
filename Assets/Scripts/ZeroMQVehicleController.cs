using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZeroMQ;
using F1Tenth;
using System.Threading;
using UnityEngine.UI;

public class ZeroMQVehicleController : MonoBehaviour
{
    [Header("ZeroMQ configuration")]
    [Tooltip("ZeroMQ adress where the client should connect to")]
    // tcp://143.129.39.59:5555
    public string address = "tcp://172.16.0.116:5556";
    [Tooltip("ZeroMQ topic where the client should subscribe to")]
    public string topic = "pose";

    [Header("Vehicle configuration")]
    [Tooltip("GameObject representing the autonomous car")]
    public GameObject vehicle;
    [Tooltip("The value the received coördinates are normalized with")]
    public float normalizeVectorFromServer = 10.0F;
    [Tooltip("Text Object to display")]
    public Text poseText;

    private bool listenerCancelled;

    ZContext context;
    ZSocket subscriber;
    F1Tenth.Pose pose;

    void Start()
    {
        AsyncIO.ForceDotNet.Force();
        context = new ZContext();
        subscriber = new ZSocket(context, ZSocketType.SUB);

        // Set a high water mark to avoid the memory running out
        subscriber.ReceiveHighWatermark = 1000;

        // Connect to publisher
        print("Connecting to " + address);
        subscriber.Connect(address);
        poseText.text = "Connected to: " + address;

        // Subscribe to topic
        print("Subscribing to topic " + topic);
        subscriber.Subscribe(topic);

    }

    void Update()
    {
        using (var replyMessage = subscriber.ReceiveMessage())
        {
            if (replyMessage != null)
            {
                print("Message from: " + replyMessage.Pop());
                pose = F1Tenth.Pose.Parser.ParseFrom(replyMessage.Pop());
                print(pose);
                poseText.text = "Pose: " + pose;
                RotateVehicleTo(vehicle, ToUnityQuaternion(pose.Rotation));
                MoveVehicleTo(vehicle, ToUnityVector3(pose.Position));
            }
            else
            {
                print("No messages from VSMS");
            }
        }



    }


    private void MoveVehicleTo(GameObject vehicle, UnityEngine.Vector3 newPosition)
    {
        UnityEngine.Vector3 _newPosition = newPosition;
        _newPosition /= normalizeVectorFromServer;
        vehicle.transform.position = _newPosition;
    }

    private void RotateVehicleTo(GameObject vehicle, UnityEngine.Quaternion newRotation)
    {
        UnityEngine.Quaternion _newRotation = newRotation;
        vehicle.transform.localRotation = _newRotation;
    }

    private UnityEngine.Vector3 ToUnityVector3(F1Tenth.Vector3 vector3)
    {
        UnityEngine.Vector3 _vector3 = new UnityEngine.Vector3();
        _vector3.x = vector3.X;
        _vector3.y = vector3.Y;
        _vector3.z = vector3.Z;
        return _vector3;
    }

    private UnityEngine.Quaternion ToUnityQuaternion(F1Tenth.Quaternion quaternion)
    {
        UnityEngine.Quaternion _quaternion = new UnityEngine.Quaternion();
        _quaternion.w = quaternion.W;
        _quaternion.x = quaternion.X;
        _quaternion.y = quaternion.Y;
        _quaternion.z = quaternion.Z;
        return _quaternion;
    }
}

