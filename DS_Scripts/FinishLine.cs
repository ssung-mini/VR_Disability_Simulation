using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class FinishLine : MonoBehaviour
{
    
    public FadeScreen pwdFade;
    /*public FadeScreen pwod1Fade;
    public FadeScreen pwod2Fade;
    public FadeScreen pwod3Fade;

    public GameObject pwodViewL;
    public GameObject pwodViewM;
    public GameObject pwodViewR;*/

    public static FadeScreen _pwdFade;
    /*public static FadeScreen _pwod1Fade;
    public static FadeScreen _pwod2Fade;
    public static FadeScreen _pwod3Fade;

    public static GameObject _pwodViewL;
    public static GameObject _pwodViewM;
    public static GameObject _pwodViewR;

    public GameObject pwod1;
    public GameObject pwod2;
    public GameObject pwod3;

    public static GameObject _pwod1;
    public static GameObject _pwod2;
    public static GameObject _pwod3;*/

    public static bool finished = false;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update

    void Start()
    {
        _pwdFade = pwdFade;
        /*_pwod1Fade = pwod1Fade;
        _pwod2Fade = pwod2Fade;
        _pwod3Fade = pwod3Fade;

        _pwodViewL = pwodViewL;
        _pwodViewM = pwodViewM;
        _pwodViewR = pwodViewR;

        _pwod1 = pwod1;
        _pwod2 = pwod2;
        _pwod3 = pwod3;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Checkpoint1"))
        {
            if (!MainManager.nowMode.Equals("PwD"))
            {
                pwod1Fade.FadeOut2();
                
            }
            
        }

        if (other.CompareTag("Checkpoint2"))
        {
            if (!MainManager.nowMode.Equals("PwD"))
            {
                pwod2Fade.FadeOut3();
                
            }
        }*/

        if (other.CompareTag("Checkpoint") && !finished)
        {
            if (MainManager.nowMode.Equals("PwD"))
            {
                pwdFade.FadeOut();
                finished = true;
            }
            //else pwod3Fade.FadeOut();
        }
    }
}
