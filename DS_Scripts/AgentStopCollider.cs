using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStopCollider : MonoBehaviour
{
    public WalkingLookIK walkingAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WalkingAgent") || other.CompareTag("Security"))
        {
            walkingAgent.stopTrigger = true;

            Animator agentAnim = walkingAgent.gameObject.GetComponent<Animator>();
            agentAnim.CrossFade("idle1", 1f, 2);
            agentAnim.applyRootMotion = false;
        }
    }
}
