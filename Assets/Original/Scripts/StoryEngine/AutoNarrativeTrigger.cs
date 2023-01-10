using UnityEngine;


public class AutoNarrativeTrigger : MonoBehaviour
{
    [SerializeField] private bool hasTriggered;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void OnTriggerEnter(Collider collider){
        if(!hasTriggered && collider.gameObject.tag == "Player"){
            StoryManager.instance.EnterAutoDialogue(inkJSON);
            hasTriggered = true;
        }
    }
}