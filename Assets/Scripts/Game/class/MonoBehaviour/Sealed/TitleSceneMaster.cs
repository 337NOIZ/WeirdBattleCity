
using System.Collections;

public sealed class TitleSceneMaster : SceneMaster
{
    protected override IEnumerator _Opening()
    {
        backgroundMusic = AudioMaster.instance.Pop(AudioClipCode.Title_0);

        backgroundMusic.gameObject.SetActive(true);

        backgroundMusic.Play(1.5f);

        yield return base._Opening();
    }

    public void Play()
    {
        GameMaster.instance.NewLevelInfo();

        LoadScene(GameMaster.instance.gameInfo.levelInfo.stageSceneCode);
    }

    public void Exit()
    {
        GameMaster.instance.QuitGame();
    }
}