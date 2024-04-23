using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestUI : MonoBehaviour
{

    public Canvas questCanvas;
    private InstructionsUI instructionsUI;
    private bool isPlayerInsideTrigger = false;

    [SerializeField] float delayBetweenCharacters = 0.01f; // Delay between each character
    public TextMeshProUGUI textMeshProText; // Reference to the TextMeshPro component
    private string fullText; // Full text to display
    private string currentText = ""; // Current text being displayed
    private bool isTyping = false; // Flag to track if typing animation is in progress
    private int characterIndex = 0;
    public GameObject textArea;
    private GameObject backButton;
    private int currentPage = 0;

    // Start is called before the first frame update
    void Start()
    {
        instructionsUI = GameObject.FindWithTag("UI").GetComponent<InstructionsUI>();
        questCanvas.enabled = false;

        // Get the full text from the TextMeshPro component
        fullText = textMeshProText.text;

        // Clear the text displayed in the TextMeshPro component
        textMeshProText.text = "";

        // Start the typewriter effect
        StartTypewriter();

        backButton = questCanvas.transform.Find("Panel").gameObject.transform.Find("Button Back").gameObject;
        backButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isPlayerInsideTrigger)
        {
            questCanvas.enabled = true;

        }
        CheckEnterWhileCanvasEnabled();
        CheckBackButton();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isPlayerInsideTrigger = true;
            instructionsUI.showEnter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            instructionsUI.showEnter = false;
            // Hide the quest canvas
            questCanvas.enabled = false;
            isPlayerInsideTrigger = false;
        }
    }

    private void CheckEnterWhileCanvasEnabled()
    {
        if (questCanvas.enabled)
        {
            instructionsUI.showEnter = false;
            // Check if there are more characters to reveal and if typing animation is not in progress
            if (characterIndex < fullText.Length && !isTyping)
            {
                // Start typing the next character
                Invoke("TypeNextCharacter", delayBetweenCharacters);
                isTyping = true;
            }
        }
        else if (isPlayerInsideTrigger && !questCanvas.enabled)
        {
            instructionsUI.showEnter = true;
            StartTypewriter();
        }
    }


    void TypeNextCharacter()
    {
        // Add the next character to the current text being displayed
        currentText += fullText[characterIndex];
        characterIndex++;

        // Update the text displayed in the TextMeshPro component
        textMeshProText.text = currentText;

        // Check if all characters have been revealed
        if (characterIndex >= fullText.Length)
        {
            // Typing animation complete
            isTyping = false;
        }
        else
        {
            // Continue typing animation
            Invoke(nameof(TypeNextCharacter), delayBetweenCharacters);
        }
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
        textMeshProText.pageToDisplay += 1;
        if (textMeshProText.pageToDisplay > 6)
        {
            questCanvas.enabled = false;
        }
    }

    public void ShowPreviousPage()
    {
        textMeshProText.pageToDisplay -= 1;
    }

    public void CheckBackButton()
    {

        if (textMeshProText.pageToDisplay == 1)
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
    }

}
