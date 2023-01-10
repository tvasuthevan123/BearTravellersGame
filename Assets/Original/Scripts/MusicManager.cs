using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MusicManager : MonoBehaviour
{
    public AudioClip mainmenuMusic;
    public AudioClip spawnMusic;
    public AudioClip endingMusic;
    public AudioClip tenseMusic;

    private AudioSource audioSource;
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);

            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.05f;
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public void PlayMenuMusic()
    {
        if (audioSource.clip != mainmenuMusic)
        {
            audioSource.Stop();
            audioSource.clip = mainmenuMusic;
            audioSource.Play();
        }
    }

    public void PlaySpawnMusic()
    {
        if (audioSource.clip != spawnMusic)
        {
            audioSource.Stop();
            audioSource.clip = spawnMusic;
            audioSource.Play();
        }
    }

    public void PlayEndingnMusic()
    {
        if (audioSource.clip != endingMusic)
        {
            audioSource.Stop();
            audioSource.clip = endingMusic;
            audioSource.Play();
        }
    }

    public void PlayTenseMusic()
    {
        if (audioSource.clip != tenseMusic)
        {
            audioSource.Stop();
            audioSource.clip = tenseMusic;
            audioSource.Play();
        }
    }
}