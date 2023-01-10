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
            if (gameObject.CompareTag("zombiehard1")) {
                StoryManager.instance.zombieGroupHardOneCounter--;
                if (StoryManager.instance.zombieGroupHardOneCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombiehard1"));
                }
            }
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
