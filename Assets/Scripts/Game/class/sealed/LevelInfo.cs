
using System.Collections.Generic;

public sealed class LevelInfo
{
    public float levelPlayTime { get; set; } = 0f;

    public PlayerInfo playerInfo { get; private set; }

    public Dictionary<SceneCode, StageInfo> stageInfos { get; private set; }

    public SceneCode stageSceneCode { get; set; } = SceneCode.city;

    public LevelInfo(Dictionary<SceneCode, StageInfo> stageInfos)
    {
        playerInfo = new PlayerInfo();

        this.stageInfos = new Dictionary<SceneCode, StageInfo>(stageInfos);
    }

    public LevelInfo(LevelInfo levelInfo)
    {
        levelPlayTime = levelInfo.levelPlayTime;

        playerInfo = new PlayerInfo(levelInfo.playerInfo);

        stageInfos = new Dictionary<SceneCode, StageInfo>(levelInfo.stageInfos);

        stageSceneCode = levelInfo.stageSceneCode;
    }
}