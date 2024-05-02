using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    private List<Quest> quests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
    }

    public Quest GetQuestByLevel(int requiredlevel)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (requiredlevel == quests[i].LevelQuest)
            {
                return quests[i];
            }
        }
        return null;
    }

    public bool CheckQuizAnswer(Quest quest, int questionIndex, Toggle submittedAnswer)
    {
        if ("Toggle" + quest.Quizzes[questionIndex].correctAnswer == submittedAnswer.name)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
