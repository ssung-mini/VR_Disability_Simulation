using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class JoystickWheelController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    public Transform frontRayPos;
    public Transform rearRayPos;

    [SerializeField]
    private LayerMask whatIsGround;

    private float rearAngle;
    private float leftAngle;

    private float horizontalInput;
    private float verticalInput;
    //private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [Header("Joystick Movements")]
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float turnMagnitude = 0.4f;
    //[SerializeField] private GameObject trackingHead;

    [Header("Wheel Setting Components")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    float rotationX = 0f;
    float rotationY = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }

    private void Update()
    {
        GetInput();
        //HandleSteering();
    }



    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        RaycastHit rearHit;
        if (Physics.Raycast(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up), out rearHit, Mathf.Infinity, whatIsGround))
        {
            //Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * rearHit.distance, Color.yellow);
            rearAngle = Vector3.Angle(rearHit.normal, Vector3.up);
            //Debug.Log(rearAngle);
        }
        else
        {
            //Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * 1000, Color.red);
            //uphill = false;
            //Debug.LogWarning("Downhill");
        }

        RaycastHit frontHit;
        Vector3 frontRayStartPos = new Vector3(frontRayPos.position.x, rearRayPos.position.y, frontRayPos.position.z);
        if (Physics.Raycast(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up), out frontHit, Mathf.Infinity, whatIsGround))
        {
            //Debug.DrawRay(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up) * frontHit.distance, Color.green);
        }
        else
        {
            //uphill = true;
            //Debug.LogWarning("Uphill");
        }

        if (frontHit.distance - rearHit.distance < -0.03) // (frontHit.distance - rearHit.distance < -0.03)
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce * 3;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce * 3;
        }


        else
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        }

        
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        transform.Rotate(0.0f, 90.0f * horizontalInput * turnMagnitude * Time.deltaTime, 0.0f);

        //transform.rotation = Quaternion.Euler(0.0f, 90.0f * horizontalInput * turnMagnitude * Time.deltaTime, 0.0f);

        /*transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 
            trackingHead.transform.rotation.eulerAngles.y, 
            transform.rotation.eulerAngles.z);
        *//*currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;*//**/
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        //rot = new Quaternion(rot.x * 0.3f, rot.y, rot.z, rot.w);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    
}
