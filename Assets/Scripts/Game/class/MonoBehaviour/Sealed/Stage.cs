
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

                if (waveInfo.enemySpawnSpots != null)
                {
                    foreach (KeyValuePair<int, CharacterCode> enemySpawnSpots in waveInfo.enemySpawnSpots)
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

                stageTimer.SetTimer(string.Format("WAVE {0:0}", roundInfo.waveNumber + 1), waveInfo.waveTimer);

                while (EnemySpawner.instance.spawnCount > 0 && waveInfo.waveTimer > 0f)
                {
                    waveInfo.waveTimer = stageTimer.timer;

                    yield return null;
                }

                ++roundInfo.waveNumber;

                yield return null;
            }

            turnRoundButton.gameObject.SetActive(true);

            stageTimer.SetTimer("NEXT ROUND", roundInfo.roundTimer);

            while (roundInfo.roundTimer > 0f)
            {
                roundInfo.roundTimer = stageTimer.timer;

                yield return null;
            }

            turnRoundButton.gameObject.SetActive(false);

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