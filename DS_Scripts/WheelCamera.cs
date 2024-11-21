using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCamera : MonoBehaviour
{
    public GameObject targetWheel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(targetWheel.transform.position.x - 1.55037f, targetWheel.transform.position.y- 0.2430964f, targetWheel.transform.position.z - 0.3454673f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetWheel.transform.eulerAngles.y + 90f, transform.eulerAngles.z);
    }
}
