using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private TimelineDirector director;
    [SerializeField] private int cutsceneTriggered;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            director.Play(cutsceneTriggered);
        }
    }
}
