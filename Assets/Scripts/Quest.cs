using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int LevelQuest { get; set; }

    public int Pages;
    public bool IsCompleted { get; set; }
    public string Instructions { get; set; }
    public List<Quiz> Quizzes { get; set; }

    public Quest(string title, string description, int levelQuest, int pages, string instructions,
        List<Quiz> quizzes)
    {
        Title = title;
        Description = description;
        LevelQuest = levelQuest;
        Pages = pages;
        Instructions = Instructions;
        IsCompleted = false;
        Instructions = instructions;
        Quizzes = quizzes;
    }

}
