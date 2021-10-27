
using System.Collections;

using UnityEngine;

public class SpinAndFloat : MonoBehaviour
{
    private new Transform transform;

    private Vector3 originalLocalPosition;

    private Vector3 originalLocalEulerAngles;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    public void Floating(Vector3 delta, float speed)
    {
        StopFloating();

        originalLocalPosition = transform.localPosition;

        _floating = _Floating(delta, speed);

        StartCoroutine(_floating);
    }

    private IEnumerator _floating = null;

    private IEnumerator _Floating(Vector3 delta, float speed)
    {
        while (true)
        {
            transform.localPosition = originalLocalPosition + delta * Mathf.Sin(speed * Time.time);

            yield return null;
        }
    }

    public void StopFloating()
    {
        if (_floating != null)
        {
            StopCoroutine(_floating);

            _floating = null;

            transform.localPosition = originalLocalPosition;
        }
    }

    public void Spining(Vector3 direction)
    {
        StopSpining();

        originalLocalEulerAngles = transform.localEulerAngles;

        _spining = _Spining(direction);

        StartCoroutine(_spining);
    }

    private IEnumerator _spining = null;

    private IEnumerator _Spining(Vector3 direction)
    {
        while (true)
        {
            transform.localEulerAngles += direction * Time.deltaTime;

            yield return null;
        }
    }

    public void StopSpining()
    {
        if (_spining != null)
        {
            StopCoroutine(_spining);

            _spining = null;

            transform.localEulerAngles = originalLocalEulerAngles;
        }
    }
}