
using UnityEngine;

public class TransformTools : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms = null;

    public Transform[] transforms { get; private set; }

    private void Awake()
    {
        transforms = _transforms;   
    }

    public void AlignTransforms(TransformTools transformTools)
    {
        AlignTransforms(transformTools.transforms);
    }

    public void AlignTransforms(Transform[] transforms)
    {
        int index_Max = _transforms.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _transforms[index].position = transforms[index].position;

            _transforms[index].rotation = transforms[index].rotation;
        }
    }
}