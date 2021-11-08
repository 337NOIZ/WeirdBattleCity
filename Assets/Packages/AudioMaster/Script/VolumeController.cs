
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
        backgroundMusicVolumeSlider.value = AudioMaster.instance.backgroundMusicMasterVolume;

        soundEffectVolumeSlider.value = AudioMaster.instance.backgroundMusicMasterVolume;
    }

    public void UpdateBackgroundMusicVolume()
    {
        AudioMaster.instance.backgroundMusicMasterVolume = backgroundMusicVolumeSlider.value;
    }

    public void UpdateSoundEffectVolume()
    {
        AudioMaster.instance.soundEffectMasterVolume = soundEffectVolumeSlider.value;
    }
}