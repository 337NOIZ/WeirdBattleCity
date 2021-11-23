
public sealed class GameInfo
{
    public float gamePlayTime { get; set; }

    public LevelInfo levelInfo { get; private set; }

    public GameInfo()
    {
        gamePlayTime = 0f;
    }

    public GameInfo(GameInfo gameInfo)
    {
        gamePlayTime = gameInfo.gamePlayTime;
        
        levelInfo = new LevelInfo(gameInfo.levelInfo);
    }

    public void NewLevelInfo(GameData gameData)
    {
        levelInfo = new LevelInfo(gameData.levelData);
    }
}