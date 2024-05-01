using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InstructionsUI : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    public Quest quest;
    public bool showEnter = false, showQuestInstruction = false, showTutorial = true, showBossInstruction = false;
    private bool movedTutorial = false, jumpedTutorial = false, hitTutorial = false;

    private Vector2 questInstructionScale = new Vector2(2f, 2f);
    private Vector2 enterInstructionScale = new Vector2(1f, 1f);

    // Update is called once per frame
    void Update()
    {
        TutorialInstruction();
        EnterInstruction();
        QuestInstruction();
        BossInstruction();
        ReviveInstruction();
    }

    private void EnterInstruction()
    {
        if (showEnter && !quest.IsCompleted)
        {
            instructionsText.rectTransform.localScale = enterInstructionScale;
            instructionsText.fontSize = 36;
            instructionsText.SetText("Press Enter");
            showQuestInstruction = false;
        }

    }

    private void QuestInstruction()
    {
        if (showQuestInstruction && !quest.IsCompleted)
        {
            instructionsText.fontSize = 20;
            instructionsText.rectTransform.localScale = questInstructionScale;
            instructionsText.SetText(quest.Instructions);
        }
    }

    private void BossInstruction()
    {
        if (showBossInstruction)
        {
            instructionsText.SetText("Find and Kill the Boss West to the Village");
        }
        else if (GameObject.FindWithTag("Enemy").GetComponent<Enemy>().isDead)
        {
            showBossInstruction = false;
            instructionsText.SetText("Congratulations! You completed the demo. Stay tuned for more.");
        }
    }

    private void ReviveInstruction()
    {
        if (GameObject.FindWithTag("Character").GetComponent<Character>().isDead)
        {
            instructionsText.SetText("Press 'R' to revive");
        }
    }

    private void TutorialInstruction()
    {
        if (showTutorial)
        {
            instructionsText.fontSize = 20;
            instructionsText.rectTransform.localScale = questInstructionScale;

            // Move Tutorial
            if (!movedTutorial)
            {
                instructionsText.SetText("Press 'WASD' to move");
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                movedTutorial = true;
            }

            // Jump Tutorial
            if (!jumpedTutorial && movedTutorial)
            {
                instructionsText.SetText("Press 'Space' to Jump");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpedTutorial = true;
                }

            }


            // Hit Tutorial
            if (!hitTutorial && jumpedTutorial && movedTutorial)
            {
                instructionsText.SetText("Press '1' Numeric Key to Hit");
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    hitTutorial = true;
                    showQuestInstruction = true;
                }
            }


            if (hitTutorial && jumpedTutorial && movedTutorial)
            {
                // End tutorial
                showTutorial = false;
            }
        }

    }


    public void SetQuest(Quest inputQuest)
    {
        quest = inputQuest;
    }
}
