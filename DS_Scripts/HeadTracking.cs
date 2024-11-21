using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    public Transform headTransform;
    public Transform cameraTransform;
    public Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        headTransform.rotation = cameraTransform.rotation;
        //cameraTransform = targetTransform;
    }
}
