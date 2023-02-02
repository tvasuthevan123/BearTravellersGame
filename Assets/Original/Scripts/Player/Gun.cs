using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public float dmg = 10f;
    public float range = 100f;
    public float impactForce = 5f;

    public new Camera camera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Image hitmarker;

    public Ammo gunAmmo;

    [SerializeField] private AudioClip gunShot;
    public AudioSource gunSource;
    [SerializeField] private AudioClip hitmarkerSound;
    public AudioSource hitSource;

    void Update() {
        if(!StoryManager.instance.isDialoguePlaying && !StoryManager.instance.isNoteOpen && Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        muzzleFlash.Play();
        gunSource.clip = gunShot;
        gunSource.Play();
        gunAmmo.ammoUsage();
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            if(target != null){
                target.TakeDmg(dmg);
                hitSource.clip = hitmarkerSound;
                hitSource.Play();
                StopAllCoroutines();
                StartCoroutine(showHit());
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(- hit.normal * impactForce);
            }

            // TODO : Add impact effect
            // GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // Destroy(impact, 2f);
        }
    }

    IEnumerator showHit()
    {
        hitmarker.enabled = true;
        yield return new WaitForSeconds(0.5f);
        hitmarker.enabled = false;
    }
}
