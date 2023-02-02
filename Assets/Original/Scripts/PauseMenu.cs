using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.UI;
using StarterAssets;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject player, gun, playerUI, narrativeUI;
    public Slider masterSlider, musicSlider, sfxSlider;

    public Scenefader sceneFader;

    public AudioMixer mixer;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        isGamePaused = true;

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);


        player.GetComponent<FirstPersonController>().ToggleInput();
        player.GetComponent<PlayerInput>().DeactivateInput();
        player.GetComponent<HealthItems>().enabled = false;
        player.GetComponent<PlayerPickup>().enabled = false;
        if(gun.activeSelf)
            gun.GetComponent<Gun>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        narrativeUI.SetActive(false);
        Time.timeScale = 0f;
    }
    
    public void Resume()
    {
        isGamePaused = false;

        player.GetComponent<FirstPersonController>().ToggleInput();
        player.GetComponent<PlayerInput>().ActivateInput();
        player.GetComponent<HealthItems>().enabled = true;
        player.GetComponent<PlayerPickup>().enabled = true;
        if(gun.activeSelf)
            gun.GetComponent<Gun>().enabled = false;
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        narrativeUI.SetActive(true);
        Time.timeScale=1f;
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

    public void MainMenu()
    {
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale=1f;
        sceneFader.FadeTo("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
