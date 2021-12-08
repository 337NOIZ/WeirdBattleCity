
using UnityEngine;

using UnityEngine.Events;

using UnityEngine.EventSystems;

public class EventTriggerWizard : MonoBehaviour
{
    private EventTrigger eventTrigger;

    private UnityEvent onPointerDown = new UnityEvent();

    private UnityEvent onPointerUp = new UnityEvent();

    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;

        entry.callback.AddListener(call => OnPointerDown());

        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerUp;

        entry.callback.AddListener(call => OnPointerUp());

        eventTrigger.triggers.Add(entry);
    }

    public void AddListener(EventTriggerType eventTriggerType, UnityAction unityAction)
    {
        switch(eventTriggerType)
        {
            case EventTriggerType.PointerDown:

                onPointerDown.AddListener(unityAction);

                break;

            case EventTriggerType.PointerUp:

                onPointerUp.AddListener(unityAction);

                break;

            default:

                break;
        }
    }

    private void OnPointerDown()
    {
        if(onPointerDown != null)
        {
            onPointerDown.Invoke();
        }
    }

    private void OnPointerUp()
    {
        if (onPointerUp != null)
        {
            onPointerUp.Invoke();
        }
    }
}