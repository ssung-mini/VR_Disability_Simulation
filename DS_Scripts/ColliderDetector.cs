using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
    private BoxCollider chairCollider;
    public SerialController servo_serial;

    
    // Start is called before the first frame update
    void Start()
    {
        chairCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            servo_serial.SendSerialMessage("0");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            /*for (int i = 0; i < 71; i++)
            {
                servo_serial.SendSerialMessage(i.ToString());
            }*/
            servo_serial.SendSerialMessage("0");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            servo_serial.SendSerialMessage("140");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapCollider"))
        {
            servo_serial.SendSerialMessage("0");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MapCollider"))
        {
            for (int i = 0; i<71; i++)
            {
                servo_serial.SendSerialMessage(i.ToString());
            }
            //servo_serial.SendSerialMessage("70");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MapCollider"))
        {
            servo_serial.SendSerialMessage("140");
        }
    }

    private void OnApplicationQuit()
    {
        servo_serial.SendSerialMessage("140");
    }

}
