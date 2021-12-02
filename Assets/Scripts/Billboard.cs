
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private bool _positionInFrontOfCamera;

    [SerializeField] private float _offset = 0.001f;

    void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    void Update()
    {
        var forwardVec = _camera.transform.forward.normalized;

        if (_positionInFrontOfCamera)
        {
            transform.position = _camera.transform.position + (forwardVec * (_camera.nearClipPlane + _offset));
        }

     // transform.LookAt(transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.up);

        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}