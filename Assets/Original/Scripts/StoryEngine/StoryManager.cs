using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;


public class StoryManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Variables File")]
    [SerializeField] private TextAsset globalStateJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private GameObject continueIcon;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Note UI")]
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TextMeshProUGUI noteText;

    private TextMeshProUGUI[] choicesText;

    private Story currentStorySegment;
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;
    private bool isAuto = false;
    private StoryVariables storyVariables;

    public bool isDialoguePlaying { get; private set; }
    public bool isNoteOpen { get; private set; }
    public static StoryManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of StoryManager");
        }
        instance = this;

        storyVariables = new StoryVariables(globalStateJSON);
    }

    void Start()
    {
        isDialoguePlaying = false;
        isNoteOpen = false;
        notePanel.GetComponent<Animator>().SetBool("isNoteOpen", false);
        storyPanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];

        for (int i = 0; i < choices.Length; i++)
        {
            choicesText[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (!isDialoguePlaying && !isAuto && !isNoteOpen)
            return;

        if (currentStorySegment!=null && currentStorySegment.currentChoices.Count == 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            ContinueStorySegment(isAuto);
        }
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        isAuto = false;
        currentStorySegment = new Story(inkJSON.text);
        isDialoguePlaying = true;
        storyVariables.StartListening(currentStorySegment);
        storyPanel.SetActive(true);

        ContinueStorySegment(isAuto);
    }

    public void EnterAutoDialogue(TextAsset inkJSON)
    {
        isAuto = true;

        currentStorySegment = new Story(inkJSON.text);
        storyPanel.SetActive(true);

        ContinueStorySegment(isAuto);
    }


    private IEnumerator ExitStorySegment()
    {
        yield return new WaitForSeconds(0.2f);

        storyVariables.StopListening(currentStorySegment);
        isDialoguePlaying = false;
        storyPanel.SetActive(false);
        storyText.text = "";
    }

    private void ContinueStorySegment(bool isAuto)
    {
        if (currentStorySegment.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStorySegment.Continue();
            // handle tags
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        }
        else
        {
            StartCoroutine(ExitStorySegment());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        storyText.text = line;
        storyText.maxVisibleCharacters = 0;

        HideChoices();

        canContinueToNextLine = false;

        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                storyText.maxVisibleCharacters = line.Length;
                break;
            }

            storyText.maxVisibleCharacters++;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (isAuto)
        {
            Debug.Log("Waiting for " + (float)((line.Length / 25 * 2) + 2) + " seconds");
            yield return new WaitForSeconds((float)((line.Length / 25 * 2) + 2));
            ContinueStorySegment(isAuto);
        }

        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }


    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStorySegment.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices");
        }

        for (int i = 0; i < currentChoices.Count; i++)
        {
            choices[i].gameObject.SetActive(true);
            choicesText[i].text = currentChoices[i].text;
        }

        for (int i = currentChoices.Count; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStorySegment.ChooseChoiceIndex(choiceIndex);
            ContinueStorySegment(isAuto);
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        storyVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
            Debug.LogWarning("Variable " + name + " doesn't exist");
        return variableValue;
    }

    public void EnterNote(string noteText){
        Debug.Log("Note opened");
        isNoteOpen = true;
        notePanel.GetComponent<Animator>().SetBool("isNoteOpen", true);
        this.noteText.text = noteText; 
    }

    public void ExitNote(){
        Debug.Log("Note closed");
        isNoteOpen = false;
        notePanel.GetComponent<Animator>().SetBool("isNoteOpen", false);
    }

}