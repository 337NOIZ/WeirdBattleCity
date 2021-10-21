
using UnityEngine;

public class AnimationTools : MonoBehaviour
{
    public static float FrameCountToSeconds(int frameCount) { return 1f / 60 * frameCount; }

    private Animator animator;

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
}