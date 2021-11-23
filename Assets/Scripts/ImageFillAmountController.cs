
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public class ImageFillAmountController : MonoBehaviour
{
    [Space]

    [SerializeField] private Image image = null;

    [Space]

    [SerializeField, Range(0f, 1f)] private float _fillAmount = 0f;

    public float fillAmount
    {
        get
        {
            return image.fillAmount;
        }

        set
        {
            _fillAmount = value;

            image.fillAmount = _fillAmount;
        }
    }

    private void Awake()
    {
        fillAmount = _fillAmount;
    }

    public void Fill(float targetFillAmount, float fillSpeed)
    {
        if(fillRoutine != null)
        {
            StopCoroutine(fillRoutine);
        }

        fillRoutine = FillRoutine(targetFillAmount, fillSpeed);

        StartCoroutine(fillRoutine);
    }

    public IEnumerator fillRoutine = null;

    public IEnumerator FillRoutine(float targetFillAmount, float fillSpeed)
    {
        if(targetFillAmount > 1f)
        {
            targetFillAmount = 1f;
        }
        
        else if(targetFillAmount < 0f)
        {
            targetFillAmount = 0f;
        }

        if (fillSpeed > 0f)
        {
            float time = 0f;

            while (_fillAmount != targetFillAmount)
            {
                time += Time.deltaTime;

                fillAmount = Mathf.Lerp(_fillAmount, targetFillAmount, time * fillSpeed);

                Debug.Log(_fillAmount);

                //fillAmount = Mathf.SmoothStep(_fillAmount, targetFillAmount, (Time.time - time) * fillSpeed);

                yield return null;
            }
        }

        fillAmount = targetFillAmount;

        fillRoutine = null;
    }
}