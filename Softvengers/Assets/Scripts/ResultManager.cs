using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<bool> results = new List<bool>();
    public static List<float> scores = new List<float>();
    public GameObject resultRecord;
    public Transform title;

    void Start()
    {
        /*
        for (int i =0; i<5; i++)
        {
            scores.Add(1.0f);
            if (i % 2 == 0)
                results.Add(true);
            else
                results.Add(false);
        }
        */
        float startY = 200.0f;
        float x = 0;
        float z = 0;

        float sum = 0;
        for (int i = 0; i < scores.Count; ++i)
            sum += scores[i];
        title.GetComponent<Text>().text = string.Format("Final Score : {0}", Math.Round((Double)sum, 2));
        for (int i=0; i<results.Count; ++i)
        {
            GameObject result = Instantiate(resultRecord, new Vector3(x, startY, z), Quaternion.identity);
            result.transform.SetParent(GameObject.FindGameObjectWithTag("ResultPage").transform, false);
            Transform questionNumber = result.transform.Find("QuestionNumber");
            Transform outcome = result.transform.Find("Outcome");
            Transform score = result.transform.Find("Score");
            questionNumber.GetComponent<Text>().text = string.Format("Question {0}", i+1);
            outcome.GetComponent<Text>().text = results[i] ? "Correct" : "Wrong";
            outcome.GetComponent<Text>().color = results[i] ? new Color(0, 1, 0) : new Color(1, 0, 0);
            score.GetComponent<Text>().text = scores[i].ToString();
            startY -= 60.0f;
        }

        for (int i = results.Count; i<10; i++)
        {
            GameObject result = Instantiate(resultRecord, new Vector3(x, startY, z), Quaternion.identity);
            result.transform.SetParent(GameObject.FindGameObjectWithTag("ResultPage").transform, false);
            Transform questionNumber = result.transform.Find("QuestionNumber");
            Transform outcome = result.transform.Find("Outcome");
            questionNumber.GetComponent<Text>().text = string.Format("Question {0}", i + 1);
            outcome.GetComponent<Text>().text = "Not Attempted";
            outcome.GetComponent<Text>().color = new Color(0.4f, 0.4f, 0.4f);
            startY -= 60.0f;
        }
    }

    public void BackButton()
    {
        results = new List<bool>();
        scores = new List<float>();
        SceneManager.LoadScene("PlanetScene");
    }

    public static void AddRecord(bool result, float score)
    {
        results.Add(result);
        scores.Add(score);
    }

}
