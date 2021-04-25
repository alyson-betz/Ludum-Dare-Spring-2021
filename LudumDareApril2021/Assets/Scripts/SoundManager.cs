using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public enum Sound
    {
        BunnyWalk,
    }

    public AudioClip[] SoundAudioClips;
    private GameObject[] SoundObjects;

    public void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.Play(clip);
    }

    public void StopSound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

}
