
using System.Collections;

public sealed class DesertSeceneMaster : SceneMaster
{
    protected override IEnumerator Opening()
    {
        if (GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        Player.instance.Initialize();

        yield return base.Opening();

        yield return Stage.instance.Routine(SceneCode.desert);
    }
}