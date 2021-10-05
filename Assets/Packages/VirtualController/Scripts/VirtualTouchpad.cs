
using UnityEngine;

using UnityEngine.EventSystems;

using UnityEngine.Events;

namespace VirtualController
{
    public class VirtualTouchpad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Space, SerializeField] private RectTransform container = null;

        [Space, SerializeField] private Vector2 _dragDirection;

        public Vector2 dragDirection
        {
            get
            {
                return _dragDirection;
            }
        }

        [Space, SerializeField] private float dragSensitivity = 0.5f;

        [Space, SerializeField] private float maxMagnitude = 200f;

        [Space, SerializeField] private bool invertX = false;

        [SerializeField] private bool invertY = true;

        [Space] public UnityEvent<Vector2> onDrag;

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
}