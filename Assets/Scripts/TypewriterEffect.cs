using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] float typingSpeed;
    private string fullText;
    private TMP_Text textIntroduction;
    private string currentText = "";
    private int currentIndex = 0;
    public bool textWritten = false;
    void Start()
    {
        textIntroduction = GetComponent<TMP_Text>();
        fullText = textIntroduction.text;
        textIntroduction.SetText("");
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        while (currentIndex < fullText.Length)
        {
            currentText += fullText[currentIndex];
            textIntroduction.text = currentText;
            currentIndex++;

            if (currentIndex < fullText.Length && (fullText[currentIndex] == ' ' || fullText[currentIndex] == '\n'))
            {
                currentText += fullText[currentIndex];
                textIntroduction.text = currentText;
                currentIndex++;
            }

            yield return new WaitForSeconds(typingSpeed);
        }
        textWritten = true;
    }
}

