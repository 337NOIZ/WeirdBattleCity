
using System.Collections;

using UnityEngine;

public sealed class AudioSourceMaster : MonoBehaviour
{
    [SerializeField] private AudioClipCode _audioClipCode = AudioClipCode.None;

    public AudioClipCode audioClipCode { get => _audioClipCode; }

    private AudioSource _audioSource;

    private float _timer;

    public void Awaken()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        Play(0f);
    }

    public void Play(float time)
    {
        if (_play != null)
        {
            _timer = time;
        }

        else
        {
            _play = _Play(time);

            StartCoroutine(_play);
        }
    }

    private IEnumerator _play = null;

    private IEnumerator _Play(float time)
    {
        _timer = time;

        while (_timer > 0f)
        {
            yield return null;

            _timer -= Time.deltaTime;
        }

        _timer = 0f;

        _audioSource.Play();

        while (_audioSource.isPlaying == true) yield return null;

        Stop();
    }

    public void Stop()
    {
        if(_play != null)
        {
            StopCoroutine(_play);

            _timer = 0f;
        }

        _play = null;

        _audioSource.Stop();

        gameObject.SetActive(false);

        AudioMaster.instance.Push(this);
    }

    public void StartFadeVolume(float virtualCurrentVolume, float targetVolume, float fadeTime)
    {
        StopFadeVolume();

        _fadeVolume = _FadeVolume(virtualCurrentVolume, targetVolume, fadeTime);

        StartCoroutine(_fadeVolume);
    }

    public void StartFadeVolume(float targetVolume, float fadeTime)
    {
        StartFadeVolume(_audioSource.volume, targetVolume, fadeTime);
    }

    public void StopFadeVolume()
    {
        if (_fadeVolume != null)
        {
            StopCoroutine(_fadeVolume);

            _fadeVolume = null;
        }
    }

    private IEnumerator _fadeVolume = null;

    private IEnumerator _FadeVolume(float virtualCurrentVolume, float targetVolume, float fadeTime)
    {
        if (targetVolume > 1f)
        {
            targetVolume = 1f;
        }

        else if (targetVolume < 0f)
        {
            targetVolume = 0f;
        }

        if (fadeTime > 0f)
        {
            float maxDelta = (virtualCurrentVolume >= targetVolume ? virtualCurrentVolume - targetVolume : targetVolume - virtualCurrentVolume) / fadeTime;

            while (_audioSource.volume != targetVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, maxDelta * Time.deltaTime);

                yield return null;
            }
        }

        else
        {
            _audioSource.volume = targetVolume;
        }

        _fadeVolume = null;
    }
}