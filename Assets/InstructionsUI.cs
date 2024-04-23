using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionsUI : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    public bool showEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        instructionsText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnterInstruction();
    }

    private void EnterInstruction()
    {
        if (showEnter)
        {
            instructionsText.enabled = true;
        }
        else
        {
            instructionsText.enabled = false;
        }
    }


}
