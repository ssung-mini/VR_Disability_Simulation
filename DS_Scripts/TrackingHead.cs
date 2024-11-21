using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingHead : MonoBehaviour
{
    public GameObject targetTrans;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 headPos = new Vector3(targetTrans.transform.position.x, targetTrans.transform.position.y + 0.1000011f, targetTrans.transform.position.z + 0.2109998f);
        Quaternion headRot = Quaternion.Euler(targetTrans.transform.eulerAngles.x, targetTrans.transform.eulerAngles.y, targetTrans.transform.eulerAngles.z);

        transform.position = Vector3.Lerp(transform.position, headPos, Time.deltaTime * 10f);
        transform.rotation = headRot;
    }
}

