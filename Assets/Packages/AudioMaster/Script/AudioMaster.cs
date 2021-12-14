
using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Audio;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster instance { get; private set; } = null;

    [SerializeField] private AudioMixer _audioMixer = null;

    [SerializeField] private AudioSourceMaster[] _audioSourceMasterPrefabArray = null;

    private Dictionary<AudioClipCode, AudioSourceMaster> _audioSourceMasterPrefabDictionary = new Dictionary<AudioClipCode, AudioSourceMaster>();

    private Dictionary<AudioClipCode, Stack<AudioSourceMaster>> _audioSourceMasterPool = new Dictionary<AudioClipCode, Stack<AudioSourceMaster>>();

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
        int index_Max = _audioSourceMasterPrefabArray.Length;

        for (int index = 0; index < index_Max; ++index)
        {
            _audioSourceMasterPrefabDictionary.Add(_audioSourceMasterPrefabArray[index].audioClipCode, _audioSourceMasterPrefabArray[index]);
        }

        foreach (AudioClipCode audioClipCode in Enum.GetValues(typeof(AudioClipCode)))
        {
            _audioSourceMasterPool.Add(audioClipCode, new Stack<AudioSourceMaster>());
        }

        masterVolume = masterVolume;

        backgroundMusicVolume = backgroundMusicVolume;

        soundEffectVolume = soundEffectVolume;
    }

    public AudioSourceMaster Pop(AudioClipCode audioClipCode)
    {
        AudioSourceMaster audioSourceMaster;

        if (_audioSourceMasterPool[audioClipCode].Count > 0)
        {
            audioSourceMaster = _audioSourceMasterPool[audioClipCode].Pop();
        }

        else
        {
            audioSourceMaster = Instantiate(_audioSourceMasterPrefabDictionary[audioClipCode]);

            audioSourceMaster.Awaken();
        }

        return audioSourceMaster;
    }

    public void Push(AudioSourceMaster audioSourceMaster)
    {
        audioSourceMaster.transform.parent = transform;

        audioSourceMaster.transform.localPosition = Vector3.zero;

        _audioSourceMasterPool[audioSourceMaster.audioClipCode].Push(audioSourceMaster);
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