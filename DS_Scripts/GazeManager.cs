using System.Collections;
using System.Collections.Generic;
using Tobii.XR.Examples.DevTools;
using UnityEngine;

public class GazeManager : MonoBehaviour
{
    [SerializeField] public GazeTarget[] _headCollider;
    [SerializeField] public GazeTarget[] _bodyCollider;

    public static GazeTarget[] headCollider;
    public static GazeTarget[] bodyCollider;

    public static float headGazeTime;
    public static float bodyGazeTime;

    // Start is called before the first frame update
    void Start()
    {
        headGazeTime = 0f;
        bodyGazeTime = 0f;

        headCollider = _headCollider;
        bodyCollider = _bodyCollider;

        UnableCollider();
    }

    public static void SummingGazeTime()
    {
        for(int i = 0; i < headCollider.Length; i++)
        {
            headGazeTime = headGazeTime + headCollider[i].gazeTime;
        }

        for (int i = 0; i < bodyCollider.Length; i++)
        {
            bodyGazeTime = bodyGazeTime + bodyCollider[i].gazeTime;
        }
    }

    public static float GetHeadGazeTime()
    {
        return headGazeTime;
    }

    public static float GetBodyGazeTime()
    {
        return bodyGazeTime;
    }

    public static void EnableCollider()
    {
        for (int i = 0; i < headCollider.Length; i++)
        {
            headCollider[i].enabled = true;
        }

        for (int i = 0; i < bodyCollider.Length; i++)
        {
            bodyCollider[i].enabled = true;
        }
    }

    public static void UnableCollider()
    {
        for (int i = 0; i < headCollider.Length; i++)
        {
            headCollider[i].enabled = false;
        }

        for (int i = 0; i < bodyCollider.Length; i++)
        {
            bodyCollider[i].enabled = false;
        }
    }
}
