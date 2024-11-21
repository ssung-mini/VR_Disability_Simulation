using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffCanvas : MonoBehaviour
{
    public static GameObject panel;
    public static GameObject startText;
    public static GameObject endText;

    public GameObject _panel;
    public GameObject _startText;
    public GameObject _endText;
    // Start is called before the first frame update
    void Start()
    {
        panel = _panel;
        startText = _startText;
        endText = _endText;
    }

    public static void StartCanvas()
    {
        panel.SetActive(false);
        startText.SetActive(false);
    }

    public static void EndCanvas()
    {
        panel.SetActive(true);
        endText.SetActive(true);
    }
}
