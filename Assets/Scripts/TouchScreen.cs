
using UnityEngine;

using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour, IDragHandler
{
    public delegate void Delegate();

    public event Delegate onDragDelegate = null;

    public void OnDrag(PointerEventData eventData)
    {
        if(onDragDelegate != null)
        {
            onDragDelegate();
        }
    }
}
