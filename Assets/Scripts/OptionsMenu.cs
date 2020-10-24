using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Toggle DifficultyToggle;

    private void Start()
    {
        // Controls auf Werte aus PlayerPrefs setzen:
        MusicSlider.value = PlayerPrefs.GetFloat("volumeMusic", -5);
        SFXSlider.value = PlayerPrefs.GetFloat("volumeSFX", -5);
        int diff = PlayerPrefs.GetInt("difficulty", 0);
        if (diff != 0)
            DifficultyToggle.isOn = true;
        else
            DifficultyToggle.isOn = false;
    }

    public void SetMusicVolume(float volume)
    {
        // Mixer und Player Preferences updaten:
        MusicMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volumeMusic", volume);
        Debug.Log("PlayerPrefs FLOAT Music Volume: " + PlayerPrefs.GetFloat("volumeMusic"));
    }

    public void SetSFXVolume(float volume)
    {
        // Mixer und Player Preferences updaten:
        SFXMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volumeSFX", volume);
        Debug.Log("PlayerPrefs FLOAT SFX Volume: " + PlayerPrefs.GetFloat("volumeSFX"));
    }

    public void SetDifficulty(bool challenging)
    {
        if (challenging)
            PlayerPrefs.SetInt("difficulty", 1);
        else
            PlayerPrefs.SetInt("difficulty", 0);
        Debug.Log("PlayerPrefs INT Difficulty: " + PlayerPrefs.GetInt("difficulty"));
    }
}
