
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [Space]

    [SerializeField, Range(0, 1)] private int startFadeProgress = 0;

    public CanvasGroupController canvasGroupController { get; private set; } = null;

    public Image image { get; private set; } = null;

    public Texture baseMap
    {
        set
        {
            image.material.SetTexture("_baseMap", value);
        }
    }

    private float _fadeProgress;

    public float fadeProgress
    {
        get
        {
            return _fadeProgress;
        }

        set
        {
            _fadeProgress = value;

            if (value == 1)
            {
                image.raycastTarget = false;
            }

            else
            {
                image.raycastTarget = true;
            }

            image.material.SetFloat("_fadeProgress", value);
        }
    }

    private void Awake()
    {
        canvasGroupController = GetComponent<CanvasGroupController>();

        image = GetComponent<Image>();

        image.material = new Material(image.material);

        fadeProgress = startFadeProgress;
    }

    private IEnumerator fade = null;

    public IEnumerator Fade(string fadePatternName, float fadeSmoothness, float virtualFadeProgress, float targetFadeProgress, float routineTime)
    {
        if (fade != null)
        {
            StopCoroutine(fade);

            fade = null;

            yield return null;
        }

        fade = _Fade(fadePatternName, fadeSmoothness, virtualFadeProgress, targetFadeProgress, routineTime);

        StartCoroutine(fade);

        while (fade != null)
        {
            yield return null;
        }
    }

    public IEnumerator Fade(string fadePatternName, float fadeSmoothness, float targetFadeProgress, float routineTime)
    {
        yield return Fade(fadePatternName, fadeSmoothness, fadeProgress, targetFadeProgress, routineTime);
    }

    public IEnumerator Fade(float fadeSmoothness, float virtualFadeProgress, float targetFadeProgress, float routineTime)
    {
        yield return Fade(null, fadeSmoothness, virtualFadeProgress, targetFadeProgress, routineTime);
    }

    public IEnumerator Fade(float fadeSmoothness, float targetFadeProgress, float routineTime)
    {
        yield return Fade(null, fadeSmoothness, fadeProgress, targetFadeProgress, routineTime);
    }

    private IEnumerator _Fade(string fadePatternName, float fadeSmoothness, float virtualFadeProgress, float targetFadeProgress, float routineTime)
    {
        if(fadePatternName != null)
        {
            image.material.SetTexture("_fadePattern", FadeScreenManager.instance.fadePatterns[fadePatternName]);
        }

        if (fadeSmoothness > 2f)
        {
            fadeSmoothness = 2f;
        }

        else if(fadeSmoothness < 0f)
        {
            fadeSmoothness = 0f;
        }

        image.material.SetFloat("_fadeSmoothness", fadeSmoothness);

        if (virtualFadeProgress > 1f)
        {
            virtualFadeProgress = 1f;
        }

        else if (virtualFadeProgress < 0f)
        {
            virtualFadeProgress = 0f;
        }

        if (targetFadeProgress > 1f)
        {
            targetFadeProgress = 1f;
        }

        else if (targetFadeProgress < 0f)
        {
            targetFadeProgress = 0f;
        }

        image.raycastTarget = true;

        if (routineTime > 0f)
        {
            float maxDelta = (virtualFadeProgress > targetFadeProgress ? virtualFadeProgress - targetFadeProgress : targetFadeProgress - virtualFadeProgress) / routineTime;

            while (true)
            {
                fadeProgress = Mathf.MoveTowards(fadeProgress, targetFadeProgress, maxDelta * Time.deltaTime);

                if (fadeProgress == targetFadeProgress)
                {
                    break;
                }

                yield return null;
            }
        }

        else
        {
            fadeProgress = targetFadeProgress;
        }

        if (fadeProgress == 1f)
        {
            image.raycastTarget = false;
        }

        fade = null;
    }
}