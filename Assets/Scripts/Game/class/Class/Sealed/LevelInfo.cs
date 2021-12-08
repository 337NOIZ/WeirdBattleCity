
using System.Collections.Generic;

public sealed class LevelInfo
{
    public PlayerInfo playerInfo { get; private set; }

    public Dictionary<SceneCode, StageInfo> stageInfos { get; private set; }

    public SceneCode stageSceneCode { get; set; }

    public float levelPlayTime { get; set; }

    public LevelInfo(LevelData levelData)
    {
        playerInfo = new PlayerInfo(levelData.characterDatas[CharacterCode.player]);

        stageInfos = new Dictionary<SceneCode, StageInfo>();

        foreach (KeyValuePair<SceneCode, StageData> stageDatas in levelData.stageDatas)
        {
            stageInfos.Add(stageDatas.Key, new StageInfo(stageDatas.Value));
        }

        stageSceneCode = SceneCode.city;

        levelPlayTime = 0f;
    }

    public LevelInfo(LevelInfo levelInfo)
    {
        playerInfo = new PlayerInfo(levelInfo.playerInfo);

        stageInfos = new Dictionary<SceneCode, StageInfo>(levelInfo.stageInfos);

        stageSceneCode = levelInfo.stageSceneCode;

        levelPlayTime = levelInfo.levelPlayTime;
    }
}