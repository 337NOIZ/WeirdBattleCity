
using System.Collections.Generic;

public sealed class StageInfo
{
    public List<RoundInfo> roundInfos { get; private set; }

    public int roundNumber { get; set; }

    public StageInfo(StageData stageData)
    {
        var roundDatas = stageData.roundDatas;

        roundInfos = new List<RoundInfo>();

        int count = roundDatas.Count;

        for(int index = 0; index < count; ++index)
        {
            roundInfos.Add(new RoundInfo(roundDatas[index]));
        }

        roundNumber = 0;
    }

    public StageInfo(StageInfo stageInfo)
    {
        roundInfos = new List<RoundInfo>(stageInfo.roundInfos);

        roundNumber = stageInfo.roundNumber;
    }
}