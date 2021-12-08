
using System.Collections;

using UnityEngine;

public class GroundedCheckSphere : MonoBehaviour
{
    [SerializeField] private Vector3 position = new Vector3(0f, 0f, 0f);

    [SerializeField] private float radius = 0.2f;

    [SerializeField] private Color defaultColor = new Color(1f, 0f, 0f, 0.5f);

    [SerializeField] private Color groundedColor = new Color(0f, 1f, 0f, 0.5f);

    [SerializeField] private string stepable = null;

    [SerializeField] private QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

    public int getMask { get; private set; }

    public int nameToLayer { get; private set; }

    private bool onCollision = false;

    public bool isGrounded { get; private set; } = false;

    private void Awake()
    {
        getMask = LayerMask.GetMask(stepable);

        nameToLayer = LayerMask.NameToLayer(stepable);
    }

    private void OnDrawGizmosSelected()
    {
        if (CheckSphere() == false)
        {
            Gizmos.color = defaultColor;
        }

        else
        {
            Gizmos.color = groundedColor;
        }

        Gizmos.DrawSphere(transform.position + position, radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == nameToLayer)
        {
            onCollision = true;

            GroundedCheck();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == nameToLayer)
        {
            onCollision = false;
        }
    }

    private void GroundedCheck()
    {
        if (groundedCheckRoutine != null)
        {
            StopCoroutine(groundedCheckRoutine);
        }

        groundedCheckRoutine = GroundedCheckRoutine();

        StartCoroutine(groundedCheckRoutine);
    }

    private IEnumerator groundedCheckRoutine = null;
    private IEnumerator GroundedCheckRoutine()
    {
        while (onCollision == true)
        {
            isGrounded = CheckSphere();

            yield return null;
        }

        while (CheckSphere() == true) yield return null;

        isGrounded = false;
    }

    private bool CheckSphere()
    {
        return Physics.CheckSphere(transform.position + position, radius, getMask, queryTriggerInteraction);
    }
}