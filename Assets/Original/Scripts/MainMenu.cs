using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Scenefader scenefader;
    public void PlayGame(){
        scenefader.FadeTo("GameWorldRework");
    }

    public void QuitGame(){
        Application.Quit();
    }


}
