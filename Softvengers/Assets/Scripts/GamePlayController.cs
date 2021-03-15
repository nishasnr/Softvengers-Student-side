using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    List<Question> questions = new List<Question>();
    private int questionNumber = 0;
    public Transform questionName;
    public Transform option1;
    public Transform option2;
    public Transform option3;
    public Transform option4;

    public Image healthBar;
    public Image timeBar;

    private float health = 100.0f;
    private float maxHealth = 100.0f;

    private float startTime = 0.0f;
    private float questionTime = 15f;


    public GamePlayController()
    {
        questionNumber = 0;
        Option o1 = new Option("A", true);
        Option o2 = new Option("B", false);
        Option o3 = new Option("C", false);
        Option o4 = new Option("D", false);
        List<Option> ol1 = new List<Option>();
        ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
        Question q1 = new Question("What", ol1);

        Option o5 = new Option("E", false);
        Option o6 = new Option("F", true);
        Option o7 = new Option("G", false);
        Option o8 = new Option("H", false);
        List<Option> ol2 = new List<Option>();
        ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
        Question q2 = new Question("Why", ol2);

        Option o9 = new Option("I", false);
        Option o10 = new Option("J", false);
        Option o11 = new Option("K", true);
        Option o12 = new Option("L", false);
        List<Option> ol3 = new List<Option>();
        ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
        Question q3 = new Question("How", ol3);

        Option o13 = new Option("M", false);
        Option o14 = new Option("N", false);
        Option o15 = new Option("O", false);
        Option o16 = new Option("P", true);
        List<Option> ol4 = new List<Option>();
        ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
        Question q4 = new Question("When", ol4);

        Option o17 = new Option("Q", false);
        Option o18 = new Option("R", false);
        Option o19 = new Option("S", false);
        Option o20 = new Option("T", true);
        List<Option> ol5 = new List<Option>();
        ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
        Question q5 = new Question("Where", ol5);

        this.questions.Add(q1);
        this.questions.Add(q2);
        this.questions.Add(q3);
        this.questions.Add(q4);
        this.questions.Add(q5);
    }
    //Create List of Questions
    
    

    void Start()
    {
        DisplayQuestion(questionNumber);
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float ratio = (questionTime - timeElapsed) / questionTime;
        ratio = ratio >= 0.0f ? ratio : 0.0f;
        timeBar.color = new Color((1 - ratio), (ratio), 0);
        timeBar.fillAmount = ratio;
        if (ratio <= 0.0f)
        {
            DecreaseHealth();
            if (questionNumber < questions.Count - 1)
            {
                questionNumber++;
                DisplayQuestion(questionNumber);
            }

        }
    }

    void DisplayQuestion(int questionID)
    {
        questionName.GetComponent<Text>().text = this.questions[questionID].questionName;
        option1.GetComponent<Text>().text = this.questions[questionID].options[0].option;
        option2.GetComponent<Text>().text = this.questions[questionID].options[1].option;
        option3.GetComponent<Text>().text = this.questions[questionID].options[2].option;
        option4.GetComponent<Text>().text = this.questions[questionID].options[3].option;
        startTime = Time.time;
    }

    public void CheckAnswer(int optionNumber)
    {
        List<Option> options = this.questions[questionNumber].options;

        if (options[optionNumber].isCorrect == true)
        {
            Debug.Log("Correct");
            if (questionNumber < questions.Count-1)
            {
                questionNumber++;
            }
            
        }
        else
        {
            Debug.Log("Wrong");
            DecreaseHealth();
            if (questionNumber < questions.Count - 1)
            {
                questionNumber++;
            }
        }

        DisplayQuestion(questionNumber);
    }

    void DecreaseHealth()
    {
        health -= 20.0f;
        float ratio = health / maxHealth;
        healthBar.fillAmount = ratio;
        healthBar.color = new Color((1 - ratio), (ratio), 0);
    }
}
