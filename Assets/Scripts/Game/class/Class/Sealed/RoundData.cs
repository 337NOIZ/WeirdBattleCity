
using System.Collections.Generic;

public sealed class RoundData
{
    public float roundTime { get; private set; }

    public List<WaveData> waveDatas { get; private set; }

    public RoundData(float roundTime, List<WaveData> waveDatas)
    {
        this.roundTime = roundTime;

        this.waveDatas = new List<WaveData>(waveDatas);
    }
}