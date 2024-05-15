using UnityEngine;

using UnityEngine.UI;

public sealed class VirtualController_Button : MonoBehaviour
{
    [SerializeField] private Image image_SkillIcon = null;

    [SerializeField] private Image image_Gauge = null;

    [SerializeField] private Text text_Timer = null;

    public Sprite skillIcon
    {
        set
        {
            image_SkillIcon.sprite = value;
        }
    }

    public float gaugeFillAmount
    {
        get => image_Gauge.fillAmount;

        set
        {
            image_Gauge.fillAmount = value;
        }
    }

    public float timer
    {
        set
        {
            text_Timer.text = string.Format("{0:0}", value);
        }
    }
}