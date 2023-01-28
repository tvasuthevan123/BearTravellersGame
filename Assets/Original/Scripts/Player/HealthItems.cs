using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthItems : MonoBehaviour
{
    public int medkits;
    public PlayerHealth playerHealth;
    public TextMeshProUGUI medkitDisplay;

    [SerializeField] private AudioClip pickupHealthItem;
    [SerializeField] private AudioClip useHealthItem;


    // Start is called before the first frame update
    void Start()
    {
        medkits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        medkitDisplay.text = "Medkits: " + medkits.ToString();
    }

    public void medkitUsage()
    {
        if (medkits > 0)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = useHealthItem;
            audio.Play();
            removeMedkit();
            playerHealth.heal(50);
        }
    }

    public void addMedkit()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = pickupHealthItem;
        audio.Play();
        medkits++;
    }

    public void removeMedkit()
    {
        medkits--;
    }
}
