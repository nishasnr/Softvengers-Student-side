using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public Navigation navigationData;
    List<Question> questions = new List<Question>();
    private int questionNumber = 0;
    public Transform questionName;
    public Transform option1;
    public Transform option2;
    public Transform option3;
    public Transform option4;

    public Image healthBar;
    public Image timeBar;

    private GameObject fragment;
    private GameObject currAsteroid;
    public GameObject asteroid;

    private float timeDelay = 1.25f;
    private float health = 100.0f;
    private float maxHealth = 100.0f;

    private float startTime = 0.0f;
    private float questionTime = 7f;
    private float ratio;
    private float baseScore = 10;

    private bool paused = false;

    private bool failedQuestion = false;
    private bool isLocked = false;

    //Create List of Questions
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
    
    
    void Start()
    {
        paused = true;
        DisplayQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        ratio = (questionTime - timeElapsed) / questionTime;
        ratio = ratio >= 0.0f ? ratio : 0.0f;
        Vector3 position = Vector3.zero;
        position.z = ratio * 10 + 0.35f;
        if (!paused) {

            timeBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
            timeBar.fillAmount = ratio;
            
            currAsteroid.transform.position = position;
            if (ratio <= 0.0f)
            {
                DecreaseHealth();
                ResultManager.AddRecord(false, 0);
                BreakAsteroid();
                questionNumber++;
                paused = true;
                Invoke("DisplayQuestion", timeDelay); 
            }
        }

        if (failedQuestion)
        {
            currAsteroid.transform.position = position;
            if (ratio <= 0.0f)
            {
                DecreaseHealth();
                BreakAsteroid();
                paused = true;
                failedQuestion = false;
                Invoke("DisplayQuestion", timeDelay);
            }
        }
    }

    void DisplayQuestion()
    {
    
        isLocked = false;
        if (questionNumber == questions.Count || GameOver())
        {
            SceneManager.LoadScene("ResultScene");
            return;
        }

        failedQuestion = false;
        questionName.GetComponent<Text>().text = this.questions[questionNumber].questionName;
        option1.GetComponent<Text>().text = this.questions[questionNumber].options[0].option;
        option2.GetComponent<Text>().text = this.questions[questionNumber].options[1].option;
        option3.GetComponent<Text>().text = this.questions[questionNumber].options[2].option;
        option4.GetComponent<Text>().text = this.questions[questionNumber].options[3].option;
        startTime = Time.time;
        currAsteroid = Instantiate(asteroid, new Vector3(0, 0, 15), Random.rotation);
        paused = false;
    }

    public void CheckAnswer(int optionNumber)
    {
        if (isLocked)
            return;
        isLocked = true;
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        List<Option> options = this.questions[questionNumber].options;

        if (options[optionNumber].isCorrect == true)
        {
            
            float score = baseScore + ratio * baseScore;
            ResultManager.AddRecord(true, score);
            Debug.Log("Correct");
            
            shooter.releaseMissile();
            BreakAsteroid();
            questionNumber++;
            Invoke("DisplayQuestion", timeDelay);

        }
        else
        {
           
            failedQuestion = true;
            Debug.Log("Wrong");
            float score = 0.0f;
            ResultManager.AddRecord(false, score);
            //DecreaseHealth();

            questionNumber++;
            
        }
        paused = true;
    }



    void BreakAsteroid()
    {
        GameObject fractured = currAsteroid.GetComponent<Fracture>().returnFractured();
        fragment = Instantiate(fractured, currAsteroid.transform.position, currAsteroid.transform.rotation);
        Destroy(currAsteroid);
        Invoke("DestroyFragment", 1);
    }

    void DestroyFragment()
    {
        Shoot shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        shooter.stopMissile();
        Destroy(fragment);
    }

    void DecreaseHealth()
    {
        health -= 20.0f;
        float ratio = health / maxHealth;
        healthBar.fillAmount = ratio;
        healthBar.color = new Color((1 - ratio), (ratio), 0.0f, 0.8f);
    }

    bool GameOver()
    {
        if (health <= 0.0f)
            return true;
        return false;
    }
}
