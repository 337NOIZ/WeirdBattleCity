
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public sealed class Stage : MonoBehaviour
{
    public static Stage instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator _Stage(SceneCode stageSceneCode)
    {
        var stageInfo = GameMaster.instance.gameInfo.levelInfo.stageInfos[stageSceneCode];

        var roundCount = stageInfo.roundInfos.Count;

        while (stageInfo.roundNumber < roundCount)
        {
            Debug.Log("Round " + (stageInfo.roundNumber));

            var roundInfo = stageInfo.roundInfos[stageInfo.roundNumber];

            var waveCount = roundInfo.waveInfos.Count;

            while (roundInfo.waveNumber < waveCount)
            {
                Debug.Log("Wave " + (roundInfo.waveNumber));

                var waveInfo = roundInfo.waveInfos[roundInfo.waveNumber];

                if(waveInfo.enemySpawnSpots != null)
                {
                    foreach(KeyValuePair<int, CharacterCode> enemySpawnSpots in waveInfo.enemySpawnSpots)
                    {
                        EnemySpawner.instance.Spawn(enemySpawnSpots.Value, enemySpawnSpots.Key, stageInfo.roundNumber + 1);
                    }
                }

                if (waveInfo.droppedItemSpawnSpots != null)
                {
                    foreach (KeyValuePair<int, ItemCode> enemySpawnSpots in waveInfo.droppedItemSpawnSpots)
                    {
                        DroppedItemSpawner.instance.Spawn(enemySpawnSpots.Value, enemySpawnSpots.Key, 1);
                    }
                }

                while(EnemySpawner.instance.spawnCount > 0 && waveInfo.waveTimer > 0f)
                {
                    yield return null;

                    waveInfo.waveTimer -= Time.deltaTime;
                }

                waveInfo.waveTimer = 0f;

                ++roundInfo.waveNumber;

                yield return null;
            }

            while (roundInfo.roundTimer > 0f)
            {
                yield return null;

                roundInfo.roundTimer -= Time.deltaTime;
            }

            roundInfo.roundTimer = 0f;

            ++stageInfo.roundNumber;

            yield return null;
        }
    }
}