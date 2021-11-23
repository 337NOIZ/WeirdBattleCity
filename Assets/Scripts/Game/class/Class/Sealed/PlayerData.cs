
using UnityEngine;

public sealed class PlayerData
{
    public Vector2 cameraPivot_LocalEulerAngles_Min { get; private set; }

    public Vector2 cameraPivot_LocalEulerAngles_Max { get; private set; }

    public CharacterData characterData { get; private set; }

    public PlayerInventoryData playerInventoryData { get; private set; }

    public PlayerData(Vector2 cameraPivot_LocalEulerAngles_Min, Vector2 cameraPivot_LocalEulerAngles_Max, CharacterData characterData, PlayerInventoryData playerInventoryData)
    {
        this.cameraPivot_LocalEulerAngles_Min = cameraPivot_LocalEulerAngles_Min;

        this.cameraPivot_LocalEulerAngles_Max = cameraPivot_LocalEulerAngles_Max;

        this.characterData = characterData;

        this.playerInventoryData = new PlayerInventoryData(playerInventoryData);
    }

    public PlayerData(PlayerData playerData)
    {
        cameraPivot_LocalEulerAngles_Min = playerData.cameraPivot_LocalEulerAngles_Min;

        cameraPivot_LocalEulerAngles_Max = playerData.cameraPivot_LocalEulerAngles_Max;

        playerInventoryData = new PlayerInventoryData(playerData.playerInventoryData);
    }
}