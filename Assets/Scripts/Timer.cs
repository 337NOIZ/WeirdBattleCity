
using System.Collections;

using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer { get; protected set; }

    public IEnumerator SetTimer(float time)
    {
        timer = time;

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.deltaTime;
        }

        timer = 0f;
    }
}