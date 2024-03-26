
using System.Collections;

using UnityEngine;

public sealed class GroundedCheckSphere : MonoBehaviour
{
    [SerializeField] private Vector3 _checkSpherePosition = new Vector3(0f, 0f, 0f);

    [SerializeField] private float _checkSphereRadius = 0.2f;

    [SerializeField] private string _stepableLayerName = "Stepable";

    [SerializeField] private QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

    [SerializeField] private bool _drawGizmo = false;

    [SerializeField] private Color _gizmoColor_Default = new Color(1f, 0f, 0f, 0.5f);

    [SerializeField] private Color _gizmoColor_Grounded = new Color(0f, 1f, 0f, 0.5f);

    public int _stepableLayerMask { get; private set; }

    public int _stepableLayerNumber { get; private set; }

    private bool isCollision = false;

    public bool isGrounded { get; private set; } = false;

    private void OnDrawGizmosSelected()
    {
        if (_drawGizmo == true)
        {
            if (CheckSphere() == false)
            {
                Gizmos.color = _gizmoColor_Default;
            }

            else
            {
                Gizmos.color = _gizmoColor_Grounded;
            }

            Gizmos.DrawSphere(transform.position + _checkSpherePosition, _checkSphereRadius);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _stepableLayerNumber)
        {
            isCollision = true;

            if (_check != null)
            {
                StopCoroutine(_check);
            }

            _check = _Check();

            StartCoroutine(_check);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == _stepableLayerNumber)
        {
            isCollision = false;
        }
    }

    public void Awaken()
    {
        _stepableLayerMask = LayerMask.GetMask(_stepableLayerName);

        _stepableLayerNumber = LayerMask.NameToLayer(_stepableLayerName);
    }

    private IEnumerator _check = null;
    private IEnumerator _Check()
    {
        while (isCollision == true)
        {
            isGrounded = CheckSphere();

            yield return null;
        }

        while (CheckSphere() == true) yield return null;

        isGrounded = false;
    }

    private bool CheckSphere()
    {
        return Physics.CheckSphere(transform.position + _checkSpherePosition, _checkSphereRadius, _stepableLayerMask, queryTriggerInteraction);
    }
}