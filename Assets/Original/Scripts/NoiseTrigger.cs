using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip bushNoise;
    [SerializeField] private AudioClip leaveNoise;
    private MusicManager musicManager;
    public GameObject noiseLocation;

    private void Start()
    {
        musicManager = MusicManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "bush")
            {
                AudioSource.PlayClipAtPoint(bushNoise, noiseLocation.transform.position);
                this.GetComponent<Collider>().enabled = false;

            } 
            if (this.gameObject.tag == "ending")
            {
                musicManager.PlayEndingnMusic();
                this.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
