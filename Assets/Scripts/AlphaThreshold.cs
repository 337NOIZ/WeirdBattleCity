
using UnityEngine;

using UnityEngine.UI;

public class AlphaThreshold : MonoBehaviour
{
    [Space]

    [SerializeField] private float alphaThreshold = 0f;

    private void Awake()
    {
        var image = GetComponent<Image>();

        if(image.sprite != null && alphaThreshold > 0f)
        {
            image.alphaHitTestMinimumThreshold = alphaThreshold;
        }
    }
}