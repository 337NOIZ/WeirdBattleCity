
using UnityEngine;

public class DebugRay : MonoBehaviour
{
    [Space]

    [SerializeField] private bool draw = false;

    [Space]

    [SerializeField] private Transform start = null;

    [SerializeField] private Transform direction = null;

    [SerializeField] private float length = 1000f;

    [SerializeField] private Color color = Color.red; 

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
            Debug.DrawRay(start.position, direction.position.normalized * length, color);
        }
    }
}