using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SufaceTest : MonoBehaviour
{
    public Transform rearRayPos;
    public Transform frontRayPos;
    public LayerMask layerMask;

    float surfaceAngle;
    bool uphill;
    bool flatSurface;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rearRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);
        frontRayPos.rotation = Quaternion.Euler(-gameObject.transform.rotation.x, 0, 0);

        RaycastHit rearHit;
        if(Physics.Raycast(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up), out rearHit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * rearHit.distance, Color.yellow);
            surfaceAngle = Vector3.Angle(rearHit.normal, Vector3.up);
            Debug.Log(surfaceAngle);
        }
        else
        {
            Debug.DrawRay(rearRayPos.position, rearRayPos.TransformDirection(-Vector3.up) * 1000, Color.red);
            uphill = false;
            Debug.LogWarning("Downhill");
        }

        RaycastHit frontHit;
        Vector3 frontRayStartPos = new Vector3(frontRayPos.position.x, rearRayPos.position.y, frontRayPos.position.z);
        if(Physics.Raycast(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up), out frontHit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(frontRayStartPos, frontRayPos.TransformDirection(-Vector3.up) * frontHit.distance, Color.yellow);
        }
        else
        {
            uphill = true;
            Debug.LogWarning("Uphill");
        }

        if(frontHit.distance < rearHit.distance)
        {
            uphill = true;
            flatSurface = false;
            Debug.LogWarning("Uphill");
        }

        else if(frontHit.distance > rearHit.distance)
        {
            uphill = false;
            flatSurface = false;
            Debug.LogWarning("Downhill");
        }
        else if(frontHit.distance == rearHit.distance)
        {
            flatSurface = true;
            Debug.LogWarning("flat surface");
        }
    }
}
