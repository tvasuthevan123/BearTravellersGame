using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spawnGroup;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            spawnGroup.SetActive(!spawnGroup.activeSelf);
            gameObject.SetActive(false);
        }
    }
}
