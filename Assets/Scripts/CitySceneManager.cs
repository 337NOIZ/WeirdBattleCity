using System.Collections;

public sealed class CitySceneManager : SceneManager
{
    protected override IEnumerator _Opening()
    {
        if (GameManager.instance.gameInfo.levelInfo == null)
        {
            GameManager.instance.NewLevelInfo();
        }

        Player.instance.Awaken();

        Player.instance.Launch();

        backgroundMusic = AudioManager.instance.Pop(AudioClipCode.City_0);

        backgroundMusic.Play(1.5f);

        yield return base._Opening();

        yield return StageManager.instance.Stage(SceneCode.City);

        yield return CoroutineManager.WaitForSeconds(5f);

        LoadScene(SceneCode.Desert);
    }
}