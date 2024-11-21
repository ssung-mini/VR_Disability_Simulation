using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioClipBag : MonoBehaviour
{
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public AudioClip neutralSound;
    public AudioSource audioSource;

    private string emotionType;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //emotionType = MainManager.nowEmotion;

        
        audioSource.spatialBlend = 0.95f;
        audioSource.minDistance = 2f;
        audioSource.maxDistance = 8f;

        if (MainManager.nowEmotion.Equals("negative"))
        {
            audioSource.clip = negativeSound;
            audioSource.volume = 1f;
        }

        else if (MainManager.nowEmotion.Equals("positive"))
        {
            audioSource.clip = positiveSound;
            audioSource.volume = 1f;
        }

        else if (MainManager.nowEmotion.Equals("neutral"))
        {
            audioSource.clip = neutralSound;
            audioSource.volume = 0.75f;
        }

        //else audioSource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
