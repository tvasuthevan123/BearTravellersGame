using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenefader : MonoBehaviour
{
    public Image img;

    void Start(){
        StartCoroutine(fadeIn());
    }
    public void FadeTo(string scene){
        StartCoroutine(fadeOut(scene));
    }

    IEnumerator fadeIn(){
        float t = 1f;
        while(t > 0f){
            t -= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
    }

    IEnumerator fadeOut(string scene){
        float t = 0f;
        while(t < 1f){
            t += Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }

}
