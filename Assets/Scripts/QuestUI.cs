using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class QuestUI : MonoBehaviour
{

    public Canvas questCanvas;
    public GameObject questPanel;
    [SerializeField] float delayBetweenCharacters = 0.01f; // Delay between each character
    private Quest npcQuest;
    
    public TextMeshProUGUI title;
    public TextMeshProUGUI description; 
    private int pages;

    private string fullText; // Full text to display
    private string currentText = ""; // Current text being displayed
    private bool isTyping = false; // Flag to track if typing animation is in progress
    private int characterIndex = 0;
    private GameObject backButton;
    private QuestNPCScript questNPC;

    public bool showQuestUI = false;


    private GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public bool startQuiz = false;
    public Toggle[] answerToggles;
    private Toggle selectedAnswer;
    private Toggle previousToggle;
    public TextMeshProUGUI tryAgainText;
    private int questionIndex;
    private QuestManager questManager;
    public bool isCorrectAnswer = false;


    // Start is called before the first frame update
    void Start()
    {

        questCanvas.enabled = false;
        quizPanel = GameObject.Find("PanelQuiz").gameObject;
        quizPanel.SetActive(false);

        // Start the typewriter effect
        StartTypewriter();

        backButton = questCanvas.transform.Find("QuestPanel").gameObject.transform.Find("Button Back").gameObject;
        backButton.SetActive(false);

        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showQuestUI)
        {
            HandleTypewriterProgression();
            questCanvas.enabled = true;
            CheckBackButton();
            if (startQuiz)
            {
                quizPanel.SetActive(true);
                questPanel.SetActive(false);
                
                OnToggleClick();
            } else
            {
                questPanel.SetActive(true);
                quizPanel.SetActive(false);
            }
        } else 
        {
            questCanvas.enabled = false;
        }
    }

    private void AssignToggles()
    {
        GameObject toggles = questCanvas.transform.Find("PanelQuiz").transform.Find("Toggles").gameObject;

        // Initialize the answerToggles array with the correct size
        answerToggles = new Toggle[toggles.transform.childCount];

        for (int i = 0; i < answerToggles.Length; i++)
        {
            // Find each Toggle GameObject within the parent GameObject
            Transform toggleTransform = toggles.transform.GetChild(i);

            if (toggleTransform != null)
            {
                // Get the Toggle component from the GameObject
                Toggle toggle = toggleTransform.GetComponent<Toggle>();

                if (toggle != null)
                {
                    // Assign the Toggle component to the answerToggles array
                    answerToggles[i] = toggle;
                }
            }
        }
    }


    private void HandleTypewriterProgression()
    {

        if (questCanvas.enabled)
        {
            // Check if there are more characters to reveal and if typing animation is not in progress
            if (characterIndex < fullText.Length && !isTyping)
            {
                // Start typing the next character
                Invoke(nameof(TypeNextCharacter), delayBetweenCharacters);
                isTyping = true;
            }
        }
        else if (!questCanvas.enabled)
        {
            StartTypewriter();
        }
    }


    void TypeNextCharacter()
    {

        // Check if all characters have been revealed
        if (characterIndex >= fullText.Length)
        {
            // Typing animation complete
            isTyping = false;
            return;
        }
        // Add the next character to the current text being displayed
        currentText += fullText[characterIndex];
        characterIndex++;

        // Update the text displayed in the TextMeshPro component
        description.text = currentText;

        // Continue typing animation
        Invoke(nameof(TypeNextCharacter), delayBetweenCharacters);
    }

    private void StartTypewriter()
    {
        // Reset variables and start typing animation
        characterIndex = 0;
        currentText = "";
        isTyping = false;
    }

    public void ShowNextPage()
    {
        description.pageToDisplay += 1;
        if (description.pageToDisplay > pages)
        {
            questPanel.SetActive(false);
            startQuiz = true;
            quizPanel.SetActive(true);
        }
    }

    public void ShowPreviousPage()
    {
        description.pageToDisplay -= 1;
    }

    public void CheckBackButton()
    {

        if (description.pageToDisplay == 1)
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
    }

    public void EscapeButton()
    {
        showQuestUI = false;
    }

    public void SetQuizActive()
    {
        startQuiz = true;
        quizPanel.SetActive(true);
    }

    public void DeactivateQuiz()
    {
        startQuiz = false;
        quizPanel.SetActive(false);
        
    }

    private void OnToggleClick()
    {
        if (answerToggles != null)
        {
            // Iterate through each toggle in the array
            foreach (Toggle toggle in answerToggles)
            {
                // Check if the toggle is on
                if (toggle != null && toggle.isOn)
                {
                    if (previousToggle != toggle && previousToggle != null)
                    {
                        previousToggle.SetIsOnWithoutNotify(false);
                    }
                    selectedAnswer = toggle;
                    previousToggle = toggle;
                }
            }
        }
    }

    public void OnSubmit()
    {
        QuestNPCScript npc = GameObject.Find("DemonGirls").GetComponent<NPCQuestHandler>().GetCurrentEnabled();
        isCorrectAnswer = questManager.CheckQuizAnswer(npcQuest, npc.questionIndex, selectedAnswer);

        if (!isCorrectAnswer)
        {
            tryAgainText.SetText("Wrong Answer. Try again...");
        } else
        {
            tryAgainText.SetText("");
        }

    }


    public void SetQuizDetails(string question, string answer1, string answer2, string answer3, string answer4)
    {
        AssignToggles();
        // Set the question text
        questionText.SetText(question);
        // Set the text for each answer choice
        answerToggles[0].transform.Find("Label").GetComponent<Text>().text = answer1;
        answerToggles[1].transform.Find("Label").GetComponent<Text>().text = answer2;
        answerToggles[2].transform.Find("Label").GetComponent<Text>().text = answer3;
        answerToggles[3].transform.Find("Label").GetComponent<Text>().text = answer4;
        tryAgainText.SetText("");
    }

    public void SetQuestDetails(Quest quest)
    {
        npcQuest = quest;
        title.SetText(quest.Title);
        description.SetText(quest.Description);
        pages = quest.Pages;
        fullText = quest.Description;

        description.text = "";
    }

}
