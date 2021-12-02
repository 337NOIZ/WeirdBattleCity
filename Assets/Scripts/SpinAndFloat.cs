
using System.Collections;

using UnityEngine;

public sealed class SpinAndFloat : MonoBehaviour
{
    private Vector3 originalLocalPosition;

    private Vector3 originalLocalEulerAngles;

    public void Floating(Vector3 delta, float speed)
    {
        StopFloating();

        originalLocalPosition = transform.localPosition;

        floatingRoutine = FloatingRoutine(delta, speed);

        StartCoroutine(floatingRoutine);
    }

    private IEnumerator floatingRoutine = null;

    private IEnumerator FloatingRoutine(Vector3 delta, float speed)
    {
        while (true)
        {
            transform.localPosition = originalLocalPosition + delta * Mathf.Sin(speed * Time.time);

            yield return null;
        }
    }

    public void StopFloating()
    {
        if (floatingRoutine != null)
        {
            StopCoroutine(floatingRoutine);

            floatingRoutine = null;

            transform.localPosition = originalLocalPosition;
        }
    }

    public void Spining(Vector3 direction)
    {
        StopSpining();

        originalLocalEulerAngles = transform.localEulerAngles;

        spiningRoutine = SpiningRoutine(direction);

        StartCoroutine(spiningRoutine);
    }

    private IEnumerator spiningRoutine = null;

    private IEnumerator SpiningRoutine(Vector3 direction)
    {
        while (true)
        {
            transform.localEulerAngles += direction * Time.deltaTime;

            yield return null;
        }
    }

    public void StopSpining()
    {
        if (spiningRoutine != null)
        {
            StopCoroutine(spiningRoutine);

            spiningRoutine = null;

            transform.localEulerAngles = originalLocalEulerAngles;
        }
    }
}