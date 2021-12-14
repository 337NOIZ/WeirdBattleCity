
using System.Collections;

using UnityEngine;

public sealed class CanvasGroupController : MonoBehaviour
{
    public CanvasGroup canvasGroup { get; private set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeAlpha(float virtualCurrentAlpha, float targetAlpha, float fadeTime)
    {
        if (_fadeAlpha != null)
        {
            StopCoroutine(_fadeAlpha);

            _fadeAlpha = null;

            yield return null;
        }

        _fadeAlpha = FadeAlpha_(virtualCurrentAlpha, targetAlpha, fadeTime);

        StartCoroutine(_fadeAlpha);

        while (_fadeAlpha != null) yield return null;
    }

    private IEnumerator _fadeAlpha = null;

    private IEnumerator FadeAlpha_(float virtualCurrentAlpha, float targetAlpha, float fadeTime)
    {
        if(targetAlpha < 0f)
        {
            targetAlpha = 0f;
        }

        else if (targetAlpha > 1f)
        {
            targetAlpha = 1f;
        }

        if (fadeTime > 0f)
        {
            float maxDelta = (virtualCurrentAlpha >= targetAlpha ? virtualCurrentAlpha - targetAlpha : targetAlpha - virtualCurrentAlpha) / fadeTime;

            while (canvasGroup.alpha != targetAlpha)
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, maxDelta * Time.deltaTime);

                yield return null;
            }
        }

        else
        {
            canvasGroup.alpha = targetAlpha;
        }

        _fadeAlpha = null;
    }
}