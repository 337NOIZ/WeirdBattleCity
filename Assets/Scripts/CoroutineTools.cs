
using System.Collections;

using UnityEngine;

public sealed class CoroutineTools : MonoBehaviour
{
    public static IEnumerator WaitOrNot(float waitTime)
    {
        while (true)
        {
            if (waitTime <= 0f || Input.anyKeyDown == true)
            {
                break;
            }

            waitTime -= Time.deltaTime;

            yield return null;
        }
    }

    public static IEnumerator WaitOrNot()
    {
        while (true)
        {
            if (Input.anyKeyDown == true)
            {
                break;
            }

            yield return null;
        }
    }
}