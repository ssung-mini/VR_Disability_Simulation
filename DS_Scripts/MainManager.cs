using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    //public int _participantsIndex;
    //public static int participantsIndex;

    public Transform _mainCam;
    public static Transform mainCam;

    public GameObject pwd;
    //public GameObject _pwdEye;
    //public static GameObject pwdEye;
    //public GameObject pwod1;
    //public GameObject pwod2;
    //public GameObject pwod3;

    public GameObject pwdView;
    //public GameObject pwodView1;
    //public GameObject pwodView2;
    //public GameObject pwodView3;

    public Transform _objTarget;
    public static Transform objTarget;

    public static bool nowPwd = false;
    public static bool nowPwod1 = false;
    public static bool nowPwod2 = false;
    public static bool nowPwod3 = false;

    public static string nowPlayingMode;
    public static int nowPlayingModeNum;

    enum Emotion { positive, negative, neutral };
    [SerializeField] Emotion selEmotion = Emotion.positive;

    enum PlayerMode { PwoD, PwD };
    [SerializeField] PlayerMode playerMode = PlayerMode.PwD;

    public static string nowEmotion;
    public static string nowMode;

    private void Awake()
    {
        mainCam = _mainCam;
        //pwdEye = _pwdEye;
        nowEmotion = selEmotion.ToString();
        nowMode = playerMode.ToString();
        objTarget = _objTarget;

        //participantsIndex = _participantsIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*if (playerMode == PlayerMode.PwD)
        {
            nowPwd = true;
            nowPlayingMode = "PwD";
            MainManager.nowPlayingModeNum = 0;
            pwd.GetComponent<WheelScript>().enabled = true;
            pwd.GetComponent<AutoMove>().enabled = false;

            pwod1.GetComponent<RootMotion.FinalIK.LookAtIK>().enabled = true;
            pwod1.GetComponent<RootMotion.FinalIK.VRIK>().enabled = false;

            pwodView1.SetActive(false);

            pwod2.GetComponent<RootMotion.FinalIK.LookAtIK>().enabled = true;
            pwod2.GetComponent<RootMotion.FinalIK.VRIK>().enabled = false;

            pwodView2.SetActive(false);

            pwod3.GetComponent<RootMotion.FinalIK.LookAtIK>().enabled = true;
            pwod3.GetComponent<RootMotion.FinalIK.VRIK>().enabled = false;

            pwodView3.SetActive(false);

            pwdView.SetActive(true);

        }
        else
        {
            nowPwod1 = true;
            nowPlayingMode = "PwoD1";
            MainManager.nowPlayingModeNum = 1;
            pwd.GetComponent<WheelScript>().enabled = false;
            pwd.GetComponent<AutoMove>().enabled = true;

            pwod1.GetComponent<RootMotion.FinalIK.LookAtIK>().enabled = false;
            pwod1.GetComponent<Animator>().runtimeAnimatorController = null;

            pwod2.GetComponent<RootMotion.FinalIK.LookAtIK>().enabled = false;
            pwod2.GetComponent<Animator>().runtimeAnimatorController = null;
            pwod2.GetComponent<RootMotion.FinalIK.VRIK>().enabled = false;

            pwod3.GetComponent<RootMotion.FinalIK.LookAtIK>().enabled = false;
            pwod3.GetComponent<Animator>().runtimeAnimatorController = null;
            pwod2.GetComponent<RootMotion.FinalIK.VRIK>().enabled = false;

            //pwod.GetComponent<RootMotion.FinalIK.VRIK>().enabled = true;

            pwdView.SetActive(false);
            pwodView1.SetActive(true);
            pwodView2.SetActive(false);
            pwodView3.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
