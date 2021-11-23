
using System.Collections;

using UnityEngine;

public sealed class CameraEffect : MonoBehaviour
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
        StopShakeCamera();

        _shakeCamera = _ShakeCamera(force, duration);

        StartCoroutine(_shakeCamera);
    }

    private IEnumerator _shakeCamera = null;

    private IEnumerator _ShakeCamera(float force, float duration)
    {
        if (force != 0f && duration != 0f)
        {
            var maxDelta = force / duration;

            while (force > 0f)
            {
                transform.localPosition = Random.insideUnitSphere * force + originalPosition;

                force = Mathf.MoveTowards(force, 0f, maxDelta * Time.deltaTime);

                yield return null;
            }

            transform.localPosition = originalPosition;
        }

        _shakeCamera = null;
    }

    public void StopShakeCamera()
    {
        if (_shakeCamera != null)
        {
            StopCoroutine(_shakeCamera);

            transform.localPosition = originalPosition;

            _shakeCamera = null;
        }
    }
}