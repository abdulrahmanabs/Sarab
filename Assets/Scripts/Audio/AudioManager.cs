using AYellowpaper.SerializedCollections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    SerializedDictionary<AudioClipNames, AudioClip> _clips;

    AudioSource _audioSource;
    public AudioSource musicSource;
    public AudioSource sfxSource;

   
    public void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MuteMusic(bool mute)
    {
        musicSource.mute = mute;
    }

    public void MuteSFX(bool mute)
    {
        sfxSource.mute = mute;
    }

}
