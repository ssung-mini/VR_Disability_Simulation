using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples.DevTools;
using UnityEngine;

public class makeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void setCollider()
    {
        var head_pivot = gameObject.transform.Find("root/pelvis/spine_01/spine_02/spine_03/neck_01/head");
        var body_pivot = gameObject.transform.Find("CC_Base_Body");

        gameObject.GetComponent<CapsuleCollider>().radius = 0.115f;

        GameObject headCollider = new GameObject("headGaze");
        GameObject bodyCollider = new GameObject("BodyGaze");

        headCollider.tag = "Face";
        bodyCollider.tag = "Body";

        headCollider.AddComponent<BoxCollider>();
        headCollider.GetComponent<BoxCollider>().isTrigger = true;
        headCollider.GetComponent<BoxCollider>().center = new Vector3(-0.002188877f, 0.07614059f, 0.009384845f);
        headCollider.GetComponent<BoxCollider>().size = new Vector3(0.1992756f, 0.2345938f, 0.2326262f);
        headCollider.AddComponent<GazeTarget>();

        bodyCollider.AddComponent<BoxCollider>();
        bodyCollider.GetComponent<BoxCollider>().isTrigger = true;
        bodyCollider.GetComponent<BoxCollider>().center = new Vector3(0.004255311f, -1.437855e-09f, 0.7287897f);
        bodyCollider.GetComponent<BoxCollider>().size = new Vector3(0.670873f, 0.3f, 1.436042f);
        bodyCollider.AddComponent<GazeTarget>();

        headCollider.transform.SetParent(head_pivot);
        bodyCollider.transform.SetParent(body_pivot);

        headCollider.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        bodyCollider.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
    }
}
