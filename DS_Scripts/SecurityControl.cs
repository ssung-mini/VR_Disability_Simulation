using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class SecurityControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Animator>().CrossFade("Standing W_Briefcase Idle", 0.1f, 0, Random.Range(0.0f, 1.0f));
    }
}
