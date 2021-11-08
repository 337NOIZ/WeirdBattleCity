
using System.Collections.Generic;

public sealed class GameInfo
{
    public float gamePlayTime { get; set; }

    public LevelInfo levelInfo { get; private set; } = null;

    public GameInfo()
    {
        gamePlayTime = 0f;
    }

    public GameInfo(GameInfo gameInfo)
    {
        gamePlayTime = gameInfo.gamePlayTime;

        if (gameInfo.levelInfo != null)
        {
            levelInfo = new LevelInfo(gameInfo.levelInfo);
        }
    }

    public void NewLevelInfo(Dictionary<SceneCode, StageInfo> stageInfos)
    {
        levelInfo = new LevelInfo(stageInfos);
    }
}