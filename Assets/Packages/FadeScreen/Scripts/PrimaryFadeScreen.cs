
using UnityEngine;

public class PrimaryFadeScreen : MonoBehaviour
{
    public static PrimaryFadeScreen instance { get; private set; }

    public FadeScreen fadeScreen { get; private set; }

    private void Awake()
    {
        instance = this;

        fadeScreen = GetComponentInChildren<FadeScreen>();
    }
}