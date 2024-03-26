
using System.Collections;

public sealed class DesertSceneMaster : SceneMaster
{
    protected override IEnumerator _Opening()
    {
        if (GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        Player.instance.Awaken();

        Player.instance.Launch();

        backgroundMusic = AudioMaster.instance.Pop(AudioClipCode.Desert_0);

        backgroundMusic.Play(1.5f);

        yield return base._Opening();

        yield return StageMaster.instance.Stage(SceneCode.Desert);

        yield return CoroutineWizard.WaitForSeconds(5f);

        LoadScene(SceneCode.Title);
    }
}