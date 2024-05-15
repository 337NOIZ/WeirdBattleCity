using UnityEngine;

public static class PhysicsManager
{
    private static RaycastHit raycastHit;

    public static bool BoxCast(Vector3 start, Quaternion rotation, Vector3 scale, LayerMask layerMask, out RaycastHit raycastHit)
    {
        return Physics.BoxCast(start, scale * 0.5f, Vector3.zero, out raycastHit, rotation, 0f, layerMask);
    }

    public static bool BoxCast(Vector3 start, Vector3 end, Quaternion rotation, Vector3 scale, LayerMask layerMask, out RaycastHit raycastHit)
    {
        return Physics.BoxCast(start, scale * 0.5f, (end - start).normalized, out raycastHit, rotation, Vector3.Distance(start, end), layerMask);
    }

    public static bool BoxCastAll(Vector3 start, Quaternion rotation, Vector3 scale, LayerMask layerMask, out RaycastHit[] raycastHits)
    {
        raycastHits = Physics.BoxCastAll(start, scale * 0.5f, Vector3.up, rotation, 0f, layerMask);

        return raycastHits.Length > 0;
    }

    public static bool BoxCastAll(Vector3 start, Vector3 end, Quaternion rotation, Vector3 scale, LayerMask layerMask, out RaycastHit[] raycastHits)
    {
        raycastHits = Physics.BoxCastAll(start, scale * 0.5f, (end - start).normalized, rotation, Vector3.Distance(start, end), layerMask);

        return raycastHits.Length > 0;
    }

    public static bool SphereCast(Vector3 start, float radius, LayerMask layerMask, out RaycastHit raycastHit)
    {
        return Physics.SphereCast(start, radius, Vector3.zero, out raycastHit, 0f, layerMask);
    }

    public static bool SphereCast(Vector3 start, Vector3 end, float radius, LayerMask layerMask, out RaycastHit raycastHit)
    {
        return Physics.SphereCast(start, radius, (end - start).normalized, out raycastHit, Vector3.Distance(start, end), layerMask);
    }

    public static bool SphereCastAll(Vector3 start, float radius, LayerMask layerMask, out RaycastHit[] raycastHits)
    {
        raycastHits = Physics.SphereCastAll(start, radius, Vector3.up, 0f, layerMask);

        return raycastHits.Length > 0;
    }

    public static bool SphereCastAll(Vector3 start, Vector3 end, float radius, LayerMask layerMask, out RaycastHit[] raycastHits)
    {
        raycastHits = Physics.SphereCastAll(start, radius, (end - start).normalized, Vector3.Distance(start, end), layerMask);

        return raycastHits.Length > 0;
    }

    public static bool LineCast(Vector3 start, Vector3 end, int layerMask, out RaycastHit raycastHit)
    {
        return Physics.Linecast(start, end, out raycastHit, layerMask);
    }

    public static bool LineCast(Vector3 start, Vector3 end, float length, int layerMask, out RaycastHit raycastHit)
    {
        return Physics.Raycast(start, (end - start).normalized, out raycastHit, length, layerMask);
    }

    public static bool LineCast(Vector3 start, Vector3 end, float length, int layerMask, Character target)
    {
        if(Physics.Raycast(start, (end - start).normalized, out raycastHit, length, layerMask) == true)
        {
            var hitBox = raycastHit.collider.GetComponent<HitBox>();

            if (hitBox != null)
            {
                return hitBox.character == target;
            }
        }

        return false;
    }

    public static bool LineCastAll(Vector3 start, Vector3 end, int layerMask, out RaycastHit[] raycastHits)
    {
        raycastHits = Physics.RaycastAll(start, end, Vector3.Distance(start, end), layerMask);

        return raycastHits.Length > 0;
    }

    public static bool LineCastAll(Vector3 start, Vector3 end, float length, int layerMask, out RaycastHit[] raycastHits)
    {
        raycastHits = Physics.RaycastAll(start, (end - start).normalized, length, layerMask);

        return raycastHits.Length > 0;
    }
}