
using System.Collections;

using UnityEngine;

using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform lever = null;

    [SerializeField] private float leverDragLimit = 150;

    private RectTransform rectTransform;

    public bool isPressed { get; private set; }

    public delegate void Vector2Delegate(Vector2 vector2);

    public event Vector2Delegate onDragDelegate;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        drag = Drag(eventData);

        StartCoroutine(drag);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(drag);

        drag = null;

        lever.anchoredPosition = Vector2.zero;
    }

    private IEnumerator drag;

    private IEnumerator Drag(PointerEventData eventData)
    {
        while (true)
        {
            var inputPosition = eventData.position - rectTransform.anchoredPosition;

            inputPosition = inputPosition.magnitude < leverDragLimit ? inputPosition : inputPosition.normalized * leverDragLimit;

            lever.anchoredPosition = inputPosition;

            if (onDragDelegate != null)
            {
                onDragDelegate(inputPosition / leverDragLimit);
            }

            yield return null;
        }
    }
}