
using System.Collections;

using UnityEngine;

public sealed class LightController : MonoBehaviour
{
    public new Light light { get; private set; } = null;

    public Discolorator discolorator { get; private set; } = null;

    private void Awake()
    {
        light = GetComponent<Light>();

        discolorator = GetComponent<Discolorator>();
    }

    public IEnumerator StartFadeLightIntensityCoroutine(float targetLightIntensity, float lapTime)
    {
        if (fadeLightIntensityCoroutine != null)
        {
            StopCoroutine(fadeLightIntensityCoroutine);

            fadeLightIntensityCoroutine = null;

            yield return null;
        }

        fadeLightIntensityCoroutine = FadeLightIntensityCoroutine(targetLightIntensity, lapTime);

        StartCoroutine(fadeLightIntensityCoroutine);

        while (fadeLightIntensityCoroutine != null)
        {
            yield return null;
        }
    }

    private IEnumerator fadeLightIntensityCoroutine = null;

    private IEnumerator FadeLightIntensityCoroutine(float targetLightIntensity, float lapTime)
    {
        if (targetLightIntensity != light.intensity)
        {
            if (lapTime > 0f)
            {
                float maxDelta = (light.intensity >= targetLightIntensity ? light.intensity - targetLightIntensity : targetLightIntensity - light.intensity) / lapTime;

                while (true)
                {
                    light.intensity = Mathf.MoveTowards(light.intensity, targetLightIntensity, maxDelta * Time.deltaTime);

                    if (light.intensity == targetLightIntensity)
                    {
                        break;
                    }

                    yield return null;
                }
            }
        }

        else
        {
            light.intensity = targetLightIntensity;
        }

        fadeLightIntensityCoroutine = null;
    }
}