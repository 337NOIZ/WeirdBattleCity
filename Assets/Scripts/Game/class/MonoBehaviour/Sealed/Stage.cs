
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public sealed class Stage : MonoBehaviour
{
    public static Stage instance { get; private set; }

    [Space]

    [SerializeField] private TimerDisplayer stageTimer = null;

    [Space]

    [SerializeField] private Button turnRoundButton = null;

    private RoundInfo roundInfo;

    private void Awake()
    {
        instance = this;

        turnRoundButton.onClick.AddListener(TurnRound);
    }

    public IEnumerator Routine(SceneCode stageSceneCode)
    {
        GameMaster.instance.StartRecordPlayTime();

        var stageInfo = GameMaster.instance.gameInfo.levelInfo.stageInfos[stageSceneCode];

        var roundCount = stageInfo.roundInfos.Count;

        while (stageInfo.roundNumber < roundCount)
        {
            roundInfo = stageInfo.roundInfos[stageInfo.roundNumber];

            var waveCount = roundInfo.waveInfos.Count;

            while (roundInfo.waveNumber < waveCount)
            {
                var waveInfo = roundInfo.waveInfos[roundInfo.waveNumber];


                if (waveInfo.enemySpawnData != null)
                {
                    var spawnEnemyInfos = waveInfo.enemySpawnData.spawnEnemyInfos;

                    int count_0 = spawnEnemyInfos.Count;

                    for(int index_0 = 0; index_0 < count_0; ++index_0)
                    {
                        var spotNumbers = spawnEnemyInfos[index_0].spotNumbers;

                        int count_1 = spotNumbers.Count;

                        for (int index_1 = 0; index_1 < count_1; ++index_1)
                        {
                            EnemySpawner.instance.Spawn(spawnEnemyInfos[index_0].characterCode, spawnEnemyInfos[index_0].characterLevel, spotNumbers[index_1]);
                        }
                    }
                }

                if (waveInfo.droppedItemSpawnData != null)
                {
                    var spawnDroppedItemInfos = waveInfo.droppedItemSpawnData.spawnDroppedItemInfos;

                    int count_0 = spawnDroppedItemInfos.Count;

                    for (int index_0 = 0; index_0 < count_0; ++index_0)
                    {
                        var spotNumbers = spawnDroppedItemInfos[index_0].spotNumbers;

                        int count_1 = spotNumbers.Count;

                        for (int index_1 = 0; index_1 < count_1; ++index_1)
                        {
                            DroppedItemSpawner.instance.Spawn(spawnDroppedItemInfos[index_0].itemCode, spawnDroppedItemInfos[index_0].itemLevel, spotNumbers[index_1]);
                        }
                    }
                }

                stageTimer.SetTimer(string.Format("WAVE {0:0}", roundInfo.waveNumber + 1), waveInfo.waveTimer);

                while (EnemySpawner.instance.spawnCount > 0 && waveInfo.waveTimer > 0f)
                {
                    waveInfo.waveTimer = stageTimer.timer;

                    yield return null;
                }

                ++roundInfo.waveNumber;

                yield return null;
            }

            if(stageInfo.roundNumber + 1 < roundCount)
            {
                turnRoundButton.gameObject.SetActive(true);

                stageTimer.SetTimer("NEXT ROUND", roundInfo.roundTimer);

                while (roundInfo.roundTimer > 0f)
                {
                    roundInfo.roundTimer = stageTimer.timer;

                    yield return null;
                }

                turnRoundButton.gameObject.SetActive(false);
            }
            
            else
            {
                stageTimer.gameObject.SetActive(false);
            }

            ++stageInfo.roundNumber;

            yield return null;
        }

        GameMaster.instance.StopRecordPlayTime();
    }

    private void TurnRound()
    {
        roundInfo.roundTimer = 0f;
    }
}