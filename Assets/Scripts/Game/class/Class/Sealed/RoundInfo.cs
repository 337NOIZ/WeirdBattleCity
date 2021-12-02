
using System.Collections.Generic;

public sealed class RoundInfo
{
    public List<WaveInfo> waveInfos { get; private set; }

    public int waveNumber { get; set; }

    public float roundTimer { get; set; }

    public RoundInfo(RoundData roundData)
    {
        var waveDatas = roundData.waveDatas;

        waveInfos = new List<WaveInfo>();

        int index_Max = waveDatas.Count;

        for (int index = 0; index < index_Max; ++index)
        {
            waveInfos.Add(new WaveInfo(waveDatas[index]));
        }

        waveNumber = 0;

        roundTimer = roundData.roundTime;
    }

    public RoundInfo(RoundInfo roundInfo)
    {
        waveInfos = new List<WaveInfo>(roundInfo.waveInfos);

        waveNumber = roundInfo.waveNumber;

        roundTimer = roundInfo.roundTimer;
    }
}