using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;
using System;

public class WheelTorque_BT : MonoBehaviour
{
    public Transform rootTrans;

    private float rootRot;
    private Rigidbody _rigidBody;
    private bool isStart;
    public static float asd;

    //public SerialController serialController;
    private BluetoothHelper BTHelper;

    public string deviceName;

    // Initialization
    void Start()
    {
        try
        {
            Debug.Log("Try Connection");
            BTHelper = BluetoothHelper.GetInstance(deviceName);
            
            BTHelper.OnConnected += OnConnected;
            BTHelper.OnConnectionFailed += OnConnFailed;

            BTHelper.setTerminatorBasedStream("\n");
            BTHelper.Connect();
            if (BTHelper.isDevicePaired())
            {
                Debug.Log("Try Paired");
                
            }
                

        }
        catch (BluetoothHelper.BlueToothNotEnabledException ex) { Debug.Log("NotEnable"); }
        catch (BluetoothHelper.BlueToothNotReadyException ex) { Debug.Log("NotReady"); }
        catch (BluetoothHelper.BlueToothNotSupportedException ex) { Debug.Log("NotSupport"); }
        catch (BluetoothHelper.BlueToothPermissionNotGrantedException ex) { Debug.Log("PermissionNotGranted"); }

        rootRot = rootTrans.rotation.x;
        _rigidBody = GetComponent<Rigidbody>();
        //serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    void OnConnected()
    {
        BTHelper.StartListening();
        Debug.Log("Hi, Arduino !");
    }

    void OnConnFailed()
    {
        Debug.Log("Connection Failed !!!!!!!!");
    }

    // Executed each frame
    void FixedUpdate()
    {
        if (!isStart) StartCoroutine(InitSensor());
        else if (isStart)
        {
            string message = BTHelper.Read();
            Debug.Log("Message is : " + BTHelper.Read());

            Debug.Log("Helper is null? : " + BTHelper!=null);
            if (BTHelper != null)
            {
                Debug.Log("Helper is Abailable? : " + BTHelper.Available);
                if (BTHelper.Available)
                {
                    Debug.Log("Now Available !!!!!!!!!!!!!!!!!! Cong !!!!!!!!!!");
                    if (message[0] == '#')
                    {
                        _rigidBody.angularVelocity = gameObject.transform.right * DecryptMessage(message);
                        Debug.Log(DecryptMessage(message));
                    }
                    if (message == null)
                    {
                        Debug.Log("Now is NULL !!!!!!!");
                    }

                }
            }
        }
        


        

        //rootRot = rootTrans.rotation.x;
        //float asdd = DecryptMessage(message);


        //_rigidBody.AddTorque(transform.right * (asdd - asd), ForceMode.VelocityChange);
        //asd = asdd;



    // Check if the message is plain data or a connect/disconnect event.

    /*if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        Debug.Log("Connection established");
    else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        Debug.Log("Connection attempt failed or disconnection detected");
    else
        Debug.Log("Message arrived: " + message);*/


    //_rigidBody.AddRelativeTorque(DecryptMessage(message), ForceMode.VelocityChange);

    /*if (Input.GetAxis("Vertical") != 0)
    {

        _rigidBody.angularVelocity = gameObject.transform.right * Input.GetAxis("Vertical");
    }*/



}

    float DecryptMessage(string message)
    {
        
        string[] s = message.Substring(1).Split('/');
        float inputFloat = (float.Parse(s[0]) / 65.5f) * 1;
        //Vector3 inputVector = new Vector3((float.Parse(s[0])/65.5f)*Mathf.PI, 0, 0);
        //Vector3 inputVector = new Vector3(float.Parse(s[0]) * Mathf.PI, float.Parse(s[1])*Mathf.PI, float.Parse(s[2])* Mathf.PI);

        return inputFloat;
    }

    private IEnumerator InitSensor()
    {
        yield return new WaitForSeconds(3);

        isStart = true;

    }

    
}
