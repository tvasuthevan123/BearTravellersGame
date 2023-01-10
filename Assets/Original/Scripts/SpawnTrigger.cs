using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spawnGroup;
    [SerializeField] private string section;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            spawnGroup.SetActive(!spawnGroup.activeSelf);
            if(section != null)
                StoryManager.instance.SetVariable("section", section);
            gameObject.SetActive(false);
        }
    }
}
