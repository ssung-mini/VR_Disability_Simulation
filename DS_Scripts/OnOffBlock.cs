using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffBlock : MonoBehaviour
{
    public GameObject[] blocks_1;

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
            for(int i=0; i<blocks_1.Length; i++)
            {
                if (blocks_1[i].activeSelf) blocks_1[i].SetActive(false);
                else blocks_1[i].SetActive(true);
            }

            gameObject.SetActive(false);
        }
    }
}
