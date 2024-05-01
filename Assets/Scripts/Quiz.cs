using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz 
{
    public string question;
    public string answer1, answer2, answer3, answer4;
    public int correctAnswer, levelQuiz;

    public Quiz(string question, string answer1, string answer2, string answer3, string answer4, int correctAnswer,  int levelQuiz)
    {
        this.question = question;
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer3 = answer3;
        this.answer4 = answer4;
        this.correctAnswer = correctAnswer;
        this.levelQuiz = levelQuiz;
    }
}
