using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPos : MonoBehaviour
{
    public GameObject targetTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetTrans.transform.position.x, targetTrans.transform.position.y + 0.324f, targetTrans.transform.position.z + 0.134f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetTrans.transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
