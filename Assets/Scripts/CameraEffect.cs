
using System.Collections;

using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public static CameraEffect instance { get; private set; } = null;

    private Vector3 originalPosition;

    private void Awake()
    {
        instance = this;

        originalPosition = transform.localPosition;
    }

    public void ShakeCamera(float force, float duration)
    {
        TryStopShakeCamera();

        shakeCameraCoroutine = ShakeCameraCoroutine(force, duration);

        StartCoroutine(shakeCameraCoroutine);
    }

    private IEnumerator shakeCameraCoroutine = null;

    private IEnumerator ShakeCameraCoroutine(float force, float duration)
    {
        if (force != 0 && duration != 0)
        {
            float maxDelta = force / duration;

            while (force > 0f)
            {
                transform.localPosition = Random.insideUnitSphere * force + originalPosition;

                force = Mathf.MoveTowards(force, 0f, maxDelta * Time.deltaTime);

                yield return null;
            }

            transform.localPosition = originalPosition;
        }

        shakeCameraCoroutine = null;
    }

    public void TryStopShakeCamera()
    {
        if (shakeCameraCoroutine != null)
        {
            StopCoroutine(shakeCameraCoroutine);

            transform.localPosition = originalPosition;

            shakeCameraCoroutine = null;
        }
    }
}
