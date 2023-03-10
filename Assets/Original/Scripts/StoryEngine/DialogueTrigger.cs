using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private Transform player, playerCamera;

    [SerializeField] private Vector3 dialogueLook, dialoguePosition;

    private bool playerInRange;
    private bool eventTriggered = false;

    private void Awake()
    {
        visualCue.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !StoryManager.instance.isDialoguePlaying)
        {
            if (visualCue)
                visualCue.SetActive(true);
            if (!eventTriggered && Input.GetKeyDown(KeyCode.E))
            {
                if (dialogueLook != Vector3.zero && dialoguePosition != Vector3.zero)
                {
                    player.position = dialoguePosition;
                    playerCamera.LookAt(dialogueLook);
                }
                StoryManager.instance.EnterDialogue(inkJSON, this.gameObject);
            }
        }
        else if (visualCue)
            visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            playerInRange = false;
    }
}