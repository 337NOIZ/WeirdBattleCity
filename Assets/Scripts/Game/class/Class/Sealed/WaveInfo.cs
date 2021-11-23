
using System.Collections.Generic;

public sealed class WaveInfo
{
    public float waveTimer { get; set; }

    public Dictionary<int, CharacterCode> enemySpawnSpots { get; private set; } = null;

    public Dictionary<int, ItemCode> droppedItemSpawnSpots { get; private set; } = null;

    public WaveInfo(float waveTimer, Dictionary<int, CharacterCode> enemySpawnSpots, Dictionary<int, ItemCode> droppedItemSpawnSpots)
    {
        this.waveTimer = waveTimer;

        if (enemySpawnSpots != null)
        {
            this.enemySpawnSpots = new Dictionary<int, CharacterCode>(enemySpawnSpots);
        }

        if(droppedItemSpawnSpots != null)
        {
            this.droppedItemSpawnSpots = new Dictionary<int, ItemCode>(droppedItemSpawnSpots);
        }
    }

    public WaveInfo(WaveInfo waveInfo)
    {
        waveTimer = waveInfo.waveTimer;

        if (waveInfo.enemySpawnSpots != null)
        {
            enemySpawnSpots = new Dictionary<int, CharacterCode>(waveInfo.enemySpawnSpots);
        }

        if (waveInfo.droppedItemSpawnSpots != null)
        {
            droppedItemSpawnSpots = new Dictionary<int, ItemCode>(waveInfo.droppedItemSpawnSpots);
        }
    }
}