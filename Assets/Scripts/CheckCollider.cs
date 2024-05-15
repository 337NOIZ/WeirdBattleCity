using UnityEngine;

public sealed class CheckCollider : MonoBehaviour
{
    private Collider _collider;

    public new Collider collider { get => _collider; }

    public bool isEntered { get; private set; } = false;

    public GameObject target { get; set; } = null;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        isEntered = Check(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        isEntered = Check(collider);
    }

    private bool Check(Collider collider)
    {
        if (target != null)
        {
            if (collider.gameObject == target)
            {
                return true;
            }
        }

        return false;
    }
}