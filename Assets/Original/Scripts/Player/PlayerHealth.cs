using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float lerpTimer;
    private float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    [SerializeField] private Scenefader scenefader;
    [SerializeField] private TextAsset deathDialogue;
    [SerializeField] private AudioClip damageSound;
    public AudioSource damageSource;

    public GameObject redScreen;

    void Start(){
        health = maxHealth;
    }

    void Update(){
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (redScreen.GetComponent<Image>().color.a > 0)
        {
            var color = redScreen.GetComponent<Image>().color;
            color.a -= 0.01f;
            redScreen.GetComponent<Image>().color = color;
        }
    }

    void UpdateHealthUI(){
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health/maxHealth;
        if(fillB > hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction){
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage){

        var color = redScreen.GetComponent<Image>().color;
        color.a = 0.35f;
        redScreen.GetComponent<Image>().color = color;
        damageSource.clip = damageSound;
        damageSource.Play();

        health -= damage;
        lerpTimer = 0f;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void heal(float healAmount){
        health += healAmount;
        lerpTimer = 0f;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public IEnumerator Die(){
        StoryManager.instance.EnterDialogue(deathDialogue, null);
        yield return new WaitForSeconds(1.5f);
        scenefader.FadeTo("GameWorldRework");
    }

}
