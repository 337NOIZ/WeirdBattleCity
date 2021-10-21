
using UnityEngine;

using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Space]

    [SerializeField]
    
    private Slider backgroundMusicVolumeSlider = null;

    [SerializeField]
    
    private Slider soundEffectVolumeSlider = null;

    private void Start()
    {
        backgroundMusicVolumeSlider.value = AudioManager.instance.backgroundMusicMasterVolume;

        soundEffectVolumeSlider.value = AudioManager.instance.backgroundMusicMasterVolume;
    }

    public void UpdateBackgroundMusicVolume()
    {
        AudioManager.instance.backgroundMusicMasterVolume = backgroundMusicVolumeSlider.value;
    }

    public void UpdateSoundEffectVolume()
    {
        AudioManager.instance.soundEffectMasterVolume = soundEffectVolumeSlider.value;
    }
}