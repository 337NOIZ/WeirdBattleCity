
using System.Collections;

public sealed class CitySceneMaster : SceneMaster
{
    protected override IEnumerator StartScene()
    {
        if (GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        Player.instance.Initialize();

        yield return base.StartScene();

        GameMaster.instance.StartRecordPlayTime();

        yield return Stage.instance._Stage(SceneCode.city);

        GameMaster.instance.StopRecordPlayTime();
    }
}