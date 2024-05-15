using UnityEngine;

using UnityEngine.UI;

public sealed class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _backgroundMusicVolumeSlider = null;

    [SerializeField] private Slider _soundEffectVolumeSlider = null;

    private void Start()
    {
        _backgroundMusicVolumeSlider.value = AudioManager.instance.backgroundMusicVolume;

        _soundEffectVolumeSlider.value = AudioManager.instance.backgroundMusicVolume;
    }

    public void UpdateBackgroundMusicVolume()
    {
        AudioManager.instance.backgroundMusicVolume = _backgroundMusicVolumeSlider.value;
    }

    public void UpdateSoundEffectVolume()
    {
        AudioManager.instance.soundEffectVolume = _soundEffectVolumeSlider.value;
    }
}