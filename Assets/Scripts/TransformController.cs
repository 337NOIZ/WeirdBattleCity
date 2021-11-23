
using System.Collections;

using UnityEngine;

public sealed class TransformController : MonoBehaviour
{
    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
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

    public void RotateTransformLocalEulerAngles(Vector3 targetTransformLocalEulerAngles, float lapTime)
    {
        if (_rotateTransformLocalEulerAngles != null)
        {
            StopCoroutine(_rotateTransformLocalEulerAngles);
        }

        _rotateTransformLocalEulerAngles = _RotateTransformLocalEulerAngles(targetTransformLocalEulerAngles, lapTime);

        StartCoroutine(_rotateTransformLocalEulerAngles);
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
}