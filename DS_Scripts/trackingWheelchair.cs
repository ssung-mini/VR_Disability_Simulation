using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackingWheelchair : MonoBehaviour
{
    //public Vector3 neckPosition;
    
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float smoothPosition = 0.5f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3(0,0.157999665,-0.0130003951)
        //neckRotation = gameObject.transform.rotation;

        transform.position = Vector3.Lerp(transform.position, target.position, smoothPosition);
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, target.rotation, smoothSpeed);
    }
}
