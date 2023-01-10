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
    [SerializeField] private Transform player;

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

    private const string SPEAKER_TAG = "speaker";
    private const string CUTSCENE_TAG = "Cutscene";
    private const string AUDIO_TAG = "audio";

    private GameObject trigger;
    private MusicManager musicManager;

    public GameObject zombieGroupHardOne;
    public int zombieGroupHardOneCounter = 6;

    public GameObject zombieGroupEasyOne;
    public TimelineDirector director;
    public int zombieGroupEasyOneCounter = 6;

    public GameObject zombieGroupFriend;
    public int zombieGroupFriendCounter = 3;

    

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of StoryManager");
        }
        instance = this;

        storyVariables = new StoryVariables(globalStateJSON, player);
    }

    void Start()
    {
        isDialoguePlaying = false;
        isNoteOpen = false;
        notePanel.GetComponent<Animator>().SetBool("isNoteOpen", false);
        storyPanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];

        musicManager = MusicManager.Instance;
        musicManager.PlaySpawnMusic();

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

    public void EnterDialogue(TextAsset inkJSON, GameObject trigger)
    {
        this.trigger = trigger;
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
            HandleTags(currentStorySegment.currentTags);
            // handle tags
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
        }
        else
        {
            StartCoroutine(ExitStorySegment());
        }
    }

    private void HandleTags(List<string> tags){
        foreach(string tag in tags){
            string[] splitTags = tag.Split(':');
            string key = splitTags[0].Trim();
            string value = splitTags[1].Trim();

            switch(key){
                case SPEAKER_TAG:
                    switch(value){
                        case "Friend":
                            storyText.color = Color.green;
                            break;
                        case "Player":
                            storyText.color = Color.white;
                            break;
                        case "Darkness":
                            storyText.color = new Color(125/255f, 2/255f, 232/255f);
                            break;
                        default:
                            storyText.color = Color.black;
                            break;
                    }
                    break;
                case CUTSCENE_TAG:
                    director.Play(int.Parse(value), true);
                    break;
                case "disable":
                    trigger.SetActive(false);
                    break;
                case AUDIO_TAG:
                    break;
                default:
                    Debug.LogWarning("Unrecognised tag");
                    break;
            }
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
            Debug.Log("Waiting for " + (float)((line.Length / 25) + 2) + " seconds");
            yield return new WaitForSeconds((float)((line.Length / 25) + 2));
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

    public void SetVariable(string variableName, string str)
    {
        Ink.Runtime.StringValue inkString = new Ink.Runtime.StringValue(str);
        storyVariables.variables[variableName] = inkString;
    }

    public void SetVariable(string variableName, int num)
    {
        Ink.Runtime.IntValue inkNum = new Ink.Runtime.IntValue(num);
        storyVariables.variables[variableName] = inkNum;
    }

    public void SetVariable(string variableName, bool boolean)
    {
        Ink.Runtime.BoolValue inkBool = new Ink.Runtime.BoolValue(boolean);
        storyVariables.variables[variableName] = inkBool;
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