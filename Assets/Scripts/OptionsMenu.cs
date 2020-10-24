using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    public Slider MusicSlider;
    public Slider SFXSlider;

    private void Start()
    {
        // Slider auf Werte aus PlayerPrefs setzen:
        MusicSlider.value = PlayerPrefs.GetFloat("volumeMusic", -5);
        SFXSlider.value = PlayerPrefs.GetFloat("volumeSFX", -5);
    }

    public void SetMusicVolume(float volume)
    {
        // Mixer und Player Preferences updaten:
        MusicMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volumeMusic", volume);
    }

    public void SetSFXVolume(float volume)
    {
        // Mixer und Player Preferences updaten:
        SFXMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volumeSFX", volume);
    }
}
