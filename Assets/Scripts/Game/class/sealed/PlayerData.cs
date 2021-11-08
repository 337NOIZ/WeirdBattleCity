
using UnityEngine;

public sealed class PlayerData
{
    public Vector2 cameraPivot_LocalEulerAngles_Min { get; private set; }

    public Vector2 cameraPivot_LocalEulerAngles_Max { get; private set; }

    public PlayerData(Vector2 cameraPivot_LocalEulerAngles_Min, Vector2 cameraPivot_LocalEulerAngles_Max)
    {
        this.cameraPivot_LocalEulerAngles_Min = cameraPivot_LocalEulerAngles_Min;

        this.cameraPivot_LocalEulerAngles_Max = cameraPivot_LocalEulerAngles_Max;
    }

    public PlayerData(PlayerData playerData)
    {
        cameraPivot_LocalEulerAngles_Min = playerData.cameraPivot_LocalEulerAngles_Min;

        cameraPivot_LocalEulerAngles_Max = playerData.cameraPivot_LocalEulerAngles_Max;
    }
}