using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Scenefader scenefader;
    public AudioMixer mixer;
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
