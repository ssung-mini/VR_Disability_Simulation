using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLookAtIK : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        /*myLookAtIK = GetComponent<LookAtIK>();
        var head_pivot = gameObject.transform.Find("root/pelvis/spine_01/spint_02/spine_03/neck_01/head");
        var spine_3_pivot = gameObject.transform.Find("root/pelvis/spine_01/spint_02/spine_03");
        var neck_pivot = gameObject.transform.Find("root/pelvis/spine_01/spint_02/spine_03/neck_01");
        var eye_L_pivot = gameObject.transform.Find("root/pelvis/spine_01/spint_02/spine_03/neck_01/head/CC_Base_FacialBone/CC_Base_L_Eye");
        var eye_R_pivot = gameObject.transform.Find("root/pelvis/spine_01/spint_02/spine_03/neck_01/head/CC_Base_FacialBone/CC_Base_R_Eye");

        myLookAtIK.solver.target = MainManager.mainCam;
        myLookAtIK.solver.head.transform = head_pivot;
        myLookAtIK.solver.spine[0].transform = spine_3_pivot;
        myLookAtIK.solver.spine[1].transform = neck_pivot;
        myLookAtIK.solver.eyes[0].transform = eye_L_pivot;
        myLookAtIK.solver.eyes[1].transform = eye_R_pivot;*/
    }

    public void SetIKSolver()
    {
        LookAtIK myLookAtIK = GetComponent<LookAtIK>();

        var head_pivot = gameObject.transform.Find("root/pelvis/spine_01/spine_02/spine_03/neck_01/head");
        var spine_3_pivot = gameObject.transform.Find("root/pelvis/spine_01/spine_02/spine_03");
        var neck_pivot = gameObject.transform.Find("root/pelvis/spine_01/spine_02/spine_03/neck_01");
        var eye_L_pivot = gameObject.transform.Find("root/pelvis/spine_01/spine_02/spine_03/neck_01/head/CC_Base_FacialBone/CC_Base_L_Eye");
        var eye_R_pivot = gameObject.transform.Find("root/pelvis/spine_01/spine_02/spine_03/neck_01/head/CC_Base_FacialBone/CC_Base_R_Eye");

        myLookAtIK.solver.head.transform = head_pivot;
        myLookAtIK.solver.spine[0].transform = spine_3_pivot;
        myLookAtIK.solver.spine[1].transform = neck_pivot;
        myLookAtIK.solver.eyes[0].transform = eye_L_pivot;
        myLookAtIK.solver.eyes[1].transform = eye_R_pivot;
    }
}
