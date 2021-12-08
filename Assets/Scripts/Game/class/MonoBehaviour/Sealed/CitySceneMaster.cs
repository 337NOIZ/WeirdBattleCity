
using System.Collections;

public sealed class CitySceneMaster : SceneMaster
{
    protected override IEnumerator Opening()
    {
        if (GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        Player.instance.Awaken();

        Player.instance.Initialize();

        yield return base.Opening();

        yield return StageMaster.instance.Routine(SceneCode.city);
    }
}