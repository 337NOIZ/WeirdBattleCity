
using UnityEngine;

public sealed class TransformInfo
{
    public Vector3 position { get; set; } = Vector3.zero;

    public Vector3 eulerAngles { get; set; } = Vector3.zero;

    public TransformInfo() { }

    public TransformInfo(TransformInfo transformInfo)
    {
        position = transformInfo.position;

        eulerAngles = transformInfo.eulerAngles;
    }
}