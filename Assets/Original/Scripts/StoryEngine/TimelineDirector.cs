using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineDirector : MonoBehaviour
{
    public Scenefader scenefader;
    public PlayableDirector director;
    public GameObject[] cutscenes;

    public void Play(int animIndex){
        cutscenes[animIndex].SetActive(true);
        StartCoroutine(restart());
    }

    IEnumerator restart(){
        yield return new WaitForSeconds(6);
        scenefader.FadeTo("GameWorldRework");
    }

    public void Stop(int animIndex){
        cutscenes[animIndex].SetActive(false);
    }
}
