using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObstacle : MonoBehaviour
{
    public static int collisionNum;
    public static int hillCollisionNum;

    private void Start()
    {
        collisionNum = 0;
        hillCollisionNum = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") || collision.gameObject.layer == LayerMask.NameToLayer("Agent"))
        {
            ++collisionNum;
            Debug.Log("일반 : " +collisionNum);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("HillWall"))
        {
            ++hillCollisionNum;
            Debug.Log("경사로 : " + hillCollisionNum);
        }
    }
}
