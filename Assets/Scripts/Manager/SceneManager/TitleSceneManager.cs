
using System.Collections;

using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(_Start());
    }

    private IEnumerator _Start()
    {
        yield return GameManager.instance.fadeScreen.Fade(2f, 0f, 1f, 2f);

        //SoundManager.instance.PlayBackgroundMusic("Title", 1f);
    }
}