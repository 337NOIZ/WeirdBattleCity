
using System.Collections;

public sealed class TitleSceneMaster : SceneMaster
{
    protected override IEnumerator _Opening_()
    {
        yield return base._Opening_();
    }

    public void Play()
    {
        if(GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        LoadScene(GameMaster.instance.gameInfo.levelInfo.stageSceneCode);
    }

    public void Exit()
    {
        GameMaster.instance.QuitGame();
    }
}