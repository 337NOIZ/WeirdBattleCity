
using UnityEngine;

public sealed class ScreenEffecter : MonoBehaviour
{
    public static ScreenEffecter instance { get; private set; }

    [SerializeField] private FadeScreen _primaryFadeScreen = null;

    [SerializeField] private FadeScreen _secondaryFadeScreen = null;

    public FadeScreen primaryFadeScreen { get; private set; }

    public FadeScreen secondaryFadeScreen { get; private set; }

    private void Awake()
    {
        instance = this;

        primaryFadeScreen = _primaryFadeScreen;

        secondaryFadeScreen = _secondaryFadeScreen;
    }
}