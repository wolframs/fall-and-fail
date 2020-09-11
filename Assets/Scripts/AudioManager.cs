using UnityEngine.Audio;
using UnityEngine;
using System;

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
            sound.source.pitch = sound.pitch;

            sound.source.outputAudioMixerGroup = sound.mixer;
        }
    }

    public void Play(string name)
    {
        // Den zu spielenden Sound aus dem sounds[] Array finden (Array.Find in "sounds[]", Ergebnis soll sein Sound, wo sound.name == parameter "name")
        Sound singleSound = Array.Find(sounds, sound => sound.name == name);
        singleSound.source.Play();
    }
}
