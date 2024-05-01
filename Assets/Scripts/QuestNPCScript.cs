using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestNPCScript : MonoBehaviour
{
    [SerializeField] QuestManager questManager;
    [SerializeField] int questLevel;
    private NPCQuestHandler npcQuestHandler;

    private Quest quest;

    private InstructionsUI instructionsUI;
    private QuestUI questUI;

    private bool isPlayerInsideTrigger = false, questReceived = false, detailSet = false, quizDetailSet = false;
    private int questionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        questUI = GameObject.Find("QuestUI").GetComponent<QuestUI>();

        instructionsUI = GameObject.Find("InstructionsUI").GetComponent<InstructionsUI>();

        npcQuestHandler = GameObject.Find("DemonGirls").GetComponent<NPCQuestHandler>();
    } 

    void Update()
    {
        if (!questReceived)
        {
            quest = questManager.GetQuestByLevel(questLevel);
            questReceived = true;
        }

        if (questLevel == GameObject.FindWithTag("Character").GetComponent<Character>().level && questReceived)
        {
            if (!detailSet)
            {
                questUI.SetQuestDetails(quest);
                instructionsUI.SetQuest(quest);
                detailSet = true;
            }

            CheckEnterWhileCanvasEnabled();
            ShowQuestUI();
            ShowQuiz();
        }

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
            questUI.showQuestUI = false;
            isPlayerInsideTrigger = false;
            instructionsUI.showQuestInstruction = true;
        }
        
    }

    private void CheckEnterWhileCanvasEnabled()
    {
        if (questUI.questCanvas.enabled)
        {
            instructionsUI.instructionsText.SetText("");
            instructionsUI.showEnter = false;
            instructionsUI.showQuestInstruction = false;
        }
        else if (isPlayerInsideTrigger && !questUI.questCanvas.enabled)
        {
            instructionsUI.showEnter = true;
        }
    }

    private void ShowQuestUI()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isPlayerInsideTrigger)
        {
            questUI.showQuestUI = true;
            questUI.description.pageToDisplay = 1;
        }
    }

    private void ShowQuiz()
    {
        if (quest != null)
        {
            int numOfQuestions = quest.Quizzes.Count;
            if (questUI.startQuiz && questionIndex < numOfQuestions)
            {
                questUI.SetQuizActive();
                if (!quizDetailSet)
                {
                    questUI.SetQuizDetails(quest.Quizzes[questionIndex].question, quest.Quizzes[questionIndex].answer1,
                        quest.Quizzes[questionIndex].answer2, quest.Quizzes[questionIndex].answer3, quest.Quizzes[questionIndex].answer4);
                    quizDetailSet = true;
                }
                if (questUI.isCorrectAnswer)
                {
                    questionIndex++;
                    questUI.isCorrectAnswer = false;
                    quizDetailSet = false;
                }
            }
            else if (questUI.startQuiz && questionIndex >= numOfQuestions)
            {
                questUI.startQuiz = false;
                questUI.showQuestUI = false;
                quest.IsCompleted = true;
                questUI.isCorrectAnswer = false;
                quizDetailSet = false;
                npcQuestHandler.GoToNextQuestNPC();
                GameObject.FindWithTag("Character").GetComponent<Character>().level++;
            }
        }
    }


}
