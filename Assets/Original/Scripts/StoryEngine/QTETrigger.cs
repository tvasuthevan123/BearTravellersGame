using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class QTETrigger : MonoBehaviour
{
    public GameObject qteObject;
    public static bool qteComplete = false;

    [SerializeField] private Vector3 dialoguePosition;
    [SerializeField] Transform playerCamera;
    [SerializeField] GameObject player;
    [SerializeField] private GameObject fence1, fence2, fence3, dialogue;

    private void Start()
    {
        qteComplete = false;
    }

    void OnTriggerEnter()
    {
        player.GetComponent<FirstPersonController>().disableInput = true;
        if (dialoguePosition != Vector3.zero)
        {
            player.transform.position = dialoguePosition;
            playerCamera.rotation = Quaternion.Euler(5.6f, 128.5f, -0.029f);
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
            player.GetComponent<FirstPersonController>().disableInput = false;
            player.GetComponent<PlayerInput>().ActivateInput();
            player.GetComponent<HealthItems>().enabled = true;
            player.GetComponent<PlayerPickup>().enabled = true;
            qteObject.SetActive(false);
            fence1.SetActive(false);
            fence2.SetActive(false);
            fence3.SetActive(false);
            dialogue.SetActive(false);
        }
    }
}
