
using System.Collections.Generic;

public sealed class RoundInfo
{
    public float roundTimer { get; set; }

    public List<WaveInfo> waveInfos { get; private set; }

    public int waveNumber { get; set; } = 0;

    public RoundInfo(float roundTimer, List<WaveInfo> waveInfos)
    {
        this.roundTimer = roundTimer;

        this.waveInfos = new List<WaveInfo>(waveInfos);
    }

    public RoundInfo(RoundInfo roundInfo)
    {
        roundTimer = roundInfo.roundTimer;

        waveInfos = new List<WaveInfo>(roundInfo.waveInfos);

        waveNumber = roundInfo.waveNumber;
    }
}