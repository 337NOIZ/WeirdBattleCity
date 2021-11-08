
using System.Collections;

public sealed class DesertSeceneMaster : SceneMaster
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

        Stage.instance._Stage(SceneCode.desert);

        GameMaster.instance.StopRecordPlayTime();
    }
}