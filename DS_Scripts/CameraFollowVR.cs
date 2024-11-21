using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowVR : MonoBehaviour
{
    public Transform target;
    //public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;

        transform.rotation = target.rotation;
        transform.position = target.position;
        //transform.LookAt(target.forward);
    }
}
