
using System.Collections;

public sealed class CitySceneMaster : SceneMaster
{
    protected override IEnumerator StartRoutine()
    {
        if (GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        Player.instance.Initialize();

        yield return base.StartRoutine();

        yield return Stage.instance.Routine(SceneCode.city);
    }
}