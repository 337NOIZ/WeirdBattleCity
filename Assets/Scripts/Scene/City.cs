
using System.Collections;

using UnityEngine;

using FadeScreen;

public class City : MonoBehaviour
{
    private void Start()
    {
        Player.instance.Initialize(GameManager.instance.gameData.playerInfo);

        StartCoroutine(_Start());
    }

    private IEnumerator _Start()
    {
        yield return PrimaryFadeScreen.instance.fadeScreen.Fade(2f, 0f, 1f, 2f);
    }
}