
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public class ImageFillAmountController : MonoBehaviour
{
    [SerializeField] private Image _image = null;

    private float _fillAmount;

    public float fillAmount
    {
        set
        {
            _fillAmount = value;

            _image.fillAmount = _fillAmount;
        }
    }

    public IEnumerator FillByLerp(float targetFillAmount, float fillSpeed)
    {
        if(_fillByLerp_ != null)
        {
            StopCoroutine(_fillByLerp_);

            _fillByLerp_ = null;

            yield return null;
        }

        _fillByLerp_ = _FillByLerp_(targetFillAmount, fillSpeed);

        StartCoroutine(_fillByLerp_);

        while (_fillByLerp_ != null) yield return null;
    }

    private IEnumerator _fillByLerp_ = null;

    private IEnumerator _FillByLerp_(float targetFillAmount, float fillSpeed)
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

            IEnumerator Fill()
            {
                time += Time.deltaTime;

                fillAmount = Mathf.Lerp(_fillAmount, targetFillAmount, time * fillSpeed);

                yield return null;
            }

            float targetFillAmount_Adjusted = targetFillAmount;

            if (_fillAmount < targetFillAmount)
            {
                targetFillAmount_Adjusted -= 0.001f;

                while (_fillAmount < targetFillAmount_Adjusted) yield return Fill();
            }

            else if(_fillAmount > targetFillAmount)
            {
                targetFillAmount_Adjusted += 0.001f;

                while (_fillAmount > targetFillAmount_Adjusted) yield return Fill();
            }
        }

        fillAmount = targetFillAmount;

        _fillByLerp_ = null;
    }
}