
using UnityEngine;

public sealed class TransformInfo
{
    public Vector3 position { get; set; }

    public Vector3 eulerAngles { get; set; }

    public TransformInfo(Vector3 position, Vector3 eulerAngles)
    {
        this.position = position;

        this.eulerAngles = eulerAngles;
    }

    public TransformInfo(TransformInfo transformInfo)
    {
        position = transformInfo.position;

        eulerAngles = transformInfo.eulerAngles;
    }
}