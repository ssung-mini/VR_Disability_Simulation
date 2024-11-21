using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SurfaceRotationDistance : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Serial Controller (Ardity)")]
    public SerialController bldc_Serial;

    [Header("Magnitude Custom")]
    [SerializeField]
    private float torqueMagnitude;

    private float torqueSpeed_L = 0;
    private float torqueSpeed_R = 0;


    [Header("Surface Alignment")]
    [SerializeField]
    private float time;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private AnimationCurve animCurve;

    private int previous_L;
    private int previous_R;

    public GameObject targetTrans;
    private Vector3 targetVec;

    [Header("Hill Detection Ray")]
    public Transform frontRayPos;
    public Transform rearRayPos;
    public Transform leftRayPos;
    public Transform rightRayPos;

    private bool uphill;
    private bool righthill;
    private bool flatSurface_front;
    private bool flatSurface_right;
    private float rearAngle;
    private float leftAngle;

    private float hillcheck = 1f;

    public static bool initCheck = false;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // surface detection object ��ġ ����
        transform.position = new Vector3(targetTrans.transform.position.x, targetTrans.transform.position.y - 0.21f, targetTrans.transform.position.z);
        SurfaceAlignment(); // ���� ���� �� �ø��� �� ����

        //targetVec = new Vector3(transform.eulerAngles.x, targetTrans.transform.rotation.y, transform.eulerAngles.z);
        //transform.eulerAngles = targetVec;
    }

    private void SurfaceAlignment()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ������ ���� ���� //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*Ray ray = new Ray(transform.position, gameObject.transform.up * -1);

        RaycastHit info = new RaycastHit();
        Quaternion rotationRef = Quaternion.Euler(0, 0, 0);

        if (Physics.Raycast(ray, out info, 2f, whatIsGround))
        {
            rotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(time));
            transform.rotation = Quaternion.Euler(rotationRef.eulerAngles.x, targetTrans.transform.eulerAngles.y, rotationRef.eulerAngles.z); ;
        }*/

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ���ܰ�� ���� ���� //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        rearRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        frontRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit rearHit;
        if (Physics.Raycast(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up), out rearHit, Mathf.Infinity, whatIsGround))
        {
            Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * rearHit.distance, Color.yellow);
            //rearAngle = Vector3.Angle(rearHit.normal, Vector3.up);
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
            Debug.DrawRay(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up) * frontHit.distance, Color.green);
        }
        else
        {
            //uphill = true;
            //Debug.LogWarning("Uphill");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Ⱦ�ܰ�� ���� ���� //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        leftRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        rightRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit leftHit;
        if (Physics.Raycast(leftRayPos.position, leftRayPos.TransformDirection(-Vector3.up), out leftHit, Mathf.Infinity, whatIsGround))
        {
            Debug.DrawRay(leftRayPos.position, leftRayPos.TransformDirection(-Vector3.up) * leftHit.distance, Color.yellow);
            //leftAngle = Vector3.Angle(rearHit.normal, Vector3.up);
            //Debug.Log(leftAngle);
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ���ܰ�� ���� (������ ������) //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (frontHit.distance - rearHit.distance < -0.03)
        {
            uphill = true;
            flatSurface_front = false;
            //Debug.LogWarning("Uphill : " + (frontHit.distance - rearHit.distance));
        }

        else if (frontHit.distance - rearHit.distance > 0.03)
        {
            uphill = false;
            flatSurface_front = false;
            //Debug.LogWarning("Downhill : " + (frontHit.distance - rearHit.distance));
        }
        else if (-0.03 < frontHit.distance - rearHit.distance && frontHit.distance - rearHit.distance < 0.03)
        {
            flatSurface_front = true;
            //Debug.LogWarning("flat surface(����) : " + (frontHit.distance - rearHit.distance));
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Ⱦ�ܰ�� ���� (���� ������ ���) //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (rightHit.distance - leftHit.distance < -0.02)
        {
            righthill = false;
            flatSurface_right = false;
            //Debug.LogWarning("Lefthill : " + (rightHit.distance - leftHit.distance));
        }

        else if (rightHit.distance - leftHit.distance > 0.02)
        {
            righthill = true;
            flatSurface_right = false;
            //Debug.LogWarning("Righthill : " + (rightHit.distance - leftHit.distance));
        }

        else if (-0.02 < rightHit.distance - leftHit.distance && rightHit.distance - leftHit.distance < 0.02)
        {
            flatSurface_right = true;
            //Debug.LogWarning("flat surface(Ⱦ��) : " + (rightHit.distance - leftHit.distance));
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Serial�� �� �����ϱ� //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // ��簪 ���� Init
        float torqueBaseline = 15f;
        float verticalAngle;
        float horizonAngle;

        // ��簪 ���밪 ó��
        verticalAngle = Mathf.Abs(Mathf.Round((frontHit.distance - rearHit.distance) * 100));
        horizonAngle = Mathf.Abs(Mathf.Round((rightHit.distance - leftHit.distance) * 100));

        /*if (transform.localEulerAngles.x < 0)
            verticalAngle = transform.localEulerAngles.x * -1;
        else 
            verticalAngle = transform.localEulerAngles.x;

        if (transform.localEulerAngles.z < 0)
            horizonAngle = transform.localEulerAngles.z * -1;
        else 
            horizonAngle = transform.localEulerAngles.z;*/



        //else torqueBaseline = torqueBaseline * -1;

        // Ⱦ�ܰ��� 1�� ������ �� 1.197235�� ���� �ݴ�� �� �Ǿ������
        if (horizonAngle < 1)
        {
            if (verticalAngle < 0.1)    // Ⱦ�� ��� X, ���� ��� X
            {
                torqueSpeed_L = 0;
                torqueSpeed_R = 0;
            }
            else                        // Ⱦ�� ��� X, ���� ��� O
            {
                torqueSpeed_L = torqueBaseline + (verticalAngle * torqueMagnitude);
                torqueSpeed_R = torqueBaseline + (verticalAngle * torqueMagnitude);
            }
        }

        /*else if (righthill)    // ���������� �������� ��簡 ���� ��
        {
            torqueSpeed_L = (torqueBaseline + (verticalAngle + horizonAngle * torqueMagnitude)); // 1.197235f
            torqueSpeed_R = torqueBaseline + (verticalAngle * torqueMagnitude);
        }*/

        /*else if (!righthill)   // �������� �������� ��簡 ���� ��
        {
            torqueSpeed_L = torqueBaseline + (verticalAngle * torqueMagnitude);
            torqueSpeed_R = (torqueBaseline + (verticalAngle + horizonAngle * torqueMagnitude)); // 1.197235f
            //torqueSpeed_R = torqueBaseline + (verticalAngle * (horizonAngle * 1.197235f) * torqueMagnitude);
        }*/

        // Torque Direction Setting (���ܰ��)
        if (!uphill)
        {
            torqueSpeed_L = torqueSpeed_L * -1;
            torqueSpeed_R = torqueSpeed_R * -1;
        }

        // Send Serial BLDC Wheel Torque Speed to Arduino (7(min) ~ 100(max))
        if (Mathf.RoundToInt(previous_L) != Mathf.RoundToInt(torqueSpeed_L) && Mathf.RoundToInt(previous_R) != Mathf.RoundToInt(torqueSpeed_R))
        {
            //Debug.Log("���� ���� �ƴ� ��");
            Debug.Log(Mathf.RoundToInt(previous_L) + ", " + Mathf.RoundToInt(torqueSpeed_L) + " / " + Mathf.RoundToInt(previous_R) + ", " + Mathf.RoundToInt(torqueSpeed_R));
            if (!flatSurface_front)
            {
                //Debug.Log("����Ʈ ���� �ƴ� ��");
                if (-100 < torqueSpeed_L && torqueSpeed_L < 100 && -100 < torqueSpeed_R && torqueSpeed_R < 100)
                {
                    bldc_Serial.SendSerialMessage((-1 * Mathf.RoundToInt(torqueSpeed_R)).ToString() + "," + (Mathf.RoundToInt(torqueSpeed_L)).ToString());
                    bldc_Serial.SendSerialMessage((-1 * Mathf.RoundToInt(torqueSpeed_R)).ToString() + "," + (Mathf.RoundToInt(torqueSpeed_L)).ToString());
                    bldc_Serial.SendSerialMessage((-1 * Mathf.RoundToInt(torqueSpeed_R)).ToString() + "," + (Mathf.RoundToInt(torqueSpeed_L)).ToString());
                    Debug.Log("�ø��� ���� �� : " + Mathf.RoundToInt(torqueSpeed_L).ToString() + "," + Mathf.RoundToInt(torqueSpeed_R+7).ToString());

                    previous_L = Mathf.RoundToInt(torqueSpeed_L);
                    previous_R = Mathf.RoundToInt(torqueSpeed_R);
                }
            }
            else if (flatSurface_front)
            {
                bldc_Serial.SendSerialMessage("0,0");
                bldc_Serial.SendSerialMessage("0,0");
                bldc_Serial.SendSerialMessage("0,0");
                bldc_Serial.SendSerialMessage("0,0");
                Debug.Log("�÷��� ���¿���");

                previous_L = 0;
                previous_R = 0;
            }


        }

        // ������ �� �ߺ� ���� (�и� ��ȭ)
        previous_L = Mathf.RoundToInt(torqueSpeed_L);
        previous_R = Mathf.RoundToInt(torqueSpeed_R);

    }



    private void OnApplicationQuit()    // ���α׷� ���� �� ���� �ӵ��� (0, 0)���� �ʱ�ȭ
    {
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");
        bldc_Serial.SendSerialMessage("0,0");

    }
}
