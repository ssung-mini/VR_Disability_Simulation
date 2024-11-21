using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueR : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private bool isStart;

    public float magnitude = 2f;
    public float threshold = 2f;

    public SerialController serialController;

    public static bool check_isUphill = false;

    // Initialization
    void Start()
    {
        //rootRot = rootTrans.rotation.x;
        _rigidBody = GetComponent<Rigidbody>();
        //serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Executed each frame
    void FixedUpdate()
    {

        string message = serialController.ReadSerialMessage();


        /*if (message == null)
        {
            _rigidBody.angularVelocity = gameObject.transform.right * 0f;
        }*/


        if(message != null)
        {
            if (message[0] == '#')
            {
                /*if(DecryptMessage(message) != 0)
                {
                    
                }*/
                _rigidBody.angularVelocity = gameObject.transform.right * DecryptMessage(message);
                //_rigidBody.velocity = new Vector3(0, 0, 0);
                //_rigidBody.AddTorque(gameObject.transform.right * DecryptMessage(message) * magnitude);
            }
        }

        



        //if (-1 * threshold < _rigidBody.angularVelocity.x && _rigidBody.angularVelocity.x < threshold) _rigidBody.angularVelocity = new Vector3(0, 0, 0);

    }

    float DecryptMessage(string message)
    {

        string[] s = message.Substring(1).Split('/');

        float inputGyroX = float.Parse(s[0]);
        float inputFloat = 0;

        if ((-1 * threshold) < inputGyroX && inputGyroX < threshold)
        {
            inputFloat = 0;
            
        }

        else if (-255f < inputGyroX && inputGyroX < 255)
        {
            inputFloat = (inputGyroX / 65.5f) * magnitude;
            //Debug.Log("Right Wheel : " + inputGyroX);
            
        }

        if (check_isUphill && inputFloat > 0)
        {
            inputFloat = (inputFloat / 3);
        }

        /*else if (check_isUphill && inputFloat < 0)
        {
            inputFloat = inputFloat * 2f;
        }*/
        
        return inputFloat;
    }
}
