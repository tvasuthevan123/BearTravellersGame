using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    [SerializeField] private float damage = 100f;

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.GetComponent<Target>() != null)
            collider.gameObject.GetComponent<Target>().TakeDmg(damage);
    }
}
