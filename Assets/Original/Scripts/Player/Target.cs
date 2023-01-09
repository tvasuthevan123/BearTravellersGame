using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDmg(float amount){
        health -= amount;
        if(health <= 0f){
            Die();
        }
    }

    void Die(){
        if(gameObject.GetComponent<ZombieCharacterControl>() != null){
            gameObject.GetComponent<ZombieCharacterControl>().Die();
            return;
        }
        Destroy(gameObject);
    }
}
