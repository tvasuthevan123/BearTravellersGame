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

    public AudioSource audioSource;
    public AudioSource audioSource2;
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

            //audioSource = gameObject.GetComponent<AudioSource>();
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
        if (audioSource.clip != spawnMusic || audioSource2 != spawnMusic)
        {
            StopAllCoroutines();

            StartCoroutine(FadeMusic(spawnMusic));
            //audioSource.Stop();
            //audioSource.clip = spawnMusic;
            //audioSource.Play();
        }
    }

    public void PlayEndingnMusic()
    {
        if (audioSource.clip != endingMusic || audioSource2 != endingMusic)
        {
            StopAllCoroutines();

            StartCoroutine(FadeMusic(endingMusic));
        }
    }

    public void PlayTenseMusic()
    {
        if (audioSource.clip != tenseMusic || audioSource2 != tenseMusic)
        {
            StopAllCoroutines();

            StartCoroutine(FadeMusic(tenseMusic));
            //audioSource.Stop();
            //audioSource.clip = tenseMusic;
            //audioSource.Play();
        }
    }

    private IEnumerator FadeMusic(AudioClip test)
    {
        float timeFade = 1f;
        float timePassed = 0;


        if (audioSource.isPlaying)
        {
            audioSource2.clip = test;
            audioSource2.Play();

            while (timePassed < timeFade)
            {
                audioSource2.volume = Mathf.Lerp(0, 0.05f, timePassed / timeFade);
                audioSource.volume = Mathf.Lerp(0.05f, 0, timePassed / timeFade);
                timePassed += Time.deltaTime;
                yield return null;
            }

            audioSource.Stop();

        } else
        {
            audioSource.clip = test;
            audioSource.Play();

            while (timePassed < timeFade)
            {
                audioSource.volume = Mathf.Lerp(0, 0.05f, timePassed / timeFade);
                audioSource2.volume = Mathf.Lerp(0.05f, 0, timePassed / timeFade);
                timePassed += Time.deltaTime;
                yield return null;
            }

            audioSource2.Stop();
        }
        
    }
}