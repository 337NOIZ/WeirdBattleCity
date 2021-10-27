
using UnityEngine;

public class PlayerInfo
{
    public Vector3 animator_LocalEulerAngles { get; set; } = Vector3.zero;

    public Vector3 cameraPivot_LocalEulerAngles { get; set; } = Vector3.zero;

    public float cameraPivot_Sensitivity { get; set; } = 1f;

    public CharacterInfo characterInfo { get; private set; }

    public InventoryInfo inventoryInfo { get; private set; }

    public PlayerInfo(CharacterInfo characterInfo, InventoryInfo inventoryInfo)
    {
        this.characterInfo = new CharacterInfo(characterInfo);

        this.inventoryInfo = new InventoryInfo(inventoryInfo);
    }
    public PlayerInfo(PlayerInfo playerInfo)
    {
        animator_LocalEulerAngles = playerInfo.animator_LocalEulerAngles;

        cameraPivot_LocalEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;

        cameraPivot_Sensitivity = playerInfo.cameraPivot_Sensitivity;

        characterInfo = new CharacterInfo(playerInfo.characterInfo);

        inventoryInfo = new InventoryInfo(playerInfo.inventoryInfo);
    }
}