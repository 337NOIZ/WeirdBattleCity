
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Space]

    [SerializeField] private LayerMask _collisionable = default;

    [Space]

    [SerializeField] private Transform _cameraFollower_Right_Pivot = null;

    [SerializeField] private Transform _cameraFollower_Right = null;

    [SerializeField] private Transform _cameraFollower_Front_Pivot = null;

    [SerializeField] private Transform _cameraFollower_Front = null;

    [SerializeField] private Transform _cameraFollower = null;

    public Transform cameraFollower => _cameraFollower;

    private RaycastHit raycastHit;

    private Ray ray;

    private void Update()
    {
        Vector3 end = _cameraFollower_Right_Pivot.position + _cameraFollower_Right_Pivot.position - transform.position;

        if (Physics.Linecast(transform.position, end, out raycastHit, _collisionable) == true)
        {
            _cameraFollower_Right.localPosition = -(end - raycastHit.point);
        }

        else
        {
            _cameraFollower_Right.localPosition = Vector3.zero;
        }

        if (Physics.Linecast(_cameraFollower_Right.position, _cameraFollower_Front_Pivot.position, out raycastHit, _collisionable) == true)
        {
            _cameraFollower_Front.position = raycastHit.point;
        }

        else
        {
            _cameraFollower_Front.localPosition = Vector3.zero;
        }
    }

    public Vector3 GetAimPosition()
    {
        ray = new Ray(_cameraFollower.position, _cameraFollower.forward);

        if (Physics.Raycast(ray, out raycastHit, 1000f, _collisionable) == true)
        {
            return raycastHit.point;
        }
        
        return ray.origin + ray.direction * 1000f;
    }
}