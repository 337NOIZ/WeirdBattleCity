
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public class ImageFillAmountController : MonoBehaviour
{
    [SerializeField] private Image _image = null;

    private float _fillAmount_;

    public float fillAmount
    {
        set
        {
            _fillAmount_ = value;

            _image.fillAmount = _fillAmount_;
        }
    }

    public void StartFillByLerp(float targetFillAmount, float fillSpeed)
    {
        if (fillByLerp != null)
        {
            StopFillByLerp();
        }

        fillByLerp = _FillByLerp(targetFillAmount, fillSpeed);

        StartCoroutine(fillByLerp);
    }

    public void StopFillByLerp()
    {
        if (fillByLerp != null)
        {
            StopCoroutine(fillByLerp);

            fillByLerp = null;
        }
    }

    public IEnumerator fillByLerp { get; private set; } = null;

    private IEnumerator _FillByLerp(float targetFillAmount, float fillSpeed)
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

                fillAmount = Mathf.Lerp(_fillAmount_, targetFillAmount, time * fillSpeed);

                yield return null;
            }

            float targetFillAmount_Adjusted = targetFillAmount;

            if (_fillAmount_ < targetFillAmount)
            {
                targetFillAmount_Adjusted -= 0.001f;

                while (_fillAmount_ < targetFillAmount_Adjusted) yield return Fill();
            }

            else if(_fillAmount_ > targetFillAmount)
            {
                targetFillAmount_Adjusted += 0.001f;

                while (_fillAmount_ > targetFillAmount_Adjusted) yield return Fill();
            }
        }

        fillAmount = targetFillAmount;

        fillByLerp = null;
    }
}