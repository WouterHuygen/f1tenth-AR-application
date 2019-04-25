using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpawnCubeMiddle : MonoBehaviour
{
    //public GameObject startCube;

    //public GameObject endCube;

    public ImageTargetBehaviour startTarget;

    public ImageTargetBehaviour endTarget;

    public GameObject vehicle;

    private List <ImageTargetBehaviour> imageTargets = new List<ImageTargetBehaviour>();

    private Transform vehicleMiddleTransform;

    private Vector3 middlePos = new Vector3(0, 0, 0);

    private bool IsSetup = false;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (startTarget.CurrentStatus == TrackableBehaviour.Status.TRACKED && endTarget.CurrentStatus == TrackableBehaviour.Status.TRACKED)
        {
            if (IsSetup == false)
            {
                SetupVehicleStartPos();
                IsSetup = true;
            }
            else if (IsSetup == true)
            {

            }
            
        }
        else if (startTarget.CurrentStatus == TrackableBehaviour.Status.NO_POSE || startTarget.CurrentStatus == TrackableBehaviour.Status.LIMITED || endTarget.CurrentStatus == TrackableBehaviour.Status.NO_POSE || endTarget.CurrentStatus == TrackableBehaviour.Status.LIMITED)
        {
            //print("Bad tracking");
        }

        //print("Start target:" + startTarget.transform.position);
        //print("End target:" + endTarget.transform.position);

    }

    private void SetupVehicleStartPos()
    {
        // Place vehicle in between the 2 anchor points
        middlePos = (startTarget.transform.position + endTarget.transform.position) / 2f;
        vehicle.transform.position = middlePos;
    }

}
