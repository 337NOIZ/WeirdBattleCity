
using UnityEngine;

using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _backgroundMusicVolumeSlider = null;

    [SerializeField] private Slider _soundEffectVolumeSlider = null;

    private void Start()
    {
        _backgroundMusicVolumeSlider.value = AudioMaster.instance.backgroundMusicVolume;

        _soundEffectVolumeSlider.value = AudioMaster.instance.backgroundMusicVolume;
    }

    public void UpdateBackgroundMusicVolume()
    {
        AudioMaster.instance.backgroundMusicVolume = _backgroundMusicVolumeSlider.value;
    }

    public void UpdateSoundEffectVolume()
    {
        AudioMaster.instance.soundEffectVolume = _soundEffectVolumeSlider.value;
    }
}