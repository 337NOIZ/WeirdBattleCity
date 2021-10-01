
using UnityEngine;

using UnityEngine.UI;

public class AlphaThreshold : MonoBehaviour
{
    [Space, SerializeField, Range(0f, 1f)] private float alphaThreshold = 0f;

    private void Awake()
    {
        var image = GetComponent<Image>();

        if(image.sprite != null)
        {
            image.alphaHitTestMinimumThreshold = alphaThreshold;
        }
    }
}