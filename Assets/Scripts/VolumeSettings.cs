using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer SFXMixer;

    void Start()
    {
        // Aus Player Preferences Lautstärkeeinstellungen holen
        musicMixer.SetFloat("volume", PlayerPrefs.GetFloat("volumeMusic"));
        SFXMixer.SetFloat("volume", PlayerPrefs.GetFloat("volumeSFX"));
    }
}
