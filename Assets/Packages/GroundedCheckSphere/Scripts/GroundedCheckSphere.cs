
using UnityEngine;

public class GroundedCheckSphere : MonoBehaviour
{
    [Space]

    [SerializeField] private Vector3 position = new Vector3(0f, 0f, 0f);

    [Space]
    
    [SerializeField] private float radius = 0.2f;

    [Space]

    [SerializeField] private Color defaultColor = new Color(1f, 0f, 0f, 0.5f);

    [SerializeField] private Color groundedColor = new Color(0f, 1f, 0f, 0.5f);

    [Space]

    [SerializeField] private string checklayerName = null;

    [Space]

    [SerializeField] private QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

    public int getMask { get; private set; }

    public int nameToLayer { get; private set; }

    public bool isGrounded { get; private set; }

    private void Awake()
    {
        getMask = LayerMask.GetMask(checklayerName);

        nameToLayer = LayerMask.NameToLayer(checklayerName);
    }

    private void OnDrawGizmosSelected()
    {
        if (Check() == false)
        {
            Gizmos.color = defaultColor;
        }

        else
        {
            Gizmos.color = groundedColor;
        }

        Gizmos.DrawSphere(transform.position + position, radius);
    }

    public bool Check()
    {
        return isGrounded = Physics.CheckSphere(transform.position + position, radius, getMask, queryTriggerInteraction);
    }
}