using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float turnSpeed = 0.2f;
    [SerializeField] public bool moving {get; set;}
    [SerializeField] private Transform player;

    

    void Update(){
        followPlayer();
    }
    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.GetComponent<Target>() != null)
            collider.gameObject.GetComponent<Target>().TakeDmg(damage);
    }

    void followPlayer(){
        Vector3 distanceVector = player.position - transform.position;
        Vector3 direction = distanceVector.normalized;
        if(distanceVector.magnitude > 10){
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
        else{
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
        
    }
}
