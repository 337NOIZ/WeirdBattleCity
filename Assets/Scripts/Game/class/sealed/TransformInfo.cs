
using UnityEngine;

public sealed class TransformInfo
{
    public Vector3 position { get; set; } = Vector3.zero;

    public Vector3 eulerAngles { get; set; } = Vector3.zero;

    public float scale_Multiple { get; set; } = 1f;

    public TransformInfo() { }

    public TransformInfo(TransformInfo transformInfo)
    {
        position = transformInfo.position;

        eulerAngles = transformInfo.eulerAngles;

        scale_Multiple = transformInfo.scale_Multiple;
    }

    public void LevelUp(TransformInfo_LevelUpData transformInfo_LevelUpData)
    {
        scale_Multiple += transformInfo_LevelUpData.scale_Multiple;
    }
}