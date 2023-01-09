using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private new Transform camera;
    [SerializeField] private Inventory inventory;

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
            if(Physics.Raycast(camera.position, camera.forward, out hit, pickupRange, pickupLayer)){
                Debug.Log("Hit: " + hit.transform.name);
                Item newItem = hit.transform.GetComponent<ItemObject>().pickupItem;
                inventory.AddItem(newItem);
                if(newItem.dialogue)
                    StoryManager.instance.EnterAutoDialogue(newItem.dialogue);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
