using System;
using System.Collections;
using UnityEngine;


public class NoteTrigger : MonoBehaviour
{
    [Header("Visual Cues")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private Material OpenedIndicatorMat;
    [SerializeField] private NoteIndicator nearbyCollider;

    [Header("Note Text")]
    [TextArea]
    [SerializeField] private string noteText;

    [Header("Misc")]
    [SerializeField] private Material[] lightColors;

    private bool playerInRange;
    private bool hasOpened;
    private AudioSource paperSFX;
    private bool flickerOn = false;


    private void Awake()
    {
        paperSFX = GetComponent<AudioSource>();
        hasOpened = GetOpened();

        if (hasOpened)
            SetOpened();
    }

    private void Start(){
        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop(){
        while(true){
            yield return new WaitForSeconds(0.5f);
            flickerOn = !flickerOn;
        }
    }

    void Update()
    {
        if(!hasOpened){
            if(flickerOn){
                SetLightColour(lightColors[0]);
            }
            else{
                SetLightColour(lightColors[1]);
            }
        }
        if (playerInRange)
        {
            if (!StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Trigger Click");
                paperSFX.Play();
                StoryManager.instance.EnterNote(noteText);
                SetOpened();
            }
            else if (StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Trigger Closed");
                StoryManager.instance.ExitNote();
            }
        }
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

    private void SetOpened()
    {
        hasOpened = true;
        StopCoroutine("FlickerLoop");
        nearbyCollider.nearbyIndicator = null;
        SetLightColour(OpenedIndicatorMat);
    }

    private void SetLightColour(Material lightMat){
        Material[] mats = visualCue.GetComponent<MeshRenderer>().materials;
        mats[1] = lightMat;
        visualCue.GetComponent<MeshRenderer>().materials = mats;
    }

    private bool GetOpened()
    {
        //TODO Get if opened on different runs
        return false;
    }
}