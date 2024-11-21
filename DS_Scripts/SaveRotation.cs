using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRotation : MonoBehaviour
{
    private Transform objectRot;

    // Start is called before the first frame update
    void Start()
    {
        objectRot = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ReturnRotation()
    {
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, objectRot.transform.rotation, Time.deltaTime * 1.0f);
        
        yield return new WaitForSecondsRealtime(1);
    }
}
