using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Audio;

public sealed class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; } = null;

    [SerializeField] private AudioMixer _audioMixer = null;

    [SerializeField] private AudioSourceMaster[] _audioSourcePrefabs = null;

    private Dictionary<AudioClipCode, AudioSourceMaster> _audioSourcePrefabDictionary = new Dictionary<AudioClipCode, AudioSourceMaster>();

    private Dictionary<AudioClipCode, Stack<AudioSourceMaster>> _audioSourcePool = new Dictionary<AudioClipCode, Stack<AudioSourceMaster>>();

    private float _masterVolume;

    public float masterVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("MasterVolume") != true)
            {
                return 1f;
            }

            return _masterVolume;
        }

        set
        {
            _masterVolume = value;

            if (_masterVolume > 1f)
            {
                _masterVolume = 1f;
            }

            else if (_masterVolume < 0f)
            {
                _masterVolume = 0f;
            }

            _audioMixer.SetFloat("MasterVolume", _masterVolume);

            PlayerPrefs.SetFloat("MasterVolume", _backgroundMusicVolume);
        }
    }

    private float _backgroundMusicVolume;

    public float backgroundMusicVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("BackgroundMusicVolume") != true)
            {
                return 1f;
            }

            return _backgroundMusicVolume;
        }

        set
        {
            _backgroundMusicVolume = value;

            if (_backgroundMusicVolume > 1f)
            {
                _backgroundMusicVolume = 1f;
            }

            else if (_backgroundMusicVolume < 0f)
            {
                _backgroundMusicVolume = 0f;
            }

            _audioMixer.SetFloat("BackgroundMusicVolume", _masterVolume);

            PlayerPrefs.SetFloat("BackgroundMusicVolume", _backgroundMusicVolume);
        }
    }

    private float _soundEffectVolume;

    public float soundEffectVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("SoundEffectVolume") != true)
            {
                return 1f;
            }

            return _soundEffectVolume;
        }

        set
        {
            _soundEffectVolume = value;

            if (_soundEffectVolume > 1f)
            {
                _soundEffectVolume = 1f;
            }

            else if (_soundEffectVolume < 0f)
            {
                _soundEffectVolume = 0f;
            }

            _audioMixer.SetFloat("SoundEffectVolume", _masterVolume);

            PlayerPrefs.SetFloat("SoundEffectVolume", _soundEffectVolume);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;

            Initialize();

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Initialize()
    {
        int index_Max = _audioSourcePrefabs.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _audioSourcePrefabDictionary.Add(_audioSourcePrefabs[index].audioClipCode, _audioSourcePrefabs[index]);
        }

        foreach (AudioClipCode audioClipCode in Enum.GetValues(typeof(AudioClipCode)))
        {
            _audioSourcePool.Add(audioClipCode, new Stack<AudioSourceMaster>());
        }

        masterVolume = masterVolume;

        backgroundMusicVolume = backgroundMusicVolume;

        soundEffectVolume = soundEffectVolume;
    }

    public AudioSourceMaster Pop(AudioClipCode audioClipCode)
    {
        AudioSourceMaster audioSource;

        if (_audioSourcePool[audioClipCode].Count > 0)
        {
            audioSource = _audioSourcePool[audioClipCode].Pop();
        }

        else
        {
            audioSource = Instantiate(_audioSourcePrefabDictionary[audioClipCode]);

            audioSource.Awaken();
        }

        return audioSource;
    }

    public void Push(AudioSourceMaster audioSource)
    {
        audioSource.transform.parent = transform;

        audioSource.transform.localPosition = Vector3.zero;

        _audioSourcePool[audioSource.audioClipCode].Push(audioSource);
    }

    public void StartFadeAudioListenerVolume(float virtualCurrentVolume, float targetVolume, float virtuaFadeTime)
    {
        StopFadeAudioListenerVolume();

        _fadeAudioListenerVolume = _FadeAudioListenerVolume(virtualCurrentVolume, targetVolume, virtuaFadeTime);

        StartCoroutine(_fadeAudioListenerVolume);
    }

    public void StartFadeAudioListenerVolume(float targetVolume, float fadeTime)
    {
        StartFadeAudioListenerVolume(AudioListener.volume, targetVolume, fadeTime);
    }

    public void StopFadeAudioListenerVolume()
    {
        if (_fadeAudioListenerVolume != null)
        {
            StopCoroutine(_fadeAudioListenerVolume);

            _fadeAudioListenerVolume = null;
        }
    }

    private IEnumerator _fadeAudioListenerVolume = null;

    private IEnumerator _FadeAudioListenerVolume(float virtualCurrentVolume, float targetVolume, float virtualFadeTime)
    {
        if (targetVolume > 1f)
        {
            targetVolume = 1f;
        }

        else if (targetVolume < 0f)
        {
            targetVolume = 0f;
        }

        if (virtualFadeTime > 0f)
        {
            float maxDelta = (virtualCurrentVolume >= targetVolume ? virtualCurrentVolume - targetVolume : targetVolume - virtualCurrentVolume) / virtualFadeTime;

            while (AudioListener.volume != targetVolume)
            {
                AudioListener.volume = Mathf.MoveTowards(AudioListener.volume, targetVolume, maxDelta * Time.deltaTime);

                yield return null;
            }
        }

        else
        {
            AudioListener.volume = targetVolume;
        }

        _fadeAudioListenerVolume = null;
    }

    public void StartFadeMasterVolume(float virtualCurrentVolume, float targetVolume, float virtualFadeTime)
    {
        StopFadeMasterVolume();

        _fadeMasterVolume = _FadeMasterVolume(virtualCurrentVolume, targetVolume, virtualFadeTime);

        StartCoroutine(_fadeMasterVolume);
    }

    public void StartFadeMasterVolume(float targetVolume, float fadeTime)
    {
        StartFadeMasterVolume(_masterVolume, targetVolume, fadeTime);
    }

    public void StopFadeMasterVolume()
    {
        if (_fadeMasterVolume != null)
        {
            StopCoroutine(_fadeMasterVolume);

            _fadeMasterVolume = null;
        }
    }

    private IEnumerator _fadeMasterVolume = null;

    private IEnumerator _FadeMasterVolume(float virtualCurrentVolume, float targetVolume, float virtualFadeTime)
    {
        if (targetVolume > 1f)
        {
            targetVolume = 1f;
        }

        else if (targetVolume < 0f)
        {
            targetVolume = 0f;
        }

        if (virtualFadeTime > 0f)
        {
            float maxDelta = (virtualCurrentVolume >= targetVolume ? virtualCurrentVolume - targetVolume : targetVolume - virtualCurrentVolume) / virtualFadeTime;

            while (_masterVolume != targetVolume)
            {
                masterVolume = Mathf.MoveTowards(_masterVolume, targetVolume, maxDelta * Time.deltaTime);

                yield return null;
            }
        }

        else
        {
            masterVolume = targetVolume;
        }

        _fadeMasterVolume = null;
    }
}