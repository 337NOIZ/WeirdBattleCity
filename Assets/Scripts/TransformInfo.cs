
using UnityEngine;

public class TransformInfo
{
    public Vector3 position { get; set; } = Vector3.zero;

    public Vector3 eulerAngles { get; set; } = Vector3.zero;

    public Vector3 scale { get; set; } = Vector3.one;

    public TransformInfo()
    {

    }
    public TransformInfo(Vector3 position)
    {
        this.position = position;
    }
    public TransformInfo(Vector3 position, Vector3 eulerAngles)
    {
        this.position = position;

        this.eulerAngles = eulerAngles;
    }
    public TransformInfo(Vector3 position, Vector3 eulerAngles, Vector3 scale)
    {
        this.position = position;

        this.eulerAngles = eulerAngles;

        this.scale = scale;
    }
    public TransformInfo(TransformInfo transformInfo)
    {
        position = transformInfo.position;

        eulerAngles = transformInfo.eulerAngles;

        scale = transformInfo.scale;
    }
}