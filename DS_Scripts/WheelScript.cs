using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
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

    public float speed;
    //private float maxSpeed = 1.2f;
    private float breakSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        wheelBL.motorTorque = Input.GetAxis("Vertical") * speed;
        wheelBR.motorTorque = Input.GetAxis("Vertical") * speed;
        wheelFLTransform.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        wheelFRTransform.Rotate(-(wheelFL.rpm / 60 * 360) * Time.deltaTime, 0.0f, 0.0f);
        wheelBLTransform.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        wheelBRTransform.Rotate(-(wheelFL.rpm / 60 * 360) * Time.deltaTime, 0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        /*if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
        }*/

        /*if (Input.GetAxis("Vertical") == 0f)
        {
            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, breakSpeed);
        }*/
    }
}
