using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public AudioMixerGroup mixer;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    // Liste von Sounds (-> Sound.cs) im Editor ermöglichen:
    public Sound[] sounds;

    void Awake()
    {
        // Den Sounds ihre jeweiligen Eigenschaften zuweisen:
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            if (sound.pitch != 0)
                sound.source.pitch = sound.pitch;
            else
                sound.source.pitch = 1;

            sound.source.outputAudioMixerGroup = sound.mixer;
            sound.source.playOnAwake = false;
        }
    }

    public void Play(string name)
    {
        // Den zu spielenden Sound aus dem sounds[] Array finden
        // (Array.Find in "sounds[]", Ergebnis soll sein Sound, wo sound.name == parameter "name")
        Sound singleSound = Array.Find(sounds, sound => sound.name == name);
        singleSound.source.Play();
    }

    public void PlayWithDelay(string name, float delay)
    {
        // Den zu spielenden Sound aus dem sounds[] Array finden
        // (Array.Find in "sounds[]", Ergebnis soll sein Sound, wo sound.name == parameter "name")
        Sound singleSound = Array.Find(sounds, sound => sound.name == name);
        singleSound.source.PlayDelayed(delay);
    }
}
