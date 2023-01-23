using UnityEngine;

public class Gun : MonoBehaviour
{
    public float dmg = 10f;
    public float range = 100f;
    public float impactForce = 5f;

    public new Camera camera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public Ammo gunAmmo;

    [SerializeField] private AudioClip gunShot;

    void Update() {
        if(!StoryManager.instance.isDialoguePlaying && !StoryManager.instance.isNoteOpen && Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        muzzleFlash.Play();
        gunAmmo.ammoUsage();
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = gunShot;
        audio.Play();
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            if(target != null){
                target.TakeDmg(dmg);
            }

            if(hit.rigidbody != null){
                hit.rigidbody.AddForce(- hit.normal * impactForce);
            }

            // TODO : Add impact effect
            // GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            // Destroy(impact, 2f);
        }
    }


}
