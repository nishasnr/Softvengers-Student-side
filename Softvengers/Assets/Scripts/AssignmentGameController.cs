﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
   {
   wrongOptions: [ 'Dance', 'With', 'You' ],
   points: 1,
   _id: 6062ab4fb5e20c11b8b8878c,
   assignmentID: 123,
   questionID: 3,
   body: 'My',
   correctOption: 'Again',
   __v: 0
 }
   */

[System.Serializable]
public class AssQuestion
{
    public string[] wrongOptions;
    public int points;
    public string _id;
    public int assignmentID;
    public int questionID;
    public string body;
    public string correctOption;
    public int _v;
}
[System.Serializable]
public class AssQuestions
{
    public AssQuestion[] questions;
}

public class AssignmentGameController : MonoBehaviour
{


    // Used to retrieve the questions
    public static string assignmentID;

    // Question Details on Screen
    public Transform questionName;
    public Transform option1;
    public Transform option2;
    public Transform option3;
    public Transform option4;

    // Question Time Limit
    public Image timeBar;

    // Asteroid for each question
    public GameObject asteroid;

    // Asteroids in Game
    private GameObject fragment;
    private GameObject currAsteroid;

    // Time Delay between each question 
    protected float timeDelay = 1.25f;

    // Question Time Limits

    private float startTime = 0.0f;


    // *************************************************************************************
    // Change to Data from server
    // *************************************************************************************
    private float questionTime = 7f;


    // Synchronization
    protected bool failedQuestion = false;
    protected bool isLocked = false;
    protected bool paused = false;

    // Time ratio
    protected float ratio;

    // Game Questions

    // *************************************************************************************
    // Once you receive data from server get the questions from that and place in List<Questions>
    // *************************************************************************************

    protected List<Question> questionBank = new List<Question>();
    protected int numQuestions;
    protected int questionNumber = 0;

    protected string nextScene = "AssignmentResultScene";


    protected virtual void Start()
        {
            StartCoroutine(ServerController.Get("http://localhost:5000/student/assignments/getAssignmentQuestions?assignmentID=123",
            result =>
            {
                if (result != null)
                {
                    Debug.Log(result);

                    AssQuestions qSet = JsonUtility.FromJson<AssQuestions>("{ \"questions\": " + result + "}");
                    Debug.Log("Number of questions: " + qSet.questions.Length);
                    foreach(AssQuestion q in qSet.questions)
                    {
                        print(q.body);
                    }

                }
                else
                {
                    Debug.Log("No sent challenges");

                }
            }
            ));
            numQuestions = questionBank.Count;
            paused = true;
            DisplayQuestion();
        }

    void Update()
    {

        float timeElapsed = Time.time - startTime;
        ratio = (questionTime - timeElapsed) / questionTime;
        ratio = ratio >= 0.0f ? ratio : 0.0f;
        Vector3 position = Vector3.zero;
        position.z = ratio * 10 + 0.35f;

        // When the player has still not answered the question

        if (!paused)
        {

            timeBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
            timeBar.fillAmount = ratio;

            currAsteroid.transform.position = position;
            
            // If Time is up
            
            if (ratio <= 0.0f)
            {
                StoreScore(false, 0);

                //ResultManager.AddRecord(false, 0);
                PenalizePlayer();
                failedQuestion = false;
                BreakAsteroid();
                questionNumber++;
                paused = true;
                Invoke(nameof(DisplayQuestion), timeDelay);
            }
        }

        if (failedQuestion)
        {
            // If asteroid hit you
            currAsteroid.transform.position = position;
            if (ratio <= 0.0f)
            {
                // Add has refinement
                PenalizePlayer();
                failedQuestion = false;
                BreakAsteroid();
                paused = true;
                Invoke(nameof(DisplayQuestion), timeDelay);
            }
        }
    }


    public void DisplayQuestion()
    {
        isLocked = false;

        if (IsGameOver())
        {
            SceneManager.LoadScene(nextScene);
            return;
        }

        failedQuestion = false;

        questionName.GetComponent<Text>().text = this.questionBank[questionNumber].questionName;
        option1.GetComponent<Text>().text = this.questionBank[questionNumber].options[0].option;
        option2.GetComponent<Text>().text = this.questionBank[questionNumber].options[1].option;
        option3.GetComponent<Text>().text = this.questionBank[questionNumber].options[2].option;
        option4.GetComponent<Text>().text = this.questionBank[questionNumber].options[3].option;

        startTime = Time.time;
        currAsteroid = Instantiate(asteroid, new Vector3(0, 0, 15), Random.rotation);
        paused = false;
    }

    public void CheckAnswer(int optionNumber)
    {
        // If player has already selected an option
        if (isLocked)
            return;
        isLocked = true;

        List<Option> options = this.questionBank[questionNumber].options;

        bool result = false;

        // Answer is Correct 
        if (options[optionNumber].isCorrect)
        {
            result = true;
            RewardPlayer();
        }

        else
        {
            //PenalizePlayer();
            failedQuestion = true;
        }

        // Set Base Score to the questions score
        double baseScore = 5.0f;
        double score = this.CalculateScore(result, baseScore);

        this.StoreScore(result, score);

        questionNumber += 1;
        paused = true;

    }

    void BreakAsteroid()
    {
        GameObject fractured = currAsteroid.GetComponent<Fracture>().returnFractured();
        fragment = Instantiate(fractured, currAsteroid.transform.position, currAsteroid.transform.rotation);
        Destroy(currAsteroid);
        Invoke(nameof(DestroyFragment), 1);
    }

    void DestroyFragment()
    {
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        shooter.stopMissile();
        Destroy(fragment);
    }

    public void StoreScore(bool result, double score)
    {
        //ResultManager.AddRecord(result, score);
        AssignmentResultManager.AddRecord(result, score);
    }

    // Make Abstract
    public virtual double CalculateScore(bool result, double baseScore)
    {
        if (result)
        {
            return baseScore;
        }

        return 0.0;
    }

    public virtual void PenalizePlayer()
    {
        failedQuestion = true;
    }

    public virtual void RewardPlayer()
    {
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        shooter.releaseMissile();
        BreakAsteroid();
        Invoke(nameof(DisplayQuestion), timeDelay);
    }

    public virtual bool IsGameOver()
    {
        if (questionNumber < numQuestions)
            return false;

        return true;
    }




    

    // Initialize
    public AssignmentGameController()
    {
        Option o1 = new Option("A", true);
        Option o2 = new Option("B", false);
        Option o3 = new Option("C", false);
        Option o4 = new Option("D", false);
        List<Option> ol1 = new List<Option>();
        ol1.Add(o1); ol1.Add(o2); ol1.Add(o3); ol1.Add(o4);
        Question q1 = new Question("1E", ol1);

        Option o5 = new Option("E", false);
        Option o6 = new Option("F", true);
        Option o7 = new Option("G", false);
        Option o8 = new Option("H", false);
        List<Option> ol2 = new List<Option>();
        ol2.Add(o5); ol2.Add(o6); ol2.Add(o7); ol2.Add(o8);
        Question q2 = new Question("2E", ol2);

        Option o9 = new Option("I", false);
        Option o10 = new Option("J", false);
        Option o11 = new Option("K", true);
        Option o12 = new Option("L", false);
        List<Option> ol3 = new List<Option>();
        ol3.Add(o9); ol3.Add(o10); ol3.Add(o11); ol3.Add(o12);
        Question q3 = new Question("3E", ol3);

        Option o13 = new Option("M", false);
        Option o14 = new Option("N", false);
        Option o15 = new Option("O", false);
        Option o16 = new Option("P", true);
        List<Option> ol4 = new List<Option>();
        ol4.Add(o13); ol4.Add(o14); ol4.Add(o15); ol4.Add(o16);
        Question q4 = new Question("4E", ol4);

        Option o17 = new Option("Q", false);
        Option o18 = new Option("R", false);
        Option o19 = new Option("S", false);
        Option o20 = new Option("T", true);
        List<Option> ol5 = new List<Option>();
        ol5.Add(o17); ol5.Add(o18); ol5.Add(o19); ol5.Add(o20);
        Question q5 = new Question("5E", ol5);

        Option o21 = new Option("A", true);
        Option o22 = new Option("B", false);
        Option o23 = new Option("C", false);
        Option o24 = new Option("D", false);
        List<Option> ol6 = new List<Option>();
        ol6.Add(o21); ol6.Add(o22); ol6.Add(o23); ol6.Add(o24);
        Question q6 = new Question("6E", ol6);

        Option o25 = new Option("E", false);
        Option o26 = new Option("F", true);
        Option o27 = new Option("G", false);
        Option o28 = new Option("H", false);
        List<Option> ol7 = new List<Option>();
        ol7.Add(o25); ol7.Add(o26); ol7.Add(o27); ol7.Add(o28);
        Question q7 = new Question("7E", ol7);

        Option o29 = new Option("I", false);
        Option o30 = new Option("J", false);
        Option o31 = new Option("K", true);
        Option o32 = new Option("L", false);
        List<Option> ol8 = new List<Option>();
        ol8.Add(o29); ol8.Add(o30); ol8.Add(o31); ol8.Add(o32);
        Question q8 = new Question("8E", ol8);

        Option o33 = new Option("M", false);
        Option o34 = new Option("N", false);
        Option o35 = new Option("O", false);
        Option o36 = new Option("P", true);
        List<Option> ol9 = new List<Option>();
        ol9.Add(o33); ol9.Add(o34); ol9.Add(o35); ol9.Add(o36);
        Question q9 = new Question("9E", ol9);

        Option o37 = new Option("Q", false);
        Option o38 = new Option("R", false);
        Option o39 = new Option("S", false);
        Option o40 = new Option("T", true);
        List<Option> ol10 = new List<Option>();
        ol10.Add(o37); ol10.Add(o38); ol10.Add(o39); ol10.Add(o40);
        Question q10 = new Question("10E", ol10);

        this.questionBank.Add(q1);
        this.questionBank.Add(q2);
        this.questionBank.Add(q3);
        this.questionBank.Add(q4);
        this.questionBank.Add(q5);
        this.questionBank.Add(q6);
        this.questionBank.Add(q7);
        this.questionBank.Add(q8);
        this.questionBank.Add(q9);
        this.questionBank.Add(q10);
    }
}
