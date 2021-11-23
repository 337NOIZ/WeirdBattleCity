
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public sealed class TitleSceneMaster : SceneMaster
{
    [Space]

    [SerializeField] private Button continueGameButton = null;

    protected override IEnumerator StartRoutine()
    {
        if(GameMaster.instance.gameInfo.levelInfo == null)
        {
            continueGameButton.interactable = false;
        }

        yield return base.StartRoutine();
    }

    public void NewGame()
    {
        GameMaster.instance.NewLevelInfo();

        ContinueGame();
    }

    public void ContinueGame()
    {
        LoadScene(GameMaster.instance.gameData.sceneNames[GameMaster.instance.gameInfo.levelInfo.stageSceneCode]);
    }

    public void Options()
    {

    }

    public void Quit()
    {
        GameMaster.instance.Quit();
    }
}