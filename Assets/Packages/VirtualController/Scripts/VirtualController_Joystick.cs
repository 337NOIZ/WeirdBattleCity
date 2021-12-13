
using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

public class VirtualController_Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Space]
    
    [SerializeField] private RectTransform container = null;

    [SerializeField] private RectTransform handle = null;

    [Space]
    
    [SerializeField] private Vector2 _dragDirection;

    [Space]
    
    [SerializeField] private float dragRange = 75f;

    [Space]
    
    [SerializeField] private bool normalize = true;

    [Space]
    
    [SerializeField] private bool invertX = false;

    [SerializeField] private bool invertY = false;

    [Space] public UnityEvent<Vector2> onDrag;

    public Vector2 dragDirection { get { return _dragDirection; } }

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
        _dragDirection = new Vector2(pointerPosition.x / container.sizeDelta.x, pointerPosition.y / container.sizeDelta.y);

        if (normalize == true) _dragDirection = _dragDirection.normalized;

        if (invertX) _dragDirection.x = -_dragDirection.x;

        if (invertY) _dragDirection.y = -_dragDirection.y;

        onDrag.Invoke(_dragDirection);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _dragDirection = Vector2.zero;

        onDrag.Invoke(Vector2.zero);

        if (handle)
        {
            handle.anchoredPosition = Vector2.zero;
        }
    }
}