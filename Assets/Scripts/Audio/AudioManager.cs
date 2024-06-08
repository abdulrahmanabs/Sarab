using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    SerializedDictionary<AudioClipNames, AudioClip> _clips;

    AudioSource _audioSource;

    public void PlayAudio(AudioClipNames clipName)
    {
        if (_clips.ContainsKey(clipName))
        {
            _audioSource.PlayOneShot(_clips[clipName]);
        }
        else
        {
            Debug.LogWarning("Audio clip with name " + clipName + " not found in the dictionary.");
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
