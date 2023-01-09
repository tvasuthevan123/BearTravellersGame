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
                Destroy(hit.transform.gameObject);
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            healthitems.medkitUsage();
        }
    }
}
