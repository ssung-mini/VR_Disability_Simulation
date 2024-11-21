using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTransform : MonoBehaviour
{
    //private Rigidbody _r;

    Quaternion initRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        initRotation = transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotationUpdate();
    }

    void RotationUpdate()
    {
        float delta = Quaternion.Angle(transform.localRotation, initRotation);
        if (delta > 1)
        {
            transform.localRotation = initRotation;
        }
    }
}
