using UnityEngine.Audio;
using System;
using UnityEngine;
public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    private AudioSource _audioSource;
    private string _currentStageAudio;

    void Awake()
    {
        _audioSource= GetComponent<AudioSource>();
        
        foreach (Sound s in sounds)
        {
            if(!s.source)
                s.source = _audioSource;
        }
    }
    

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        _audioSource.clip = s.clip;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0,1)]
        public float volume = 1;
        [Range(-3,3)]
        public float pitch = 1;
        public bool loop = false;
        public AudioSource source;

        public Sound()
        {
            volume = 1;
            pitch = 1;
            loop = false;
        }
    }
}
