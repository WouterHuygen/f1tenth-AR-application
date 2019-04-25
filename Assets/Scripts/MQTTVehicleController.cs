/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Software edited by Wouter Huygen'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using M2MqttUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

/// <summary>
/// Adaptation for Unity of the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// modified to run on UWP (also tested on Microsoft HoloLens).
/// </summary>
/// 
public class MQTTVehicleController : M2MqttUnityClient
{

    [Header("Vehicle configuration")]
    [Tooltip("GameObject representing the autonomous car")]
    public GameObject vehicle;
    [Tooltip("The value the received coördinates are normalized with")]
    public float normalizeVectorFromServer = 10.0F;

    private List<string> eventMessages = new List<string>();
    private Vector3 newVehiclePos;


    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
        float.Parse(sArray[0]),
        float.Parse(sArray[1]),
        float.Parse(sArray[2]));

        return result;
    }


    // Overriding the update method so it reads out the MQTT messages everytime the update method is called.
    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()

        if (eventMessages.Count > 0)
        {
            foreach (string msg in eventMessages)
            {
                Debug.Log("Position:" + msg);
                MoveVehicleTo(vehicle, newVehiclePos);
            }
            eventMessages.Clear();
        }
    }



    protected override void SubscribeTopics()
    {
        client.Subscribe(new string[] { "M2MQTT_Unity/test" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }
    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { "M2MQTT_Unity/test" });
    }

    protected override void DecodeMessage(string topic, byte[] message)
    {
        string msg = System.Text.Encoding.UTF8.GetString(message);
        if (msg.StartsWith("(") && msg.EndsWith(")"))
        {
            // Normalize vector
            newVehiclePos = StringToVector3(msg) / normalizeVectorFromServer;
            Debug.Log("Received valid: " + msg);
            StoreMessage(msg);
        }
        else
        {
            Debug.Log("Received invalid: " + msg);
        }
    }

    private void StoreMessage(string eventMsg)
    {
        eventMessages.Add(eventMsg);
    }

    private void MoveVehicleTo(GameObject vehicle, Vector3 newPosition)
    {
        Vector3 nVector = newPosition;
        vehicle.transform.position = nVector;
    }



}
