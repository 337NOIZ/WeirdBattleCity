
using System.Collections;

using UnityEngine;

using FadeScreen;

public class Title : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(_Start());
    }

    private IEnumerator _Start()
    {
        yield return PrimaryFadeScreen.instance.fadeScreen.Fade(2f, 0f, 1f, 2f);
    }
}