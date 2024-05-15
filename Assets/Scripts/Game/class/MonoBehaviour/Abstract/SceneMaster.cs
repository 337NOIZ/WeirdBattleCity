
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public abstract class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance { get; private set; } = null;

    public AudioSourceMaster backgroundMusic { get; protected set; } = null;

    protected void Awake()
    {
        instance = this;
    }

    protected void Start()
    {
        StartCoroutine(_Opening());
    }

    protected virtual IEnumerator _Opening()
    {
        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(2f, 0f, 1f, 2f);

        yield return CoroutineManager.WaitForSeconds(2f);
    }

    public void LoadScene(SceneCode sceneCode)
    {
        StartCoroutine(_LoadScene(GameMaster.instance.gameData.sceneNames[sceneCode]));
    }

    protected IEnumerator _LoadScene(string sceneName)
    {
        backgroundMusic.StartFadeVolume(0f, 2f);

        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(null, 2, 0, 2f);

        SceneManager.LoadScene(sceneName);
    }
}