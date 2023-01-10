using UnityEngine;


public class NoteTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Note Text")]
    [TextArea]
    [SerializeField] private string noteText;

    private bool playerInRange;

    private void Awake(){
        visualCue.SetActive(false);
    }

    void Update(){
        if(playerInRange){
            visualCue.SetActive(true);
            if(!StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E)){
                StoryManager.instance.EnterNote(noteText);
            }
            else if(StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E)){
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