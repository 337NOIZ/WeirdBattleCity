
using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;

using FadeScreen;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    public void LoadScene(string sceneName, float routineTime)
    {
        StartCoroutine(_LoadScene(sceneName, routineTime));
    }

    private IEnumerator _LoadScene(string sceneName, float routineTime)
    {
        AudioManager.instance.FadeAudioListenerVolume(0f, routineTime);

        yield return PrimaryFadeScreen.instance.fadeScreen.Fade(null, 2, 0, routineTime);

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