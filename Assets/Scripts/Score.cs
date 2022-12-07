using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    int correctAnswers = 0;
    public int questionsSeen = 0; 
    
    public int GetCorrectOne()
    {
        return correctAnswers;
    }
        
    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }
    
    public void IncreaseCorrectOne()
    {
        correctAnswers++;
    }
    public void IncreaseSeenOne()
    {
        questionsSeen++; 
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt (correctAnswers / (float)questionsSeen * 100);
    }
}
