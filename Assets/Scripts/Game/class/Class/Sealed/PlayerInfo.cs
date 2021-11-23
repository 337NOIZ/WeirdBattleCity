
using UnityEngine;

public sealed class PlayerInfo
{
    public Vector3 animator_LocalEulerAngles { get; set; }

    public Vector3 cameraPivot_LocalEulerAngles { get; set; }

    public float cameraPivot_Sensitivity { get; set; }

    public CharacterInfo characterInfo { get; private set; }

    public PlayerInventoryInfo playerInventoryInfo { get; private set; }

    public PlayerInfo(PlayerData playerData)
    {
        animator_LocalEulerAngles = Vector3.zero;

        cameraPivot_LocalEulerAngles = Vector3.zero;

        cameraPivot_Sensitivity = 1f;

        characterInfo = new CharacterInfo(playerData.characterData, new TransformInfo());

        playerInventoryInfo = new PlayerInventoryInfo(playerData.playerInventoryData);
    }

    public PlayerInfo(PlayerInfo playerInfo)
    {
        animator_LocalEulerAngles = playerInfo.animator_LocalEulerAngles;

        cameraPivot_LocalEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;

        cameraPivot_Sensitivity = playerInfo.cameraPivot_Sensitivity;

        characterInfo = new CharacterInfo(playerInfo.characterInfo);

        playerInventoryInfo = new PlayerInventoryInfo(playerInfo.playerInventoryInfo);
    }
}