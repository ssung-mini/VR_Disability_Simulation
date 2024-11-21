using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWalkCollider : MonoBehaviour
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
        if(other.CompareTag("Player"))
        {
            walkingAgent.wcTrigger = true;

            Animator agentAnim = walkingAgent.gameObject.GetComponent<Animator>();
            //agentAnim.CrossFade("Walk 2", 0.8f, 2);
            agentAnim.applyRootMotion = true;
        }
    }
}
