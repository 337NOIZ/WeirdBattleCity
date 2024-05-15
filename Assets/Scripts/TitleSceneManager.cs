using System.Collections;

public sealed class TitleSceneManager : SceneManager
{
    protected override IEnumerator _Opening()
    {
        backgroundMusic = AudioManager.instance.Pop(AudioClipCode.Title_0);

        backgroundMusic.gameObject.SetActive(true);

        backgroundMusic.Play(1.5f);

        yield return base._Opening();
    }

    public void Play()
    {
        GameManager.instance.NewLevelInfo();

        LoadScene(GameManager.instance.gameInfo.levelInfo.stageSceneCode);
    }

    public void Exit()
    {
        GameManager.instance.QuitGame();
    }
}