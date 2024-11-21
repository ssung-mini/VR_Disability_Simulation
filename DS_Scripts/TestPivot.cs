using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPivot : MonoBehaviour
{
    public Transform objTarget;
    GameObject objPivot;

    // Start is called before the first frame update
    void Start()
    {
        objPivot = new GameObject("DummyPivot");
        objPivot.transform.parent = transform;
        objPivot.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(objTarget.position.x,
                                        objPivot.transform.position.y,
                                        objTarget.position.z);
        objPivot.transform.LookAt(targetPostition);

        float pivotRotY = objPivot.transform.localRotation.y;
        Debug.Log(pivotRotY);
        
    }
}
