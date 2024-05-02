using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestHandler : MonoBehaviour
{

    [SerializeField] List<GameObject> questNPCs;
    private int currentNPCIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

        // Disable all NPC scripts except the first one
        for (int i=1; i < questNPCs.Count; i++)
        {
            questNPCs[i].GetComponent<QuestNPCScript>().enabled = false;
            questNPCs[i].transform.Find("QuestMarker").gameObject.SetActive(false);
        }

    }

    public void GoToNextQuestNPC()
    {
        // Disable the current NPC script and its marker
        questNPCs[currentNPCIndex].GetComponent<QuestNPCScript>().enabled = false;
        questNPCs[currentNPCIndex].transform.Find("QuestMarker").gameObject.SetActive(false);

        // Move to the next NPC
        currentNPCIndex++;

        // Check if index is within bounds
        if (currentNPCIndex < questNPCs.Count)
        {
            // Enable the next NPC script and its marker
            questNPCs[currentNPCIndex].GetComponent<QuestNPCScript>().enabled = true;
            questNPCs[currentNPCIndex].transform.Find("QuestMarker").gameObject.SetActive(true);
        }
        else
        {
            //Debug.LogWarning("No more NPCs left in the list.");
            GameObject.Find("InstructionsUI").GetComponent<InstructionsUI>().showBossInstruction = true;
        }
    }

    public int GetCurrentNPCIndex()
    {
        return currentNPCIndex;
    }

    public QuestNPCScript GetCurrentEnabled()
    {
        return questNPCs[currentNPCIndex].GetComponent<QuestNPCScript>();
    }
}
