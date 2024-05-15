using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

public sealed class VirtualController_TouchScreen : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform container = null;

    [SerializeField] private Vector2 _dragDirection;

    [SerializeField] private float dragSensitivity = 0.5f;

    [SerializeField] private float maxMagnitude = 200f;

    [SerializeField] private bool invertX = false;

    [SerializeField] private bool invertY = true;

    public UnityEvent<Vector2> onDrag;

    public Vector2 dragDirection { get { return _dragDirection; } }

    private Vector2 pointerDownPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(container, eventData.position, eventData.pressEventCamera, out Vector2 pointerDragPosition);

        _dragDirection = Vector2.ClampMagnitude((pointerDragPosition - pointerDownPosition) * dragSensitivity, maxMagnitude);

        if (invertX) _dragDirection.x = -dragDirection.x;

        if (invertY) _dragDirection.y = -dragDirection.y;

        onDrag.Invoke(_dragDirection);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _dragDirection = Vector2.zero;

        onDrag.Invoke(Vector2.zero);
    }
}