
using System.Collections;

using UnityEngine;

public class TransformController : MonoBehaviour
{
    public new Transform transform { get; private set; } = null;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private IEnumerator _moveTransformLocalPosition = null;

    private IEnumerator _MoveTransformLocalPosition(Vector3 virtualTransformLocalPosition, Vector3 targetTransformLocalPosition, float lapTime)
    {
        if (lapTime > 0f)
        {
            float maxDeltaX = (virtualTransformLocalPosition.x >= targetTransformLocalPosition.x ? virtualTransformLocalPosition.x - targetTransformLocalPosition.x : targetTransformLocalPosition.x - virtualTransformLocalPosition.x) / lapTime;

            float maxDeltaY = (virtualTransformLocalPosition.y >= targetTransformLocalPosition.y ? virtualTransformLocalPosition.y - targetTransformLocalPosition.y : targetTransformLocalPosition.y - virtualTransformLocalPosition.y) / lapTime;

            float maxDeltaZ = (virtualTransformLocalPosition.z >= targetTransformLocalPosition.z ? virtualTransformLocalPosition.z - targetTransformLocalPosition.z : targetTransformLocalPosition.z - virtualTransformLocalPosition.z) / lapTime;

            while (transform.localPosition != targetTransformLocalPosition)
            {
                float x = Mathf.MoveTowards(transform.localPosition.x, targetTransformLocalPosition.x, maxDeltaX * Time.deltaTime);

                float y = Mathf.MoveTowards(transform.localPosition.y, targetTransformLocalPosition.y, maxDeltaY * Time.deltaTime);

                float z = Mathf.MoveTowards(transform.localPosition.z, targetTransformLocalPosition.z, maxDeltaZ * Time.deltaTime);

                transform.localPosition = new Vector3(x, y, z);

                yield return null;
            }
        }

        else
        {
            transform.localPosition = targetTransformLocalPosition;
        }

        _moveTransformLocalPosition = null;
    }

    public IEnumerator MoveTransformLocalPosition(Vector3 virtualTransformLocalPosition, Vector3 targetTransformLocalPosition, float lapTime)
    {
        if (_moveTransformLocalPosition != null)
        {
            StopCoroutine(_moveTransformLocalPosition);
        }

        _moveTransformLocalPosition = _MoveTransformLocalPosition(virtualTransformLocalPosition, targetTransformLocalPosition, lapTime);

        StartCoroutine(_moveTransformLocalPosition);

        while (_moveTransformLocalPosition != null) yield return null;
    }

    public IEnumerator MoveTransformLocalPosition(Vector3 targetTransformLocalPosition, float lapTime)
    {
        yield return MoveTransformLocalPosition(transform.localPosition, targetTransformLocalPosition, lapTime);
    }

    private IEnumerator _rotateTransformLocalEulerAngles = null;

    private IEnumerator _RotateTransformLocalEulerAngles(Vector3 targetTransformLocalEulerAngles, float lapTime)
    {
        if (lapTime > 0f)
        {
            Vector3 maxDelta = default;

            maxDelta.x = (transform.localEulerAngles.x >= targetTransformLocalEulerAngles.x ? transform.localEulerAngles.x - targetTransformLocalEulerAngles.x : targetTransformLocalEulerAngles.x - transform.localEulerAngles.x) / lapTime;

            maxDelta.y = (transform.localEulerAngles.y >= targetTransformLocalEulerAngles.y ? transform.localEulerAngles.y - targetTransformLocalEulerAngles.y : targetTransformLocalEulerAngles.y - transform.localEulerAngles.y) / lapTime;

            maxDelta.z = (transform.localEulerAngles.z >= targetTransformLocalEulerAngles.z ? transform.localEulerAngles.z - targetTransformLocalEulerAngles.z : targetTransformLocalEulerAngles.z - transform.localEulerAngles.z) / lapTime;

            Vector3 transformLocalEulerAngles = default;

            while (true)
            {
                transformLocalEulerAngles.x = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, targetTransformLocalEulerAngles.x, maxDelta.x * Time.deltaTime);

                transformLocalEulerAngles.y = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, targetTransformLocalEulerAngles.y, maxDelta.y * Time.deltaTime);

                transformLocalEulerAngles.z = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, targetTransformLocalEulerAngles.z, maxDelta.z * Time.deltaTime);

                transform.localEulerAngles = transformLocalEulerAngles;

                if (transform.localEulerAngles == targetTransformLocalEulerAngles)
                {
                    break;
                }

                yield return null;
            }
        }

        else
        {
            transform.localEulerAngles = targetTransformLocalEulerAngles;
        }

        _rotateTransformLocalEulerAngles = null;
    }

    public void RotateTransformLocalEulerAngles(Vector3 targetTransformLocalEulerAngles, float lapTime)
    {
        if (_rotateTransformLocalEulerAngles != null)
        {
            StopCoroutine(_rotateTransformLocalEulerAngles);
        }

        _rotateTransformLocalEulerAngles = _RotateTransformLocalEulerAngles(targetTransformLocalEulerAngles, lapTime);

        StartCoroutine(_rotateTransformLocalEulerAngles);
    }

    private IEnumerator _spiningTransformLocalEulerAngles = null;

    private IEnumerator _SpiningTransformLocalEulerAngles(Vector3 direction)
    {
        while (true)
        {
            transform.localEulerAngles += direction * Time.deltaTime;

            yield return null;
        }
    }

    public void SpiningTransformLocalEulerAngles(Vector3 direction)
    {
        StopSpiningTransformLocalEulerAngles();

        _spiningTransformLocalEulerAngles = _SpiningTransformLocalEulerAngles(direction);

        StartCoroutine(_spiningTransformLocalEulerAngles);
    }

    public void StopSpiningTransformLocalEulerAngles()
    {
        if (_spiningTransformLocalEulerAngles != null)
        {
            StopCoroutine(_spiningTransformLocalEulerAngles);

            _spiningTransformLocalEulerAngles = null;
        }
    }

    private IEnumerator _floating = null;

    private IEnumerator _Floating(Vector3 delta, float speed)
    {
        Vector3 localPosition = transform.localPosition;

        while (true)
        {
            localPosition.x = delta.x * Mathf.Sin(Time.time * speed);

            localPosition.y = delta.y * Mathf.Sin(Time.time * speed);

            localPosition.z = delta.z * Mathf.Sin(Time.time * speed);

            transform.localPosition = localPosition;

            yield return null;
        }
    }

    public void Floating(Vector3 delta, float speed)
    {
        StopFloating();

        _floating = _Floating(delta, speed);

        StartCoroutine(_floating);
    }

    public void StopFloating()
    {
        if (_floating != null)
        {
            StopCoroutine(_floating);

            _floating = null;
        }
    }
}