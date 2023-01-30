using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip bushNoise;
    [SerializeField] private AudioClip leaveNoise;
    public GameObject noiseLocation;

    void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.tag == "bush")
            {
                AudioSource.PlayClipAtPoint(bushNoise, noiseLocation.transform.position);

            } else
            {
                audio.clip = leaveNoise;
                audio.Play();
            }
        }
    }
}
