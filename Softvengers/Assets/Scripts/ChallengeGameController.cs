using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * _id
:
605723d04178e834a4d4b2a3
wrongOptions:Array
points:15
universeID:0
solarID:0
planetID:2
questionID:1
body:"What does MERN mean?"
correctOption:"Mongo, Express, React, Node"
__v:0
 */
//needs to be tested, was working with empty db
[System.Serializable]
public class ChaQuestion
{
    //question structure for challenge
    public string _id;
    public string[] wrongOptions;
    public int points;
    public int universeID;
    public int solarID;
    public int planetID;
    public int questionID;
    public string body;
    public string correctOption;
    public int _v;

}
[System.Serializable]
public class ChaQuestions
{
    public ChaQuestion[] questions;
}


public class ChallengeGameController : AssignmentGameController {
    public static float startTime;
    public static float endTime;
    
    public override bool IsGameOver()
    {
        if(base.IsGameOver())
        {
            endTime = Time.time;

        }
        return (base.IsGameOver());
    }

    
    public override double CalculateScore(bool result, double baseScore)
    {
        if (result)
        {
            return baseScore * (1 + ratio);
        }

        return 0.0;
    }

    public new void StoreScore(bool result, double score)
    {
        ResultManager.AddRecord(result, score);
        //ChallengeResultManager.AddRecord(result, score);
    }

    public override void Start()
    {
        
        StartCoroutine(ServerController.Get("http://localhost:5000/student/challenge/getQuestions?challengeID=60583372dadc7c4b705fb5a9",
        result =>
        {
            if (result != null)
            {
                Debug.Log(result);

                ChaQuestions qSet = JsonUtility.FromJson<ChaQuestions>("{ \"questions\": " + result + "}");
                print(qSet.questions.Length);
                foreach (ChaQuestion q in qSet.questions)
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
        startTime = Time.time;
        DisplayQuestion();
    }

    // Initialize
    

    public ChallengeGameController()
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
        Debug.Log(questionBank.Count);
    }
}
