using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ammo : MonoBehaviour
{
    public int ammo;
    public PlayerHealth playerHealth;
    public TextMeshProUGUI ammoDisplay;

    [SerializeField] private AudioClip pickupAmmo;
    public AudioSource ammoSource;

    // Start is called before the first frame update
    void Start()
    {
        ammo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = "Ammo: " + ammo.ToString();
    }

    public void ammoUsage()
    {
        if (ammo > 0)
        {
            ammo--;
        } else
        {
            playerHealth.TakeDamage(10);
        }
    }

    public void addAmmo(int ammoAmount)
    {
        ammoSource.clip = pickupAmmo;
        ammoSource.Play();
        ammo += ammoAmount;
    }
}
