using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceTest : MonoBehaviour
{
    [Header("Magnitude Custom")]
    [SerializeField]
    private float torqueMagnitude;

    [Header("Surface Alignment")]
    [SerializeField]
    private float time;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private AnimationCurve animCurve;

    public GameObject targetTrans;

    public Transform frontRayPos;
    public Transform rearRayPos;
    public Transform leftRayPos;
    public Transform rightRayPos;
    //public LayerMask layerMask;

    float surfaceAngle;
    bool uphill;
    bool righthill;
    bool flatSurface;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = targetTrans.transform.position;
        //targetVec = new Vector3(transform.eulerAngles.x, targetTrans.transform.rotation.y, transform.eulerAngles.z);
        //transform.eulerAngles = targetVec;
        SurfaceAlignment();
    }

    private void SurfaceAlignment()
    {
        Ray ray = new Ray(transform.position, gameObject.transform.up * -1);

        RaycastHit info = new RaycastHit();
        Quaternion rotationRef = Quaternion.Euler(0, 0, 0);

        if (Physics.Raycast(ray, out info, 5.0f, whatIsGround))
        {
            rotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(time));
            transform.rotation = Quaternion.Euler(rotationRef.eulerAngles.x, transform.eulerAngles.y, rotationRef.eulerAngles.z);
        }

        rearRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        frontRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit rearHit;
        if (Physics.Raycast(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up), out rearHit, Mathf.Infinity, whatIsGround))
        {
            Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * rearHit.distance, Color.yellow);
            //surfaceAngle = Vector3.Angle(rearHit.normal, Vector3.up);
            //Debug.Log(surfaceAngle);
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
            Debug.DrawRay(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up) * frontHit.distance, Color.green);
        }
        else
        {
            //uphill = true;
            //Debug.LogWarning("Uphill");
        }

        // 횡단경사 레이 설정 //

        leftRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        rightRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit leftHit;
        if (Physics.Raycast(leftRayPos.position, leftRayPos.TransformDirection(-Vector3.up), out leftHit, Mathf.Infinity, whatIsGround))
        {
            Debug.DrawRay(leftRayPos.position, leftRayPos.TransformDirection(-Vector3.up) * leftHit.distance, Color.yellow);
            //surfaceAngle = Vector3.Angle(rearHit.normal, Vector3.up);
            //Debug.Log(surfaceAngle);
        }
        else
        {
            //Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * 1000, Color.red);
            //uphill = false;
            //Debug.LogWarning("Downhill");
        }

        RaycastHit rightHit;
        Vector3 rightRayStartPos = new Vector3(rightRayPos.position.x, leftRayPos.position.y, rightRayPos.position.z);
        if (Physics.Raycast(rightRayStartPos, rightRayPos.TransformDirection(-Vector3.up), out rightHit, Mathf.Infinity, whatIsGround))
        {
            Debug.DrawRay(rightRayStartPos, rightRayPos.TransformDirection(-Vector3.up) * rightHit.distance, Color.green);
        }
        else
        {
            //uphill = true;
            //Debug.LogWarning("Uphill");
        }

        // 앞뒤 종단경사 
        if (frontHit.distance - rearHit.distance < -0.01)
        {
            uphill = true;
            flatSurface = false;
            Debug.LogWarning("Uphill");
        }

        else if (frontHit.distance - rearHit.distance > 0.01)
        {
            uphill = false;
            flatSurface = false;
            Debug.LogWarning("Downhill");
        }
        else if (-0.01 < frontHit.distance - rearHit.distance && frontHit.distance - rearHit.distance < 0.01)
        {
            flatSurface = true;
            Debug.LogWarning("flat surface(종단)");
        }

        // 양옆 횡단경사
        if (rightHit.distance - leftHit.distance < -0.01)
        {
            righthill = false;
            flatSurface = false;
            Debug.LogWarning("Lefthill");
        }

        else if (rightHit.distance - leftHit.distance > 0.01)
        {
            righthill = true;
            flatSurface = false;
            Debug.LogWarning("Righthill");
        }

        else if (-0.01 < rightHit.distance - leftHit.distance && rightHit.distance - leftHit.distance < 0.01)
        {
            flatSurface = true;
            Debug.LogWarning("flat surface(횡단)");
        }
    }
}