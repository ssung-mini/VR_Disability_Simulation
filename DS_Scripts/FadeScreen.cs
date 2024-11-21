using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
            FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    /// /////////////////////////////////////////////////////////////////////////////////////////////////

/*    public void FadeIn2()
    {
        Fade2(1, 0);
    }

    public void FadeOut2()
    {
        Fade2(0, 1);
    }

    public void FadeIn3()
    {
        Fade3(1, 0);
    }

    public void FadeOut3()
    {
        Fade3(0, 1);
    }*/

    

    /// /////////////////////////////////////////////////////////////////////////////////////////////////

    public void Fade(float alphaIn, float alphaOut)
    {
        if(alphaIn == 1)
        {
            StartCoroutine(FadeInRoutine(alphaIn, alphaOut));
        }

        else
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeOutRoutine(alphaIn, alphaOut));
        }
            

    }

    public IEnumerator FadeInRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);

        CSVWritingManager.End_Timer();
        StartCoroutine(CSVWritingManager.GameClearQuit());
    }

    
    /// /////////////////////////////////////////////////////////////////////////////////////////////////
    

    /*public void Fade2(float alphaIn, float alphaOut)
    {
        if (alphaIn == 1)
        {
            StartCoroutine(FadeInRoutine2(alphaIn, alphaOut));
        }

        else
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeOutRoutine2(alphaIn, alphaOut));
        }


    }

    public IEnumerator FadeInRoutine2(float alphaIn, float alphaOut)
    {
        rend = FinishLine._pwod2Fade.gameObject.GetComponent<Renderer>();
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutRoutine2(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
            
        }
        
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);

        MainManager.nowPwod1 = false;
        MainManager.nowPwod2 = true;

        MainManager.nowPlayingMode = "PwoD2";
        MainManager.nowPlayingModeNum = 2;

        FinishLine._pwodViewL.SetActive(false);
        FinishLine._pwodViewM.SetActive(true);

        FinishLine._pwod1.GetComponent<VRIK>().enabled = false;
        FinishLine._pwod2.GetComponent<VRIK>().enabled = true;

        FinishLine._pwod2Fade.FadeIn2();

    }

    public void Fade3(float alphaIn, float alphaOut)
    {
        if (alphaIn == 1)
        {
            StartCoroutine(FadeInRoutine3(alphaIn, alphaOut));
        }

        else
        {
            gameObject.SetActive(true);
            StartCoroutine(FadeOutRoutine3(alphaIn, alphaOut));
        }


    }

    public IEnumerator FadeInRoutine3(float alphaIn, float alphaOut)
    {
        rend = FinishLine._pwod3Fade.gameObject.GetComponent<Renderer>();
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutRoutine3(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }
        
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);

        MainManager.nowPwod2 = false;
        MainManager.nowPwod3 = true;

        MainManager.nowPlayingMode = "PwoD3";
        MainManager.nowPlayingModeNum = 3;

        FinishLine._pwodViewM.SetActive(false);
        FinishLine._pwodViewR.SetActive(true);

        FinishLine._pwod2.GetComponent<VRIK>().enabled = false;
        FinishLine._pwod3.GetComponent<VRIK>().enabled = true;

        FinishLine._pwod3Fade.FadeIn3();

    }*/
}
