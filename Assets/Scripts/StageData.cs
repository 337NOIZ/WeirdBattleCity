using System.Collections.Generic;

public sealed class StageData
{
    public List<RoundData> roundDatas { get; private set; }

    public StageData(List<RoundData> roundDatas)
    {
        this.roundDatas = new List<RoundData>(roundDatas);
    }
}