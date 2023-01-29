using System;
using UnityEngine;


public class NoteTrigger : MonoBehaviour
{
    [Header("Visual Cues")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private Material OpenedIndicatorMat;
    [SerializeField] private GameObject nearbyIndicator;

    [Header("Note Text")]
    [TextArea]
    [SerializeField] private string noteText;

    private bool playerInRange;
    private bool hasOpened;
    private AudioSource paperSFX;

    private void Awake()
    {
        nearbyIndicator.SetActive(false);
        paperSFX = GetComponent<AudioSource>();
        hasOpened = GetOpened();

        if (hasOpened)
            SetOpened();
    }

    void Update()
    {
        if (playerInRange)
        {
            nearbyIndicator.SetActive(true);
            if (!StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E))
            {
                paperSFX.Play();
                StoryManager.instance.EnterNote(noteText);
                SetOpened();
            }
            else if (StoryManager.instance.isNoteOpen && Input.GetKeyDown(KeyCode.E))
            {
                StoryManager.instance.ExitNote();
            }
        }
        else
        {
            nearbyIndicator.SetActive(false);
            if(StoryManager.instance.isNoteOpen)
                StoryManager.instance.ExitNote();
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
        Material[] mats = visualCue.GetComponent<MeshRenderer>().materials;
        mats[1] = OpenedIndicatorMat;
        visualCue.GetComponent<MeshRenderer>().materials = mats;
    }

    private bool GetOpened()
    {
        //TODO Get if opened on different runs
        return false;
    }
}