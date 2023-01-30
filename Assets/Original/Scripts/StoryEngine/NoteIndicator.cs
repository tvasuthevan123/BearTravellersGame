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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player"){
            nearbyIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player"){
            nearbyIndicator.SetActive(false);
        }
    }
}