
using UnityEngine;

public class DebugRay : MonoBehaviour
{
    [Space]

    [SerializeField] private bool draw = false;

    [Space]

    [SerializeField] private Transform target;

    [Space]

    [SerializeField] private Vector3 direction = Vector3.forward * 1000f;

    private void Update()
    {
        if (draw == true)
        {
            Debug.DrawRay(target.position, direction, Color.red);
        }
    }
}