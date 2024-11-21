using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;


public class ManageLookIK : MonoBehaviour
{
    WaitForSeconds delay2 = new WaitForSeconds(2f);
    WaitForSeconds delay3 = new WaitForSeconds(3f);

    private bool shouldRepeatFunction1 = true;
    private bool shouldRepeatFunction2 = true;
    private float function1Timer = 0f;
    private float function2Timer = 0f;
    private bool enableAnim = true;

    public Transform lookTarget;
    private Transform objTarget;
    private float lookDistance = 5f;   // 4
    private float audioDistance = 4f;  
    private float rotateDistance = 2.5f; // 2.5
    private float closeDistance = 2.5f;  // 2.5
    public SkinnedMeshRenderer body;

    private Mesh bodyMesh;

    private float distanceToPlayer;
 
    private Vector3 theLookAtPosition;
    private LookAtIK ikLookAt;
    
    
    int faceMorphIndex;

    private float lookWeight;
    private float faceWeight;
    private float animWeight = 0f;

    private float lookSmoother = 2f;
    private float faceSmoother = 1.2f;

    private string emotionType;
    private int emotionNum;

    private AudioSource audioPlay;
    private bool audioCheck = false;

    bool isWalked = false;
    bool isWalked1 = false;
    bool isWalked2 = false;

    private bool animWeightUp = true;
    bool isClose = false;
    private bool outDistance = false;

    int[] negBag;
    int[] posBag;

    GameObject objPivot;

    private Quaternion objectRot;

    private Animator animator;

    [HideInInspector] public Vector3 speed = Vector3.zero;

    void Awake()
    {
        bodyMesh = body.sharedMesh;

        

        if (gameObject.GetComponent<AudioSource>() != null)
        {
            audioPlay = gameObject.GetComponent<AudioSource>();
            audioCheck = true;
        }

        ikLookAt = GetComponent<LookAtIK>();
        

        if(GetComponent<Animator>() != null) animator = gameObject.GetComponent<Animator>();
        else animator = gameObject.GetComponentInChildren<Animator>();

        negBag = new int[] { 1, 3, 4, 5 };
        posBag = new int[] { 1, 2, 3, 4 };


        /*lookAtController.target = MainManager.mainCam;

        lookTarget = MainManager.mainCam;*/



    }

    private void Start()
    {
        ikLookAt.solver.target = MainManager.mainCam;
        lookTarget = ikLookAt.solver.target;
        objTarget = MainManager.objTarget;
        ikLookAt.solver.IKPositionWeight = 0f;

        objectRot = gameObject.transform.rotation;
        //Debug.Log(objectRot);

        if (MainManager.nowEmotion.Equals("negative")) emotionType = "neg";
        else if (MainManager.nowEmotion.Equals("positive")) emotionType = "pos";
        else emotionType = "neutral";


        if (emotionType.Equals("neg"))
        {
            
            emotionNum = Random.Range(0, 4);
            faceMorphIndex = bodyMesh.GetBlendShapeIndex(emotionType + negBag[emotionNum].ToString());

        }

        else if(emotionType.Equals("pos"))
        {
            
            emotionNum = Random.Range(0, 4);
            faceMorphIndex = bodyMesh.GetBlendShapeIndex(emotionType + posBag[emotionNum].ToString());
            //Debug.Log(gameObject.name + "의 값 : " + faceMorphIndex);

        }

        ikLookAt.solver.headWeight = 0.6f;
        ikLookAt.solver.clampWeightEyes = 0.6f;

        //animator.applyRootMotion = false;

        objPivot = new GameObject("DummyPivot");
        objPivot.transform.parent = transform;
        objPivot.transform.localPosition = new Vector3(0, 0, 0);

        /*targetPos = new GameObject().GetComponent<Transform>();
        targetPos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);*/


        //else neutral = true;
    }

    void Update()
    {
        if(!emotionType.Equals("neutral"))
        {
            // Only check the distance if the Player is alive

            distanceToPlayer = Vector3.Distance(objTarget.position, transform.position);

            

            // Only look at the player if within lookdistance
            if (distanceToPlayer < lookDistance)
            {
                Vector3 targetPostition = new Vector3(objTarget.position.x,
                                        objPivot.transform.position.y,
                                        objTarget.position.z);

                objPivot.transform.LookAt(targetPostition);

                float pivotRotY = objPivot.transform.localRotation.y;


                if (pivotRotY < 0.69f && pivotRotY > -0.69f)
                {

                    theLookAtPosition = lookTarget.position;
                    ikLookAt.solver.IKPosition = theLookAtPosition;

                    lerpWieghtUp();

                    ikLookAt.solver.IKPositionWeight = lookWeight;
                    body.SetBlendShapeWeight(faceMorphIndex, (faceWeight * 100));
                }

                else lerpWieghtdown();
                
                if (distanceToPlayer < audioDistance && audioCheck)
                {
                    audioPlay.Play();   
                    audioCheck = false;
                }
            }

            else if (distanceToPlayer > lookDistance)
            {
                //animator.applyRootMotion = false;
                lerpWieghtdown();
                ikLookAt.solver.IKPositionWeight = lookWeight;

                body.SetBlendShapeWeight(faceMorphIndex, (faceWeight * 100));
            }
        }

        else
        {
            distanceToPlayer = Vector3.Distance(objTarget.position, transform.position);

            if (distanceToPlayer < audioDistance && audioCheck)
            {
                audioPlay.Play();
                audioCheck = false;
            }
        }
    }



    void lerpWieghtUp()
    {
        if(faceWeight < 1.1f)
        {
            lookWeight = Mathf.Lerp(lookWeight, 1.0f, Time.deltaTime * lookSmoother);
            faceWeight = Mathf.Lerp(faceWeight, 1.3f, Time.deltaTime * faceSmoother);
        }
        
        //StartCoroutine(FaceWeightUp());
    }

    void lerpWieghtdown()
    {
        if(faceWeight > 0.05f)
        {
            lookWeight = Mathf.Lerp(lookWeight, 0.0f, Time.deltaTime * lookSmoother);
            faceWeight = Mathf.Lerp(faceWeight, 0.0f, Time.deltaTime * faceSmoother);
        }
        
    }

    /*void AnimWeight()
    {
        if (animWeightUp)
        {
            animWeight = Mathf.Lerp(animWeight, 0.3f, 0.08f); //animWeight = Mathf.Lerp(animWeight, 0.3f, Time.deltaTime * 1.5f);
            animator.SetLayerWeight(1, animWeight);
        }
        else
        {
            animWeight = Mathf.Lerp(animWeight, 0f, 0.08f);
            animator.SetLayerWeight(1, animWeight);
        }
    }*/

    /*private void AnimateWalk()
    {
        //Debug.Log("함수 실행");
        animWeightUp = true;
        isClose = true;

        animWeight = Mathf.Lerp(animWeight, 0.4f, 0.2f);
        animator.SetLayerWeight(1, animWeight);

        if (enableAnim)
        {
            animator.applyRootMotion = true;
            if (emotionType.Equals("neg")) animator.CrossFade("Walk Backwards", 0.5f, 1);
            else if (emotionType.Equals("pos")) animator.CrossFade("Walk 2", 0.5f, 1);
            enableAnim = false;
            //Debug.Log("걷기 애니메이션 실행");
        }
        

        //isWalked1 = true;
    }

    private void DeAnimateWalk()
    {
        //Debug.Log("두번째 함수 실행");
        animWeightUp = false;

        //AnimWeight();
        animWeight = Mathf.Lerp(animWeight, 0f, Time.deltaTime);
        animator.SetLayerWeight(1, animWeight);
        animator.applyRootMotion = false;
        

        if (!enableAnim)
        {
            //Debug.Log("걷기 애니메이션 종료");
            animator.CrossFade("idle1", 1.5f, 1);
            enableAnim = true;
        }
        

        //animator.SetLayerWeight(1, 0);

        *//*animator.applyRootMotion = false;

        isClose = false;
        isWalked2 = true;*//*
    }*/

    /*private IEnumerator AnimateWalk()
    {
        if(!isWalked)
        {
            
            animWeightUp = true;
            isClose = true;
            
            animator.applyRootMotion = true;

            animWeight = Mathf.Lerp(animWeight, 0.35f, 0.8f);
            

            if (emotionType.Equals("neg")) animator.CrossFade("Walk Backwards", 0.1f, 1);
            else if (emotionType.Equals("pos")) animator.CrossFade("Walk 2", 0.1f, 1);

            yield return delay3;
            
            isWalked1 = true;

            StartCoroutine("DeAnimateWalk");
        }
        
    }

    private IEnumerator DeAnimateWalk()
    {
        if (isWalked && isWalked1)
        {
            animWeightUp = false;
            animWeight = Mathf.Lerp(animWeight, 0f, 0.6f);
            
            animator.SetLayerWeight(1, animWeight);

            yield return delay2;
            
            animator.CrossFade("idle1", 0.5f, 1);
            
            
            //animator.SetLayerWeight(1, 0);
            
            
            
            animator.applyRootMotion = false;

            isClose = false;
            isWalked2 = true;
        }

    }*/
    /*private IEnumerator AnimateTurn(string loc)
    {
        if (!isTurned)
        {
            animWeightUp = true;
            animator.applyRootMotion = true;
            yield return new WaitForSecondsRealtime(1f);
            //animator.SetLayerWeight(1, 1f);
            animator.Play("Start Quarter Turn " + loc, 1);
            //animator.CrossFade("Stand Quarter Turn " + loc, 0.1f, 1, Random.Range(0.0f, 1.0f));
            Debug.Log("애니메이트 턴");
            

            if (!isTurned1)
            {
                
                isTurned1 = true;
            }

            yield return new WaitForSecondsRealtime(1f);

            isTurned = true;
        }

    }*/

    /*private IEnumerator FaceWeightUp()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        faceWeight = Mathf.Lerp(lookWeight, 1.2f, Time.deltaTime * faceSmoother);
    }*/


    public void SetComponent()
    {
        var bodyObject = gameObject.transform.Find("CC_Base_Body").GetComponent<SkinnedMeshRenderer>();
        body = bodyObject;
    }
}
