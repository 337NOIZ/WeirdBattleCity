
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;

    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void FixedUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.up);
    }
}