
using System.Collections;

using UnityEngine;

public abstract class ParticleEffect : MonoBehaviour
{
    public abstract ParticleEffectCode particleEffectCode { get; }

    [SerializeField] protected ParticleSystem _particleSystem = null;

    public void Play()
    {
        Stop();

        _play = Play_();

        StartCoroutine(_play);
    }

    protected IEnumerator _play = null;

    protected IEnumerator Play_()
    {
        _particleSystem.Play();

        while (_particleSystem.isPlaying == true) yield return null;

        Disable();
    }

    public void Stop()
    {
        if (_play != null)
        {
            StopCoroutine(_play);

            _particleSystem.Stop();

            _play = null;
        }
    }

    public void Disable()
    {
        Stop();

        gameObject.SetActive(false);

        ObjectPool.instance.Push(this);
    }
}