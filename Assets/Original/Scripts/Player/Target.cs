using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDmg(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
            if (gameObject.CompareTag("zombiehard1"))
            {
                StoryManager.instance.zombieGroupHardOneCounter--;
                if (StoryManager.instance.zombieGroupHardOneCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombiehard1"));
                }
            }
            else if (gameObject.CompareTag("zombieeasy1"))
            {
                StoryManager.instance.zombieGroupEasyOneCounter--;
                if (StoryManager.instance.zombieGroupEasyOneCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombieeasy1"));
                }
            }
            else if (gameObject.CompareTag("zombiefriend"))
            {
                StoryManager.instance.zombieGroupFriendCounter--;
                if (StoryManager.instance.zombieGroupFriendCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombiefriend"));
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
