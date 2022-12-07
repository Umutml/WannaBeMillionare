using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Questions", fileName = "Question")]
public class QuestionScriptableObject : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question;
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctIndex;


    
    public string GetQuestion()
    {
        return question;
    }
    public int GetCorrectAnswer()
    {
        return correctIndex;
    }
    public string GetAnswer(int index)
    {
        return answers[index];
    }


}
