
using UnityEngine;

public sealed class TransformInfo
{
    public Vector3 position { get; set; }

    public Vector3 eulerAngles { get; set; }

    public TransformInfo()
    {
        position = Vector3.zero;

        eulerAngles = Vector3.zero;
    }

    public TransformInfo(TransformInfo transformInfo)
    {
        position = transformInfo.position;

        eulerAngles = transformInfo.eulerAngles;
    }
}