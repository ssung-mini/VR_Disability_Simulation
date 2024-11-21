using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ChooseHeadTracking : TrackedPoseDriver
{
    public GameObject wheelchair;

    protected override void SetLocalTransform(Vector3 newPosition, Quaternion newRotation)
    {
        base.SetLocalTransform(newPosition, newRotation);

        Vector3 ChooseRot = new Vector3(newRotation.eulerAngles.x, wheelchair.transform.eulerAngles.y, newRotation.eulerAngles.z);
        transform.eulerAngles = ChooseRot;
        Vector3 wheelRot = new Vector3(wheelchair.transform.localEulerAngles.x, newRotation.eulerAngles.y, wheelchair.transform.localEulerAngles.z);
        wheelchair.transform.localEulerAngles = wheelRot;
    }
}
