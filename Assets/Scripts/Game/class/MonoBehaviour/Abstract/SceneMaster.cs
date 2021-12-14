
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

using UnityEngine.SceneManagement;

public abstract class SceneMaster : MonoBehaviour
{
    public static SceneMaster instance { get; private set; } = null;

    [SerializeField] protected Button _button_0 = null;

    [SerializeField] protected Button _button_1 = null;

    [SerializeField] protected Button _button_2 = null;

    public AudioSourceMaster backgroundMusic { get; protected set; } = null;

    protected void Awake()
    {
        instance = this;

        _button_0.onClick.AddListener(()=> { GameMaster.instance.NewLevelInfo(); LoadScene(SceneCode.Title); });

        _button_1.onClick.AddListener(() => { GameMaster.instance.NewLevelInfo(); LoadScene(SceneCode.City); });

        _button_2.onClick.AddListener(() => { GameMaster.instance.NewLevelInfo(); LoadScene(SceneCode.Desert); });
    }

    protected void Start()
    {
        StartCoroutine(_Opening_());
    }

    protected virtual IEnumerator _Opening_()
    {
        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(2f, 0f, 1f, 2f);

        yield return CoroutineWizard.WaitForSeconds(2f);
    }

    public void LoadScene(SceneCode sceneCode)
    {
        StartCoroutine(_LoadScene_(GameMaster.instance.gameData.sceneNames[sceneCode]));
    }

    protected IEnumerator _LoadScene_(string sceneName)
    {
        backgroundMusic.StartFadeVolume(0f, 2f);

        yield return ScreenEffecter.instance.primaryFadeScreen.Fade(null, 2, 0, 2f);

        SceneManager.LoadScene(sceneName);
    }
}