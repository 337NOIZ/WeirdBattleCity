
using System.Collections;

using UnityEngine;

public class ImageRepeater : MonoBehaviour
{
    private new Transform transform = null;

    private RectTransform rectTransform = null;

    [Space]

    [SerializeField]

    private float pixelPerSecond = 0f;

    private Vector3 startPosition = default;

    private void Awake()
    {
        transform = GetComponent<Transform>();

        rectTransform = GetComponent<RectTransform>();

        startPosition = transform.localPosition;
    }

    public void StartReapeatImage()
    {
        StopReapeatImage();

        reapeatImageCoroutine = ReapeatImageCoroutine();

        StartCoroutine(reapeatImageCoroutine);
    }

    private IEnumerator reapeatImageCoroutine = null;

    private IEnumerator ReapeatImageCoroutine()
    {
        float value = 0f;

        while(true)
        {
            transform.localPosition = startPosition + Vector3.right * Mathf.Repeat(value, rectTransform.rect.width);

            value += pixelPerSecond * Time.deltaTime;

            yield return null;
        }
        
    }

    public void StopReapeatImage()
    {
        if(reapeatImageCoroutine != null)
        {
            StopCoroutine(reapeatImageCoroutine);

            reapeatImageCoroutine = null;
        }
    }
}