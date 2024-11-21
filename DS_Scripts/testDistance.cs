using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDistance : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public float distanceThreshold = 5f;

    private bool shouldRepeatFunction1 = true;
    private bool shouldRepeatFunction2 = true;
    private float function1Timer = 0f;
    private float function2Timer = 0f;

    private void Update()
    {
        float distance = Vector3.Distance(object1.position, object2.position);

        if (distance <= distanceThreshold)
        {
            
            Function1();
        }
        

        if (distance <= distanceThreshold && shouldRepeatFunction1)
        {
            function1Timer += Time.deltaTime;
            if (function1Timer >= 3f)
            {
                
                shouldRepeatFunction1 = false;
                function1Timer = 0f;
            }
        }
        else if (distance <= distanceThreshold && !shouldRepeatFunction1 && shouldRepeatFunction2)
        {
            function2Timer += Time.deltaTime;
            Function2();
            if (function2Timer >= 2f)
            {
                shouldRepeatFunction2 = false;
                function2Timer = 0f;
            }
        }
    }

    private void Function1()
    {
        if (shouldRepeatFunction1)
        {
            // Function 1 내용 실행
            Debug.Log("Function 1");
        }
    }

    private void Function2()
    {
        // Function 2 내용 실행
        Debug.Log("Function 2");
    }
}