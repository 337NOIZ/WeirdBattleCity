
using UnityEngine;

public class DebugRay : MonoBehaviour
{
    [Space]

    [SerializeField] private bool draw = false;

    [Space]

    [SerializeField] private Transform target;

    [Space]

    [SerializeField] private float length = 1000f;

    private void Awake()
    {
        #if UNITY_EDITOR == false

            Destroy(this);

        #endif
    }

    private void Update()
    {
        if (draw == true)
        {
            Debug.DrawRay(target.position, target.forward * length, Color.red);
        }
    }
}