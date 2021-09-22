
using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Space]

    [SerializeField] private RectTransform container = null;

    [SerializeField] private RectTransform handle = null;

    [Space]

    [SerializeField] private float dragRange = 75f;

    public bool invertX = false;

    public bool invertY = false;

    [Space]

    public UnityEvent<Vector2> onDrag;

    void Start()
    {
        if (handle)
        {
            handle.anchoredPosition = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out Vector2 pointerPosition);

        if (handle)
        {
            handle.anchoredPosition = pointerPosition.magnitude < dragRange ? pointerPosition : pointerPosition.normalized * dragRange;
        }

        var dragDirection = new Vector2(pointerPosition.x / container.sizeDelta.x, pointerPosition.y / container.sizeDelta.y);

        onDrag.Invoke(ApplyInversionFilter(dragDirection));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onDrag.Invoke(Vector2.zero);

        if(handle)
        {
            handle.anchoredPosition = Vector2.zero;
        }
    }

    Vector2 ApplyInversionFilter(Vector2 position)
    {
        if(invertX)
        {
            position.x = -position.x;
        }

        if(invertY)
        {
            position.y = -position.y;
        }

        return position;
    }
}