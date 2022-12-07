using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TestQuiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionScriptableObject currentQuestionScript;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionScriptableObject> questions = new List<QuestionScriptableObject>();

    [Header("Answers")]
    int correctAnswerIndex;
    bool isAnsweredEarly;
    [SerializeField] Button[] answerButtons = new Button[4];
    TextMeshProUGUI buttonText;
    
    [Header("Button Sprites")]
    [SerializeField] Sprite correctSprite;
    [SerializeField] Sprite defaultSprite;
    Image buttonImage;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score scoreScripts;
    [SerializeField]Slider scoreSlider;
    public bool gameComplete;
    public GameObject endGameCanvas;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreScripts = FindObjectOfType<Score>();
        //scoreSlider.maxValue = questions.Count;
        
        scoreText.text = "%0";
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillAmount;
        if (timer.loadNextQuestion)
        {
            isAnsweredEarly = false;
            NextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!isAnsweredEarly && !timer.isAnswering)
        {
            DisplayAnswer(-1);
            ButtonReadyNot(false);
        }
        if (scoreScripts.questionsSeen == 10)
        {
            Invoke(nameof(EndScreenDisplay), 5);
        }
    }

    void EndScreenDisplay()
    {
        endGameCanvas.SetActive(true);
    }

    void DisplayAnswer(int index)
    {
        if (index == currentQuestionScript.GetCorrectAnswer())
        {
            questionText.text = "DOGRU CEVAP!!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.color = Color.green;
            buttonImage.sprite = correctSprite;
            scoreScripts.IncreaseCorrectOne();
        }
        else
        {
            correctAnswerIndex = currentQuestionScript.GetCorrectAnswer();
            string correctAnswer = currentQuestionScript.GetAnswer(correctAnswerIndex);
            questionText.text = "BILEMEDIN DOGRU ÞIK -- " + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.color = Color.green;
            buttonImage.sprite = correctSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        isAnsweredEarly = true;
        DisplayAnswer(index);
        ButtonReadyNot(false);
        timer.FinishTimer();
        scoreScripts.IncreaseSeenOne();
        scoreText.text = "%" + scoreScripts.CalculateScore();
        scoreSlider.value = scoreScripts.CalculateScore();

    }
    void NextQuestion()
    {
        if (questions.Count > 0)
        {
            ButtonReadyNot(true);
            SetDefaultColorSprite();
            GetRandomQuestion();
            QuestionReady();
            
        }   
    }

    void GetRandomQuestion() 
    {
        int index = Random.Range(0, questions.Count);
        currentQuestionScript = questions[index];

        if (questions.Contains(currentQuestionScript))
        {
            questions.Remove(currentQuestionScript);
        }
        
    }

    void SetDefaultColorSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.color = Color.red; // Default Sprite Color
            buttonImage.sprite = defaultSprite;
        }
    }
    void QuestionReady()
    {
        questionText.text = currentQuestionScript.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestionScript.GetAnswer(i);
        }
        
    }
    void ButtonReadyNot(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button answerButtonsInteractable = answerButtons[i].GetComponent<Button>();
            answerButtonsInteractable.interactable = state;
        }
    }
}
