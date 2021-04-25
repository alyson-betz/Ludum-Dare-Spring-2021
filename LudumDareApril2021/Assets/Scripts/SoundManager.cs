using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        BunnyWalkingMedium,
        BunnyWalkingFast,
        BunnyWalkingSlow,
        Crows,
        Crickets,
        Wind,
    }

    public SoundAudioClip[] soundAudioClipArray;
    public Dictionary<Sound, float> soundTimerDictionary;

    private List<GameObject> soundGameObjects;
    private List<GameObject> musicGameObjects;

    public void Start()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundGameObjects = new List<GameObject>();
        musicGameObjects = new List<GameObject>();

        //Bunny walking
        soundTimerDictionary[Sound.BunnyWalkingMedium] = 0f;
        soundTimerDictionary[Sound.BunnyWalkingFast] = 0f;
        soundTimerDictionary[Sound.BunnyWalkingSlow] = 0f;

        //Start the Musics/Ambience (can delete this later)
        PlayMusic(Sound.Crows, 0.3f);
        PlayMusic(Sound.Crickets, 0.2f);
        PlayMusic(Sound.Wind, 0.1f);
    }

    public void PlaySoundOneShot(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(soundAudioClipArray[(int)sound].audioClip);

            //Add to list
            soundGameObjects.Add(soundGameObject);
        }
    }

    public void PlaySoundOneShot3D(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = soundAudioClipArray[(int)sound].audioClip;
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            //Add to list
            soundGameObjects.Add(soundGameObject);
        }
    }

    public void PlayMusic(Sound sound, float volume)
    {
        GameObject musicGameObject = new GameObject("Sound");
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = soundAudioClipArray[(int)sound].audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        //Add to list
        musicGameObjects.Add(musicGameObject);
    }

    public void StopMusic()
    {
        foreach (GameObject musicGameObject in musicGameObjects)
        {
            AudioSource audioSource = musicGameObject.GetComponent<AudioSource>();
            audioSource.Stop();
            Destroy(musicGameObject);
        }
        musicGameObjects.Clear();
    }

    public void ClearSoundEffects()
    {
        foreach (GameObject soundGameObject in soundGameObjects)
        {
            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
            audioSource.Stop();
            Destroy(soundGameObject);
        }
        soundGameObjects.Clear();
    }

    public void Update()
    {
        List<int> listToDelete = new List<int>();

        for(int i = 0; i < soundGameObjects.Count; i++)
        {
            GameObject soundGameObject = soundGameObjects[i];
            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                Destroy(soundGameObject);
                listToDelete.Add(i);
            }
        }

        foreach(int index in listToDelete)
        {
            soundGameObjects.RemoveAt(index);
        }
    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.BunnyWalkingMedium:
            {
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float bunnyWalkMaxTimer = 15f;
                    if (lastTimePlayed + bunnyWalkMaxTimer < Time.time || lastTimePlayed == 0f)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            case Sound.BunnyWalkingSlow:
            {
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float bunnyWalkMaxTimer = 24f;
                    if (lastTimePlayed + bunnyWalkMaxTimer < Time.time || lastTimePlayed == 0f)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            case Sound.BunnyWalkingFast:
            {
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float bunnyWalkMaxTimer = 4f;
                    if (lastTimePlayed + bunnyWalkMaxTimer < Time.time || lastTimePlayed == 0f)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    bool True()
    {
        return true;
    }

}
