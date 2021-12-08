
using UnityEngine;

public sealed class PhysicsWizard : MonoBehaviour
{
    public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit raycastHit, float maxDistance, int layerMask)
    {
        if (Physics.Raycast(start, (end - start).normalized, out raycastHit, maxDistance, layerMask) == true)
        {
            return true;
        }

        return false;
    }

    public static bool Linecast(Vector3 start, Vector3 end, float maxDistance, int layerMask, GameObject target)
    {
        RaycastHit raycastHit;

        if (Linecast(start, end, out raycastHit, maxDistance, layerMask) == true)
        {
            if(raycastHit.collider.gameObject == target)
            {
                return true;
            }
        }

        return false;
    }

    public static bool BoxCast(Vector3 Start, Vector3 halfExtents, Vector3 end, out RaycastHit raycastHit, Quaternion orientation, float maxDistance, int layerMask)
    {
        if (Physics.BoxCast(Start, halfExtents, (end - Start).normalized , out raycastHit, orientation, maxDistance, layerMask) == true)
        {
            return true;
        }

        return false;
    }
}