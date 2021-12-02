
using System.Collections;

using UnityEngine;

public abstract class ParticleEffect : MonoBehaviour
{
    public abstract ParticleEffectCode particleEffectCode { get; }

    [Space]

    [SerializeField] private ParticleSystem _particleSystem = null;

    public new ParticleSystem particleSystem { get; private set; }

    public void Initialize()
    {
        particleSystem = _particleSystem;
    }

    public void PlayParticleSystem()
    {
        StopParticleSystem();

        playRoutine = PlayRoutine();

        StartCoroutine(playRoutine);
    }

    private IEnumerator playRoutine = null;

    private IEnumerator PlayRoutine()
    {
        particleSystem.Play();

        while (particleSystem.isPlaying == true) yield return null;

        Disable();
    }

    public void StopParticleSystem()
    {
        if (playRoutine != null)
        {
            StopCoroutine(playRoutine);

            particleSystem.Stop();

            playRoutine = null;
        }
    }

    public void Disable()
    {
        StopParticleSystem();

        gameObject.SetActive(false);

        ObjectPool.instance.Push(this);
    }
}