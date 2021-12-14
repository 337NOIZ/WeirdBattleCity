
using System.Collections;

using UnityEngine;

public sealed class ParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleEffectCode _particleEffectCode = ParticleEffectCode.None;

    [SerializeField] private ParticleSystem _particleSystem = null;

    public ParticleEffectCode particleEffectCode { get => _particleEffectCode; }

    public void Play()
    {
        Stop();

        _play = Play_();

        StartCoroutine(_play);
    }

    private IEnumerator _play = null;

    private IEnumerator Play_()
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