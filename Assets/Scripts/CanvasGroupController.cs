
using System.Collections;

using UnityEngine;

public class CanvasGroupController : MonoBehaviour
{
    private CanvasGroup canvasGroup = null;

    public float alpha
    {
        get
        {
            return canvasGroup.alpha;
        }

        set
        {
            canvasGroup.alpha = value;
        }
    }

    public bool interactable
    {
        get
        {
            return canvasGroup.interactable;
        }

        set
        {
            canvasGroup.interactable = value;
        }
    }

    public bool blocksRaycasts
    {
        get
        {
            return canvasGroup.blocksRaycasts;
        }

        set
        {
            canvasGroup.blocksRaycasts = value;
        }
    }

    public bool ignoreParentGroups
    {
        get
        {
            return canvasGroup.ignoreParentGroups;
        }

        set
        {
            canvasGroup.ignoreParentGroups = value;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        interactable = interactable;
    }

    public void SetActivation(bool setActivationValue, float lapTime)
    {
        if (_setActivation != null)
        {
            StopCoroutine(_setActivation);
        }

        _setActivation = _SetActivation(setActivationValue, lapTime);

        StartCoroutine(_setActivation);
    }

    private IEnumerator _setActivation = null;

    private IEnumerator _SetActivation(bool setActivationValue, float lapTime)
    {
        float targetAlpha = 0f;

        if (setActivationValue == true)
        {
            blocksRaycasts = setActivationValue;

            targetAlpha = 1f;
        }

        else
        {
            interactable = setActivationValue;
        }

        if (lapTime > 0f)
        {
           yield return FadeAlpha(targetAlpha, lapTime);
        }
        
        else
        {
            alpha = targetAlpha;
        }

        if (setActivationValue == true)
        {
            interactable = setActivationValue;
        }

        else
        {
            blocksRaycasts = setActivationValue;
        }

        _setActivation = null;
    }

    public IEnumerator FadeAlpha(float targetAlpha, float lapTime)
    {
        if (_fadeAlpha != null)
        {
            StopCoroutine(_fadeAlpha);

            _fadeAlpha = null;

            yield return null;
        }

        _fadeAlpha = _FadeAlpha(targetAlpha, lapTime);

        StartCoroutine(_fadeAlpha);

        while(_fadeAlpha != null)
        {
            yield return null;
        }
    }

    private IEnumerator _fadeAlpha = null;

    private IEnumerator _FadeAlpha(float targetAlpha, float lapTime)
    {
        if(targetAlpha < 0f)
        {
            targetAlpha = 0f;
        }

        else if (targetAlpha > 1f)
        {
            targetAlpha = 1f;
        }

        if (lapTime > 0f)
        {
            float maxDelta = (alpha >= targetAlpha ? alpha - targetAlpha : targetAlpha - alpha) / lapTime;

            while (true)
            {
                alpha = Mathf.MoveTowards(alpha, targetAlpha, maxDelta * Time.deltaTime);

                if (alpha == targetAlpha) break;

                yield return null;
            }
        }

        else
        {
            alpha = targetAlpha;
        }

        _fadeAlpha = null;
    }
}