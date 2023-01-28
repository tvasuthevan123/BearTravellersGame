using UnityEngine;


public class NoteTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Note Text")]
    [TextArea]
    [SerializeField] private string noteText;

    private bool playerInRange;
    private AudioSource paperSFX;

    private void Awake(){
        visualCue.SetActive(false);
        paperSFX = GetComponent<AudioSource>();
    }

    void Update(){
        if(playerInRange){
            visualCue.SetActive(true);
            if(!StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E)){
                Debug.Log("Note Triggered");
                paperSFX.Play();
                StoryManager.instance.EnterNote(noteText);
            }
            else if(StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E)){
                Debug.Log("Note Closed");
                StoryManager.instance.ExitNote();
            }
        }
        else
            visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player")
            playerInRange=true;
    }

    private void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "Player")
            playerInRange=false;
    }
}