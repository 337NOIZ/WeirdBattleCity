
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public enum ColorType
{
    componentBaseColor, materialBaseColor, materialEmissionColor
}

public class Discolorator : MonoBehaviour
{
    [Space]

    [SerializeField] private GameObject discolorTarget = null;

    [Space]

    [SerializeField] private ColorType colorType = ColorType.componentBaseColor;

    private SpriteRenderer discolorTargetSpriteRenderer = null;

    private Image discolorTargetImage = null;

    private Text discolorTargetText = null;

    private Light discolorTargetLight = null;

    private MeshRenderer meshRenderer = null;

    private Material discolorTargetMaterial = null;

    private Color _color = new Color(0f, 0f, 0f, 1f);

    private float _intensity = 1f;

    private IEnumerator breathingCoroutine = null;

    private Color originalColor = new Color(1f, 1f, 1f, 1f);

    private float originalIntensity = 1f;

    public bool emission
    {
        get
        {
            return discolorTargetMaterial.IsKeywordEnabled("_EMISSION");
        }

        set
        {
            if (value == true)
            {
                discolorTargetMaterial.EnableKeyword("_EMISSION");

                discolorTargetMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

                SetEmissionColor(_color, _intensity);
            }

            else
            {
                discolorTargetMaterial.DisableKeyword("_EMISSION");

                discolorTargetMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;

                SetEmissionColor(Color.black, 0f);
            }
        }
    }

    public Color color
    {
        get
        {
            return _color;
        }

        set
        {
            _color = value;

            if (colorType == ColorType.componentBaseColor)
            {
                if (discolorTargetSpriteRenderer != null)
                {
                    discolorTargetSpriteRenderer.color = value;
                }

                else if (discolorTargetImage != null)
                {
                    discolorTargetImage.color = value;
                }

                else if (discolorTargetText != null)
                {
                    discolorTargetText.color = value;
                }

                else if (discolorTargetLight != null)
                {
                    discolorTargetLight.color = value;
                }
            }

            else if (colorType == ColorType.materialBaseColor)
            {
                if (discolorTargetMaterial != null)
                {
                    discolorTargetMaterial.SetColor("_BaseColor", value);
                }
            }

            else if (colorType == ColorType.materialEmissionColor)
            {
                if (discolorTargetMaterial != null)
                {
                    SetEmissionColor(value, _intensity);
                }
            }
        }
    }

    public float intensity
    {
        get
        {
            return _intensity;
        }

        set
        {
            _intensity = value;

            if (colorType == ColorType.componentBaseColor)
            {
                if (discolorTargetLight != null)
                {
                    discolorTargetLight.intensity = value;
                }
            }

            else if (colorType == ColorType.materialEmissionColor)
            {
                if (discolorTargetMaterial != null)
                {
                    SetEmissionColor(_color, value);
                }
            }
        }
    }

    private void Awake()
    {
        if (discolorTarget == null)
        {
            discolorTarget = gameObject;
        }

        if (discolorTarget.GetComponent<SpriteRenderer>() != null)
        {
            discolorTargetSpriteRenderer = discolorTarget.GetComponent<SpriteRenderer>();

            discolorTargetMaterial = new Material(discolorTargetSpriteRenderer.material);

            if (discolorTargetMaterial != null)
            {
                discolorTargetSpriteRenderer.material = discolorTargetMaterial;
            }
        }

        else if (discolorTarget.GetComponent<Image>() != null)
        {
            discolorTargetImage = discolorTarget.GetComponent<Image>();

            discolorTargetMaterial = new Material(discolorTargetImage.material);

            if (discolorTargetMaterial != null)
            {
                discolorTargetImage.material = discolorTargetMaterial;
            }
        }

        else if (discolorTarget.GetComponent<Text>() != null)
        {
            discolorTargetText = discolorTarget.GetComponent<Text>();

            discolorTargetMaterial = new Material(discolorTargetText.material);

            if (discolorTargetMaterial != null)
            {
                discolorTargetText.material = discolorTargetMaterial;
            }
        }

        else if (discolorTarget.GetComponent<Light>() != null)
        {
            discolorTargetLight = discolorTarget.GetComponent<Light>();
        }

        else if (discolorTarget.GetComponent<MeshRenderer>() != null)
        {
            meshRenderer = discolorTarget.GetComponent<MeshRenderer>();

            discolorTargetMaterial = new Material(discolorTarget.GetComponent<MeshRenderer>().material);

            if (discolorTargetMaterial != null)
            {
                meshRenderer.material = discolorTargetMaterial;
            }
        }

        if (colorType == ColorType.componentBaseColor)
        {
            if (discolorTargetSpriteRenderer != null)
            {
                _color = discolorTargetSpriteRenderer.color;
            }

            else if (discolorTargetImage != null)
            {
                _color = discolorTargetImage.color;
            }

            else if (discolorTargetText != null)
            {
                _color = discolorTargetText.color;
            }

            else if (discolorTargetLight != null)
            {
                _color = discolorTargetLight.color;

                _intensity = discolorTargetLight.intensity;
            }
        }

        if (colorType == ColorType.materialBaseColor)
        {
            _color = discolorTargetMaterial.GetColor("_BaseColor");
        }

        else if (colorType == ColorType.materialEmissionColor)
        {
            Color emissionColor = discolorTargetMaterial.GetColor("_EmissionColor");

            _intensity = Mathf.Max(emissionColor.r, emissionColor.g, emissionColor.b);

            if (_intensity > 0f)
            {
                _color = emissionColor / _intensity;
            }
        }
    }

    private IEnumerator discolorCoroutine = null;

    private IEnumerator DiscolorCoroutine(Color targetColor, float targetIntensity, float lapTime)
    {
        if (targetColor != _color)
        {
            if (lapTime > 0f)
            {
                Color maxDelta;

                maxDelta.r = (_color.r >= targetColor.r ? _color.r - targetColor.r : targetColor.r - _color.r) / lapTime;

                maxDelta.g = (_color.g >= targetColor.g ? _color.g - targetColor.g : targetColor.g - _color.g) / lapTime;

                maxDelta.b = (_color.b >= targetColor.b ? _color.b - targetColor.b : targetColor.b - _color.b) / lapTime;

                maxDelta.a = (_color.a >= targetColor.a ? _color.a - targetColor.a : targetColor.a - _color.a) / lapTime;

                float maxDelta_i = (_intensity >= targetIntensity ? _intensity - targetIntensity : targetIntensity - _intensity) / lapTime;

                Color color;

                do
                {
                    color.r = Mathf.MoveTowards(_color.r, targetColor.r, maxDelta.r * Time.deltaTime);

                    color.g = Mathf.MoveTowards(_color.g, targetColor.g, maxDelta.g * Time.deltaTime);

                    color.b = Mathf.MoveTowards(_color.b, targetColor.b, maxDelta.b * Time.deltaTime);

                    color.a = Mathf.MoveTowards(_color.a, targetColor.a, maxDelta.a * Time.deltaTime);

                    _intensity = Mathf.MoveTowards(_intensity, targetIntensity, maxDelta_i * Time.deltaTime);

                    this.color = color;

                    yield return null;
                }
                while (_color != targetColor);
            }

            else
            {
                _intensity = targetIntensity;

                color = targetColor;
            }
        }

        discolorCoroutine = null;
    }

    public void StartDiscolor(Color targetColor, float intensity, float lapTime)
    {
        if (discolorCoroutine != null)
        {
            StopCoroutine(discolorCoroutine);
        }

        discolorCoroutine = DiscolorCoroutine(targetColor, intensity, lapTime);

        StartCoroutine(discolorCoroutine);
    }

    public void StartDiscolor(Color targetColor, float lapTime)
    {
        StartDiscolor(targetColor, _intensity, lapTime);
    }

    public void StartDiscolor(float intensity, float lapTime)
    {
        StartDiscolor(_color, intensity, lapTime);
    }

    public void SetEmissionColor(Color color, float intensity)
    {
        _color = color;

        _intensity = intensity;

        discolorTargetMaterial.EnableKeyword("_EMISSION");

        discolorTargetMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        discolorTargetMaterial.SetColor("_EmissionColor", color * intensity);

        RendererExtensions.UpdateGIMaterials(meshRenderer);

        DynamicGI.SetEmissive(meshRenderer, color);

        DynamicGI.UpdateEnvironment();
    }

    public void StartBreathing(Color targetColor, float targetIntensity, float lapTime, float inhaleDelayMin, float inhaleDelayMax, float exhaleDelayMin, float exhaleDelayMax)
    {
        StopBreathing();

        breathingCoroutine = BreathingCoroutine(targetColor, targetIntensity, lapTime, inhaleDelayMin, inhaleDelayMax, exhaleDelayMin, exhaleDelayMax);

        StartCoroutine(breathingCoroutine);
    }

    public void StartBreathing(Color targetColor, float lapTime, float inhaleDelayMin, float inhaleDelayMax, float exhaleDelayMin, float exhaleDelayMax)
    {
        StartBreathing(targetColor, _intensity, lapTime, inhaleDelayMin, inhaleDelayMax, exhaleDelayMin, exhaleDelayMax);
    }

    public void StartBreathing(Color targetColor, float breathingtIntensity, float lapTime)
    {
        StartBreathing(targetColor, breathingtIntensity, lapTime, lapTime, lapTime, lapTime, lapTime);
    }

    public void StartBreathing(float breathingtIntensity, float lapTime, float inhaleDelayMin, float inhaleDelayMax, float exhaleDelayMin, float exhaleDelayMax)
    {
        StartBreathing(_color, breathingtIntensity, lapTime, inhaleDelayMin, inhaleDelayMax, exhaleDelayMin, exhaleDelayMax);
    }

    public void StartBreathing(float breathingtIntensity, float lapTime)
    {
        StartBreathing(_color, breathingtIntensity, lapTime, lapTime, lapTime, lapTime, lapTime);
    }

    public void StopBreathing()
    {
        if (breathingCoroutine != null)
        {
            StopCoroutine(breathingCoroutine);

            breathingCoroutine = null;

            _color = originalColor;
        }
    }

    private IEnumerator BreathingCoroutine(Color targetColor, float targetIntensity, float lapTime, float inhaleDelayMin, float inhaleDelayMax, float exhaleDelayMin, float exhaleDelayMax)
    {
        if (exhaleDelayMin < 0f)
        {
            exhaleDelayMin = 0f;
        }

        if (exhaleDelayMax < 0f)
        {
            exhaleDelayMax = 0f;
        }

        bool loof = exhaleDelayMin + exhaleDelayMax > 0f ? true : false;

        do
        {
            originalColor = _color;

            originalIntensity = _intensity;

            StartDiscolor(targetColor, targetIntensity, lapTime);

            yield return new WaitForSeconds(Random.Range(inhaleDelayMin, inhaleDelayMax));

            StartDiscolor(originalColor, originalIntensity, lapTime);

            yield return new WaitForSeconds(Random.Range(exhaleDelayMin, exhaleDelayMax));
        }
        while (loof);
    }
}