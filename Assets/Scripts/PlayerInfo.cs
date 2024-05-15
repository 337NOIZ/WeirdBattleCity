using UnityEngine;

public sealed class PlayerInfo
{
    public CharacterInfo characterInfo { get; private set; }

    public Vector3 animator_LocalEulerAngles { get; set; }

    public Vector3 cameraPivot_LocalEulerAngles { get; set; }

    public float cameraPivot_Sensitivity { get; set; }

    public PlayerInfo(CharacterData characterData)
    {
        characterInfo = new CharacterInfo(characterData);

        animator_LocalEulerAngles = Vector3.zero;

        cameraPivot_LocalEulerAngles = Vector3.zero;

        cameraPivot_Sensitivity = 1f;
    }

    public PlayerInfo(PlayerInfo playerInfo)
    {
        characterInfo = new CharacterInfo(playerInfo.characterInfo);

        animator_LocalEulerAngles = playerInfo.animator_LocalEulerAngles;

        cameraPivot_LocalEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;

        cameraPivot_Sensitivity = playerInfo.cameraPivot_Sensitivity;
    }
}