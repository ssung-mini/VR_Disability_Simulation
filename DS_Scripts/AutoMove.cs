using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;
    public Transform wheelFLTransform;
    public Transform wheelFRTransform;
    public Transform wheelBLTransform;
    public Transform wheelBRTransform;

    public Rigidbody rigidBody;

    public float speed = 3000.0f;
    private float maxSpeed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        
        wheelBL.motorTorque = 1.0f * speed;
        wheelBR.motorTorque = 1.0f * speed;

        

        wheelFLTransform.Rotate(0.0f, 0.0f, wheelFL.rpm / 60 * 360 * Time.deltaTime);
        wheelFRTransform.Rotate(0.0f, 0.0f, wheelFR.rpm / 60 * 360 * Time.deltaTime);
        wheelBLTransform.Rotate(0.0f, 0.0f, wheelBL.rpm / 60 * 360 * Time.deltaTime);
        wheelBRTransform.Rotate(0.0f, 0.0f, wheelBR.rpm / 60 * 360 * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if(rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }
    }
}
