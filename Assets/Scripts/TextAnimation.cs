
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    [Space]

    [SerializeField]

    private List<string> textStringList = null;

    public Text text { get; private set; } = null;

    private string originalTextstring = null;

    private int textStringListCount = 0;

    [HideInInspector]

    public int index = 0;

    private void Awake()
    {
        text = GetComponent<Text>();

        originalTextstring = text.text;

        textStringListCount = textStringList.Count;
    }

    public void StartTextSlideShow(float latency, bool loof)
    {
        TryStopTextSlideShow();

        textSlideShowCoroutine = TextSlideShowCoroutine(latency, loof);

        StartCoroutine(textSlideShowCoroutine);
    }

    private IEnumerator textSlideShowCoroutine = null;

    private IEnumerator TextSlideShowCoroutine(float latency, bool loof)
    {
        while (true)
        {
            text.text = originalTextstring;

            text.text += textStringList[index++];

            if (index == textStringListCount)
            {
                if (loof == false)
                {
                    break;
                }
                
                index = 0;
            }

            yield return new WaitForSeconds(latency);
        }

        textSlideShowCoroutine = null;
    }

    public bool TryStopTextSlideShow(float initialLatency)
    {
        if(textSlideShowCoroutine != null)
        {
            StartCoroutine(StopTextSlideShowCoroutine(initialLatency));

            return true;
        }

        return false;
    }

    public void TryStopTextSlideShow()
    {
        TryStopTextSlideShow(0f);
    }

    private IEnumerator StopTextSlideShowCoroutine(float initialLatency)
    {
        if(initialLatency > 0)
        {
            yield return new WaitForSeconds(initialLatency);
        }

        StopCoroutine(textSlideShowCoroutine);

        textSlideShowCoroutine = null;
    }
}
