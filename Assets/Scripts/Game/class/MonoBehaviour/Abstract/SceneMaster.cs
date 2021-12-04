
using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;

public abstract class SceneMaster : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Opening());
    }

    protected virtual IEnumerator Opening()
    {
        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(2f, 0f, 1f, 2f);

        yield return new WaitForSeconds(2f);

        AudioMaster.instance.PlayBackgroundMusic("sceneName", 1f);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(_LoadScene_(sceneName));
    }

    private IEnumerator _LoadScene_(string sceneName)
    {
        AudioMaster.instance.FadeAudioListenerVolume(0f, 2f);

        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(null, 2, 0, 2f);

        AudioMaster.instance.backgroundMusicAudioSource.Stop();

        AudioMaster.instance.StopSoundEffectAll();

        AudioMaster.instance.StopFadeAudioListenerVolume();

        AudioListener.volume = 1f;

        SceneManager.LoadScene(sceneName);
    }
}