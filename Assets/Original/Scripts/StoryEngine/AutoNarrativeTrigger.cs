using UnityEngine;


public class AutoNarrativeTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player")
            StoryManager.instance.EnterAutoDialogue(inkJSON);
    }
}