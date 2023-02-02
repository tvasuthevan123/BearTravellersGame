using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Scenefader scenefader;
    public AudioMixer mixer;

    private MusicManager musicManager;

    public Slider masterSlider, musicSlider, sfxSlider;


    private void Awake()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicManager = MusicManager.Instance;
        musicManager.PlayMenuMusic();
    }
    public void PlayGame(){
        scenefader.FadeTo("GameWorldRework");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void SetMasterVolume(float volume){
        mixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume(float volume){
        mixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void SetMusicVolume(float volume){
        mixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

}
