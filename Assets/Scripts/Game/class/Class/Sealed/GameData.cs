
using System.Collections.Generic;

public sealed class GameData
{
    public Dictionary<SceneCode, string> sceneNames { get; private set; } = new Dictionary<SceneCode, string>();

    public LevelData levelData { get; private set; } = new LevelData();

    public GameData()
    {
        sceneNames.Add(SceneCode.city, "City");

        sceneNames.Add(SceneCode.desert, "Desert");

        sceneNames.Add(SceneCode.title, "Title");
    }
}