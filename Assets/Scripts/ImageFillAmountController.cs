
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
        if(_fill != null)
        {
            StopCoroutine(_fill);
        }

        _fill = _Fill(targetFillAmount, fillSpeed);

        StartCoroutine(_fill);
    }

    public IEnumerator _fill = null;

    public IEnumerator _Fill(float targetFillAmount, float fillSpeed)
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
            float time = Time.time;

            while (_fillAmount != targetFillAmount)
            {
                fillAmount = Mathf.SmoothStep(_fillAmount, targetFillAmount, (Time.time - time) * fillSpeed);

                yield return null;
            }
        }

        fillAmount = targetFillAmount;

        _fill = null;
    }
}