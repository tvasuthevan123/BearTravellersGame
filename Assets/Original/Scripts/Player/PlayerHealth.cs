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

    void Start(){
        health = maxHealth;
    }

    void Update(){
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
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
