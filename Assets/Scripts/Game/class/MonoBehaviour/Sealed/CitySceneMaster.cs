
using System.Collections;

public sealed class CitySceneMaster : SceneMaster
{
    protected override IEnumerator _Opening_()
    {
        if (GameMaster.instance.gameInfo.levelInfo == null)
        {
            GameMaster.instance.NewLevelInfo();
        }

        Player.instance.Awaken();

        Player.instance.Launch();

        yield return base._Opening_();

        yield return StageMaster.instance.Stage(SceneCode.City);

        yield return CoroutineWizard.WaitForSeconds(5f);

        LoadScene(SceneCode.Desert);
    }
}