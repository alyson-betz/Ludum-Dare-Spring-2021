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

    private Dictionary<uint, GameObject> assignedSoundDictionary;
    private uint assignedSoundId;

    public void Start()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundGameObjects = new List<GameObject>();
        musicGameObjects = new List<GameObject>();
        assignedSoundDictionary = new Dictionary<uint, GameObject>();
        assignedSoundId = 0;

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
            GameObject soundGameObject = new GameObject("OneShotSound");
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
            GameObject soundGameObject = new GameObject("OneShotSound3D");
            soundGameObject.transform.position = position;
            AudioSource audioSource = CreateAudioSource(soundGameObject, soundAudioClipArray[(int)sound].audioClip, 100f, 1f, 1f, true);
            audioSource.Play();

            //Add to list
            soundGameObjects.Add(soundGameObject);
        }
    }

    // Next two functions are used for continuous sounds (returns an id which you can reference to stop the sound)

    public uint PlaySoundAssigned(Sound sound, Vector3 position)
    {
        GameObject assignedSoundGO = new GameObject("ContinuousSound");
        assignedSoundGO.transform.position = position;
        AudioSource audioSource = CreateAudioSource(assignedSoundGO, soundAudioClipArray[(int)sound].audioClip, 100f, 1f, 1f, true);
        audioSource.Play();

        //Add to assignedSound list
        assignedSoundDictionary.Add(assignedSoundId++, assignedSoundGO);

        return assignedSoundId;
    }

    public bool StopSoundAssigned(uint soundID)
    {
        GameObject assignedSoundGO = null;
        bool soundFound = assignedSoundDictionary.TryGetValue(soundID, out assignedSoundGO);

        if (soundFound)
        {
            assignedSoundGO.GetComponent<AudioSource>().Stop();
            assignedSoundDictionary.Remove(soundID);
        }

        return soundFound;
    }

    public void PlayMusic(Sound sound, float volume)
    {
        GameObject musicGameObject = new GameObject("Music");
        AudioSource audioSource = CreateAudioSource(musicGameObject, soundAudioClipArray[(int)sound].audioClip, 100000f, 0f, volume, true);
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

        foreach (KeyValuePair<uint, GameObject> keyPair in assignedSoundDictionary)
        {
            keyPair.Value.GetComponent<AudioSource>().Stop();
            Destroy(keyPair.Value);
        }
        assignedSoundDictionary.Clear();
        assignedSoundId = 0;
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

    private AudioSource CreateAudioSource(GameObject GameObject, AudioClip clip, float maxDistance, float spatialBlend, float volume, bool loop)
    {
        AudioSource audioSource = GameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.maxDistance = maxDistance;
        audioSource.volume = volume;
        audioSource.spatialBlend = spatialBlend;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = 0f;
        audioSource.loop = loop;
        return audioSource;
    }


    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
