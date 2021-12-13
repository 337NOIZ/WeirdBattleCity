
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public sealed class StageMaster : MonoBehaviour
{
    public static StageMaster instance { get; private set; }

    [SerializeField] private TimerDisplayer _timerDisplayer = null;

    [SerializeField] private Button _turnRoundButton = null;

    private RoundInfo _roundInfo;

    private void Awake()
    {
        instance = this;

        _turnRoundButton.onClick.AddListener(TurnRound);
    }

    public IEnumerator Stage(SceneCode stageSceneCode)
    {
        GameMaster.instance.StartRecordPlayTime();

        var stageInfo = GameMaster.instance.gameInfo.levelInfo.stageInfos[stageSceneCode];

        var roundCount = stageInfo.roundInfos.Count;

        while (stageInfo.roundNumber < roundCount)
        {
            _roundInfo = stageInfo.roundInfos[stageInfo.roundNumber];

            var waveCount = _roundInfo.waveInfos.Count;

            while (_roundInfo.waveNumber < waveCount)
            {
                var waveInfo = _roundInfo.waveInfos[_roundInfo.waveNumber];

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

                _timerDisplayer.SetTimer(string.Format("WAVE {0:0}", _roundInfo.waveNumber + 1), waveInfo.waveTimer);

                while (EnemySpawner.instance.spawnCount > 0 && waveInfo.waveTimer > 0f)
                {
                    waveInfo.waveTimer = _timerDisplayer.timer;

                    yield return null;
                }

                ++_roundInfo.waveNumber;

                yield return null;
            }

            if(stageInfo.roundNumber + 1 < roundCount)
            {
                _turnRoundButton.gameObject.SetActive(true);

                _timerDisplayer.SetTimer("NEXT ROUND", _roundInfo.roundTimer);

                while (_roundInfo.roundTimer > 0f)
                {
                    _roundInfo.roundTimer = _timerDisplayer.timer;

                    yield return null;
                }

                _turnRoundButton.gameObject.SetActive(false);
            }
            
            else
            {
                _timerDisplayer.gameObject.SetActive(false);
            }

            ++stageInfo.roundNumber;

            yield return null;
        }

        GameMaster.instance.StopRecordPlayTime();
    }

    private void TurnRound()
    {
        _roundInfo.roundTimer = 0f;
    }
}