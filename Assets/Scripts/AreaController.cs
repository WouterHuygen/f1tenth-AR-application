using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AreaController : MonoBehaviour
{
    public ImageTargetBehaviour startTarget;

    public ImageTargetBehaviour endTarget;

    public GameObject groundPlane;


    // Start is called before the first frame update
    void Start()
    {
        

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetsProduct = (startTarget.transform.position + endTarget.transform.position) / 2f;
        groundPlane.transform.position = targetsProduct;
        float dist = Vector3.Distance(endTarget.transform.position, startTarget.transform.position);
        Vector3 groundScale = new Vector3(dist / 10, dist / 10, dist / 10);
        groundPlane.transform.localScale = groundScale;

    }
}


