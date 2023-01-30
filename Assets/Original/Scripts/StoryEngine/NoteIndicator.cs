using System;
using UnityEngine;


public class NoteIndicator : MonoBehaviour
{
    [Header("Visual Cues")]
    [SerializeField] public GameObject nearbyIndicator;

    private bool playerInRange;

    private void Awake()
    {
        nearbyIndicator.SetActive(false);
    }

    void Update()
    {
        if(nearbyIndicator == null)
            return;
        if (playerInRange)
            nearbyIndicator.SetActive(true);
        else
            nearbyIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player"){
            playerInRange = false;
            Debug.Log("?? Confusion?");
            if(StoryManager.instance.isNoteOpen){
                Debug.Log("BIG CONFUSION");
                StoryManager.instance.ExitNote();
            }
        }
    }
}