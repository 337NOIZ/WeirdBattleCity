
using UnityEngine;

public class DrawDebugLine : MonoBehaviour
{
    [Space]

    [SerializeField] private bool draw = false;

    [Space]

    [SerializeField] private Transform start = null;

    [SerializeField] private Transform end = null;

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
            Debug.DrawLine(start.position, end.position, color);
        }
    }
}
