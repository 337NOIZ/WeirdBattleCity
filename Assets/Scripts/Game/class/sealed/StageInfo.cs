
using System.Collections.Generic;

public sealed class StageInfo
{
    public List<RoundInfo> roundInfos { get; private set; }

    public int roundNumber { get; set; } = 0;

    public StageInfo(List<RoundInfo> roundInfos)
    {
        this.roundInfos = new List<RoundInfo>(roundInfos);
    }

    public StageInfo(StageInfo stageInfo)
    {
        roundInfos = new List<RoundInfo>(stageInfo.roundInfos);
    }
}