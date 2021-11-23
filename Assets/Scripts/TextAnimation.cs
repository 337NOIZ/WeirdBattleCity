
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public sealed class TextAnimation : MonoBehaviour
{
    [Space]

    [SerializeField] private List<string> _textStrings = null;

    public List<string> textStrings
    {
        get
        {
            return _textStrings;
        }

        set
        {
            _textStrings = value;
        }
    }

    public Text text { get; private set; }

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SlideShow(bool loof, float latency)
    {
        StopSlideShow(0f);

        _slideShow = _SlideShow(loof, latency);

        StartCoroutine(_slideShow);
    }

    private IEnumerator _slideShow = null;

    private IEnumerator _SlideShow(bool loof, float latency)
    {
        string textString_Origin = text.text;

        int count = _textStrings.Count;

        if (count > 0)
        {
            for (int index = 0; ; ++index)
            {
                if (index >= count)
                {
                    if (loof == false) break;

                    index = 0;
                }

                text.text = textString_Origin + _textStrings[index];

                yield return new WaitForSeconds(latency);
            }
        }

        _slideShow = null;
    }

    public void StopSlideShow(float waitTime)
    {
        if(_stopSlideShow != null)
        {
            StopCoroutine(_stopSlideShow);
        }

        _stopSlideShow = _StopSlideShow(waitTime);

        StartCoroutine(_stopSlideShow);
    }

    private IEnumerator _stopSlideShow = null;

    private IEnumerator _StopSlideShow(float waitTime)
    {
        if (_slideShow != null)
        {
            if(waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }

            StopCoroutine(_slideShow);

            _slideShow = null;
        }
    }
}
