
using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

public class VirtualTouchpad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Space]

    [SerializeField] private RectTransform container = null;

    [Space]

    [SerializeField] private float dragSensitivity = 0.5f;

    [SerializeField] private bool clampMagnitude = true;

    public float maxMagnitude = 200f;

    public bool invertX = false;

    public bool invertY = true;

    [Space]

    public UnityEvent<Vector2> onDrag;

    private Vector2 pointerDownPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out Vector2 pointerDragPosition);

        var dragDirection = (pointerDragPosition - pointerDownPosition) * dragSensitivity;

        if(clampMagnitude) dragDirection = Vector2.ClampMagnitude(dragDirection, maxMagnitude);

        if (invertX) dragDirection.x = -dragDirection.x;

        if (invertY) dragDirection.y = -dragDirection.y;

        onDrag.Invoke(dragDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onDrag.Invoke(Vector2.zero);
    }
}