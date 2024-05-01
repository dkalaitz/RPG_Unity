using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContinueTextScript : MonoBehaviour
{

    private TMP_Text textContinue;
    [SerializeField] TMP_Text textIntroduction;
    // Start is called before the first frame update
    void Start()
    {
        textContinue = GetComponent<TMP_Text>();
        textContinue.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
        if (textIntroduction.GetComponent<TypewriterEffect>().textWritten)
        {
            textContinue.SetText("Press Enter to Continue...");
        }
    }
}
