
using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Space]

    public FadeScreen fadeScreen = null;

    private void Awake()
    {
        instance = this;
    }

    public void LoadScene(string sceneName, float routineTime)
    {
        StartCoroutine(_LoadScene(sceneName, routineTime));
    }

    private IEnumerator _LoadScene(string sceneName, float routineTime)
    {
        AudioManager.instance.FadeAudioListenerVolume(0f, routineTime);

        yield return fadeScreen.Fade(null, 2, 0, routineTime);

        AudioManager.instance.backgroundMusicAudioSource.Stop();

        AudioManager.instance.StopSoundEffectAll();

        AudioManager.instance.StopFadeAudioListenerVolume();

        AudioListener.volume = 1f;

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(float routineTime)
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else

Application.Quit();

#endif
    }
}