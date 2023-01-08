using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppearingText : MonoBehaviour
{
    private TextMeshPro textElement;
    private bool playerInRange;

    void Awake(){
        textElement = gameObject.GetComponentInChildren<TextMeshPro>();
        textElement.gameObject.SetActive(false);
    }
    void Update()
    {
        if(playerInRange)
            textElement.gameObject.SetActive(true);
        else
            textElement.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player")
            playerInRange=true;
    }

    private void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "Player")
            playerInRange=false;
    }
}
