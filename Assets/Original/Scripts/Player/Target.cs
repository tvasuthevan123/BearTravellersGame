using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    private MusicManager musicManager;

    private void Start()
    {
        musicManager = MusicManager.Instance;
    }

    public void TakeDmg(float amount)
    {
        
        if (health <= 0f)
        {
            Debug.Log("zombie already dead");
            return;
        } else
        {
            health -= amount;
        }
 
        if (health <= 0f)
        {
            Die();
            if (gameObject.CompareTag("zombiehard1"))
            {
                StoryManager.instance.zombieGroupHardOneCounter--;
                Debug.Log(StoryManager.instance.zombieGroupHardOneCounter);
                if (StoryManager.instance.zombieGroupHardOneCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombiehard1"));
                    musicManager.PlaySpawnMusic();
                }
            }
            if (gameObject.CompareTag("zombieeasy1"))
            {
                StoryManager.instance.zombieGroupEasyOneCounter--;
                Debug.Log(StoryManager.instance.zombieGroupEasyOneCounter);
                if (StoryManager.instance.zombieGroupEasyOneCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombieeasy1"));
                    musicManager.PlaySpawnMusic();
                }
            }
            if (gameObject.CompareTag("zombiefriend"))
            {
                StoryManager.instance.zombieGroupFriendCounter--;
                Debug.Log(StoryManager.instance.zombieGroupFriendCounter);
                if (StoryManager.instance.zombieGroupFriendCounter <= 0)
                {
                    Destroy(GameObject.FindWithTag("zombiefriend"));
                    musicManager.PlaySpawnMusic();
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
