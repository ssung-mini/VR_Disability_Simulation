using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPosition : MonoBehaviour
{
    [HideInInspector] private Transform target;
    //public Transform targetPos;

    private Transform targetppos;

    [HideInInspector] public Vector3 speed = Vector3.zero;

    private void Awake()
    {
        target = MainManager.mainCam;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetppos = new GameObject().GetComponent<Transform>();
        targetppos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetppos.position, ref speed, 0.3f);
    }


}
