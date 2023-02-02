using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTETrigger : MonoBehaviour
{
    public GameObject qteObject;
    public static bool qteComplete = false;

    [SerializeField] private Vector3 dialogueLook, dialoguePosition;
    [SerializeField] Transform playerCamera;
    [SerializeField] GameObject player;
    [SerializeField] private GameObject fence1, fence2, fence3;

    void OnTriggerEnter()
    {
        if (dialogueLook != Vector3.zero && dialoguePosition != Vector3.zero)
        {
            player.transform.position = dialoguePosition;
            playerCamera.LookAt(dialogueLook);
        }
        player.GetComponent<PlayerInput>().DeactivateInput();
        player.GetComponent<HealthItems>().enabled = false;
        player.GetComponent<PlayerPickup>().enabled = false;
        qteObject.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (qteComplete == true)
        {
            player.GetComponent<PlayerInput>().ActivateInput();
            player.GetComponent<HealthItems>().enabled = true;
            player.GetComponent<PlayerPickup>().enabled = true;
            qteObject.SetActive(false);
            fence1.gameObject.SetActive(false);
            fence2.gameObject.SetActive(false);
            fence3.gameObject.SetActive(false);
        }
    }
}
