using System.Collections;
using UnityEngine;
using TMPro;

public class AppearingText : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private Transform playerCam;
    private TextMeshPro textElement;
    private bool playerInRange;
    private bool setTransform = false;

    void Awake()
    {
        textElement = gameObject.GetComponentInChildren<TextMeshPro>();
        textElement.alpha = 0;
    }
    void Update()
    {

        if (playerInRange)
            StartCoroutine(fadeIn());
        else
            StartCoroutine(fadeOut());
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

    public IEnumerator fadeIn()
    {
        while (textElement.alpha < 1.0f)
        {
            textElement.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public IEnumerator fadeOut()
    {
        while (textElement.alpha > 0.0f)
        {
            textElement.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
