
using System.Collections;

using UnityEngine;

using UnityEngine.SceneManagement;

public abstract class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance { get; private set; }

    protected void Awake()
    {
        instance = this;
    }

    protected void Start()
    {
        StartCoroutine(_Opening_());
    }

    protected virtual IEnumerator _Opening_()
    {
        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(2f, 0f, 1f, 2f);

        yield return new WaitForSeconds(2f);

        AudioMaster.instance.PlayBackgroundMusic("sceneName", 1f);
    }

    public void LoadScene(SceneCode sceneCode)
    {
        StartCoroutine(_LoadScene_(GameMaster.instance.gameData.sceneNames[sceneCode]));
    }

    protected IEnumerator _LoadScene_(string sceneName)
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