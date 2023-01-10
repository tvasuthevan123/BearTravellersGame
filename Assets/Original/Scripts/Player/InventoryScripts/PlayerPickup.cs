using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private new Transform camera;
    [SerializeField] private HealthItems healthitems;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject fence;
    [SerializeField] private GameObject button1a; //left path
    [SerializeField] private GameObject button1b; //right path
    [SerializeField] private GameObject rock1a; //left path
    [SerializeField] private GameObject rock1b; //right path
    private bool path1choice = false;

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
            if(Physics.Raycast(camera.position, camera.forward, out hit, pickupRange, pickupLayer)){
                Debug.Log("Hit: " + hit.transform.name);
                if (hit.transform.CompareTag("gunpickup")) 
                {
                    gun.SetActive(true);
                    fence.SetActive(false);
                }
                if (hit.transform.CompareTag("medkit"))
                {
                    healthitems.addMedkit();
                }
                if (hit.transform.CompareTag("button1a") && path1choice == false)
                {
                    rock1a.SetActive(false); //TODO: ADD SOUND?
                    button1b.SetActive(false);
                    path1choice = true;
                }
                if (hit.transform.CompareTag("button1b") && path1choice == false)
                {
                    rock1b.SetActive(false); //TODO: ADD SOUND?
                    button1a.SetActive(false); // opposite button removed
                    path1choice = true;
                }
                Destroy(hit.transform.gameObject);
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            healthitems.medkitUsage();
        }
    }
}
