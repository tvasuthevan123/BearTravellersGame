using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{
    public RawImage displayMouse;
    public Image timerFill;
    public Image timeBackground;
    public TextMeshProUGUI passBox; //TODO: only used for testing, can be removed once friend convo complete
    public bool qteActive = true;
    public bool waitingForKey = true;
    public int correctKey; //TODO: believe this can be removed
    public bool countingDown = false;

    [SerializeField] private AudioClip friendHurt;
    public GameObject friendLocation;

    public float duration;

    private float remainingDuration = 0;
    [SerializeField] private TextAsset passDialogue, failDialogue;
    [SerializeField] private GameObject player, passAnim;

    private void Update()
    {
        if (waitingForKey)
        {
            waitingForKey = false;
            passBox.text = "Voice: Do it, shoot him now!";
            displayMouse.enabled = true;
            timerFill.enabled = true;
            timeBackground.enabled = true;
            countingDown = true;
            StartCoroutine(CountDown());
        }
        if (qteActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                qteActive = false;
                correctKey = 1;
                AudioSource.PlayClipAtPoint(friendHurt, friendLocation.transform.position);
                StartCoroutine(KeyPressing());
            }
        }
        
    }

    IEnumerator KeyPressing()
    {
        if (correctKey == 1)
        {
            countingDown = false;
            displayMouse.enabled = false;
            timerFill.enabled = false;
            timeBackground.enabled = false;
            passBox.text = "Voice: Good job";
            passAnim.SetActive(true);
            StoryManager.instance.EnterDialogue(passDialogue, null);
            //TODO: Trigger bad friend conversation
            yield return new WaitForSeconds(1.5f);
            QTETrigger.qteComplete = true;
            correctKey = 0;
            passBox.text = "";
        }
    }

    IEnumerator CountDown()
    {
        while (remainingDuration <= 1f && QTETrigger.qteComplete == false)
        {
            timerFill.fillAmount = 1 - remainingDuration;
            remainingDuration += Time.deltaTime / duration;
            yield return null;
        }

        if (countingDown == true)
        {
            countingDown = false;
            qteActive = false;
            displayMouse.enabled = false;
            timerFill.enabled = false;
            timeBackground.enabled = false;
            passBox.text = "Voice: You didn't listen!";
            StoryManager.instance.EnterDialogue(failDialogue, null);
            //TODO: Trigger good friend conversation
            yield return new WaitForSeconds(1.5f);
            QTETrigger.qteComplete = true;
            correctKey = 0;
            passBox.text = "";
        }        
    }
}
