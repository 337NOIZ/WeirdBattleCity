
using System.Collections;

using UnityEngine;

public sealed class CanvasGroupController : MonoBehaviour
{
    public CanvasGroup canvasGroup { get; private set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeAlpha(float virtualAlpha, float targetAlpha, float fadeTime)
    {
        if (_fadeAlpha != null)
        {
            StopCoroutine(_fadeAlpha);

            _fadeAlpha = null;

            yield return null;
        }

        _fadeAlpha = _FadeAlpha(virtualAlpha, targetAlpha, fadeTime);

        StartCoroutine(_fadeAlpha);

        while (_fadeAlpha != null) yield return null;
    }

    private IEnumerator _fadeAlpha = null;

    private IEnumerator _FadeAlpha(float virtualAlpha, float targetAlpha, float fadeTime)
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
            float maxDelta = (virtualAlpha >= targetAlpha ? virtualAlpha - targetAlpha : targetAlpha - virtualAlpha) / fadeTime;

            while (canvasGroup.alpha == targetAlpha)
            {
                yield return null;

                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, maxDelta * Time.deltaTime);
            }
        }

        else
        {
            canvasGroup.alpha = targetAlpha;
        }

        _fadeAlpha = null;
    }
}