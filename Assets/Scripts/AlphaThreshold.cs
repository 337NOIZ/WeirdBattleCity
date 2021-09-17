
using UnityEngine;

using UnityEngine.UI;

public class AlphaThreshold : MonoBehaviour
{
    [SerializeField] private float alphaThreshold = 0f;

    private void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }
}