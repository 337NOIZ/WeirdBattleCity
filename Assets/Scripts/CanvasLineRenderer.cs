
using UnityEngine;

using UnityEngine.UI;

public sealed class CanvasLineRenderer : MonoBehaviour
{
    public new Transform transform { get; private set; } = null;

    public RectTransform rectTransform { get; private set; } = null;

    public Image image { get; private set; } = null;

    public Discolorator discolorator { get; private set; } = null;

    public Vector3 canvasLineEndLocalPosition
    {
        set
        {
            transform.localScale = new Vector2(Vector3.Distance(value, transform.localPosition), 1f);

            transform.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(value.y - transform.localPosition.y, value.x - transform.localPosition.x) * 180 / Mathf.PI);
        }
    }

    public float canvasLineThickness
    {
        get
        {
            return rectTransform.rect.height;
        }

        set
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, value);
        }
    }

    public Color canvasLineColor
    {
        get
        {
            return image.color;
        }

        set
        {
            image.color = value;
        }
    }

    private void Awake()
    {
        transform = GetComponent<Transform>();

        rectTransform = GetComponent<RectTransform>();

        image = GetComponent<Image>();

        discolorator = GetComponent<Discolorator>();
    }

    public void Initialize()
    {

    }
}