using System.Collections.Generic;

using UnityEngine;

public static class CoroutineManager
{
    private class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }

        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds waitforSeconds;

        if (!timeInterval.TryGetValue(seconds, out waitforSeconds))
        {
            timeInterval.Add(seconds, waitforSeconds = new WaitForSeconds(seconds));
        }

        return waitforSeconds;
    }
}