
using UnityEngine;

using UnityEngine.Events;

public sealed class AnimationTools : MonoBehaviour
{
    public static float FrameCountToSeconds(int frameCount) { return 1f / 60 * frameCount; }

    public Animator animator { get; private set; }

    private UnityEvent eventAction = new UnityEvent();
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetParameterTrue(string name)
    {
        animator.SetBool(name, true);
    }

    public void SetParameterFalse(string name)
    {
        animator.SetBool(name, false);
    }

    public void SetEventAction(UnityAction action)
    {
        if(eventAction != null)
        {
            eventAction.RemoveAllListeners();
        }

        eventAction.AddListener(action);
    }

    public void InvokeEventAction()
    {
        eventAction.Invoke();
    }
}