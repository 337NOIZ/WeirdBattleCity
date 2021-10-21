
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public Dictionary<string, AudioClip> audioClips { get; private set; } = new Dictionary<string, AudioClip>();

    public AudioSource backgroundMusicAudioSource { get; private set; }

    public AudioSource[] soundEffectAudioSources { get; private set; }

    public float backgroundMusicMasterVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("BackgroundMusicMasterVolume") != true)
            {
                return backgroundMusicMasterVolume = 1f;
            }

            return PlayerPrefs.GetFloat("BackgroundMusicMasterVolume");
        }

        set
        {
            if (value > 1f)
            {
                value = 1f;
            }

            else if (value < 0f)
            {
                value = 0f;
            }

            backgroundMusicAudioSource.volume *= value;

            PlayerPrefs.SetFloat("Background Music Master Volume", value);
        }
    }

    public float soundEffectMasterVolume
    {
        get
        {
            if (PlayerPrefs.HasKey("Sound Effect Master Volume") != true)
            {
                return soundEffectMasterVolume = 1f;
            }

            return PlayerPrefs.GetFloat("Sound Effect Master Volume");
        }

        set
        {
            if (value > 1f)
            {
                value = 1f;
            }

            else if (value < 0f)
            {
                value = 0f;
            }

            for (int index = 0; index < soundEffectAudioSources.Length; ++index)
            {
                soundEffectAudioSources[index].volume *= value;
            }

            PlayerPrefs.SetFloat("Sound Effect Master Volume", value);
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        instance = this;

        Initialize();
    }

    private void Initialize()
    {
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("Audio Clips");

        for (int index = 0; index < audioClips.Length; ++index)
        {
            this.audioClips.Add(audioClips[index].name, audioClips[index]);
        }

        backgroundMusicAudioSource = gameObject.AddComponent<AudioSource>();

        backgroundMusicAudioSource.playOnAwake = false;

        backgroundMusicAudioSource.loop = true;

        soundEffectAudioSources = new AudioSource[10];

        for (int index = 0; index < soundEffectAudioSources.Length; ++index)
        {
            soundEffectAudioSources[index] = gameObject.AddComponent<AudioSource>();

            soundEffectAudioSources[index].playOnAwake = false;
        }
    }

    public void PlayBackgroundMusic(AudioClip BackgroundMusicAudioClip, float volume)
    {
        backgroundMusicAudioSource.clip = BackgroundMusicAudioClip;

        backgroundMusicAudioSource.volume = volume;

        backgroundMusicAudioSource.Play();
    }

    public void PlayBackgroundMusic(string BackgroundMusicName, float volume)
    {
        backgroundMusicAudioSource.clip = audioClips[BackgroundMusicName];

        backgroundMusicAudioSource.volume = volume;

        backgroundMusicAudioSource.Play();
    }

    public void PlaySoundEffect(string soundEffectNameString, bool loop, float soundEffectVolume)
    {
        if (soundEffectNameString != "")
        {
            for (int index = 0; index < soundEffectAudioSources.Length; ++index)
            {
                if (soundEffectAudioSources[index].isPlaying == false)
                {
                    soundEffectAudioSources[index].clip = audioClips[soundEffectNameString];

                    soundEffectAudioSources[index].volume = soundEffectVolume;

                    soundEffectAudioSources[index].loop = loop;

                    soundEffectAudioSources[index].Play();

                    break;
                }
            }
        }
    }

    private void StopSoundEffect(string audioClipName)
    {
        for (int index = 0; index < soundEffectAudioSources.Length; ++index)
        {
            if (soundEffectAudioSources[index].clip.name == audioClipName)
            {
                if (soundEffectAudioSources[index].isPlaying == true)
                {
                    soundEffectAudioSources[index].Stop();

                    break;
                }
            }
        }
    }

    public void StopSoundEffectAll()
    {
        for (int index = 0; index < soundEffectAudioSources.Length; ++index)
        {
            soundEffectAudioSources[index].Stop();
        }
    }

    public void FadeAudioListenerVolume(float virtualAudioListenerVolume, float targetAudioListenerVolume, float routineTime)
    {
        StopFadeAudioListenerVolume();

        _fadeAudioListenerVolume = _FadeAudioListenerVolume(virtualAudioListenerVolume, targetAudioListenerVolume, routineTime);

        StartCoroutine(_fadeAudioListenerVolume);
    }

    public void FadeAudioListenerVolume(float targetAudioListenerVolume, float routineTime)
    {
        FadeAudioListenerVolume(AudioListener.volume, targetAudioListenerVolume, routineTime);
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

    private IEnumerator _FadeAudioListenerVolume(float virtualAudioListenerVolume, float targetAudioListenerVolume, float routineTime)
    {
        if (targetAudioListenerVolume > 1f)
        {
            targetAudioListenerVolume = 1f;
        }

        else if (targetAudioListenerVolume < 0f)
        {
            targetAudioListenerVolume = 0f;
        }

        if (routineTime > 0f)
        {
            float maxDelta = (virtualAudioListenerVolume >= targetAudioListenerVolume ? virtualAudioListenerVolume - targetAudioListenerVolume : targetAudioListenerVolume - virtualAudioListenerVolume) / routineTime;

            while (AudioListener.volume != targetAudioListenerVolume)
            {
                AudioListener.volume = Mathf.MoveTowards(AudioListener.volume, targetAudioListenerVolume, maxDelta * Time.deltaTime);

                yield return null;
            }
        }

        else
        {
            AudioListener.volume = targetAudioListenerVolume;
        }

        _fadeAudioListenerVolume = null;
    }
}
