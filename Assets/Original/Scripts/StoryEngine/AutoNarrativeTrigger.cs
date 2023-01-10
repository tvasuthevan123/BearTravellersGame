using UnityEngine;


public class AutoNarrativeTrigger : MonoBehaviour
{
    [SerializeField] private bool hasTriggered;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private bool doesStop;

    private void OnTriggerEnter(Collider collider){
        if(!hasTriggered && collider.gameObject.tag == "Player"){
            if(doesStop)
                StoryManager.instance.EnterDialogue(inkJSON, this.gameObject);
            else
                StoryManager.instance.EnterAutoDialogue(inkJSON);
            hasTriggered = true;
        }
    }
}