
using System.Collections;

using UnityEngine;

public class DesertSceneManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(_Start());
    }

    private IEnumerator _Start()
    {
        yield return GameManager.instance.fadeScreen.Fade(2f, 0f, 1f, 2f);

        //SoundManager.instance.PlayBackgroundMusic("Desert", 1f);
    }
}
