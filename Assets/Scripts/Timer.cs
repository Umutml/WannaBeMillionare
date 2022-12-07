
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float valueTimer;
    [SerializeField] float timeForOneQuest = 25f;
    [SerializeField] float correctAnswerTimer = 15f;

    public bool isAnswering;
    public float fillAmount;
    

    public bool loadNextQuestion;


    void Update()
    {
        TimerUpdate();
    }

    void TimerUpdate()
    {
        valueTimer -= Time.deltaTime;

        if (isAnswering)
        {
            if (valueTimer > 0)
            {
                fillAmount = valueTimer / timeForOneQuest;
            }
            else
            {
                isAnswering = false;
                valueTimer = correctAnswerTimer;
            }
        }
        else
        {
            if (valueTimer > 0)
            {
                fillAmount = valueTimer / correctAnswerTimer;
            }
            else
            {
                isAnswering = true;
                valueTimer = timeForOneQuest;
                loadNextQuestion = true;
            }
        }

       
    }

    public void FinishTimer()
    {
        valueTimer = 0;
    }

    

}
