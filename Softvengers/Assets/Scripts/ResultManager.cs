using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<bool> results = new List<bool>();
    public static List<double> scores = new List<double>();
    public GameObject resultRecord;
    public Transform title;
    public Player playerData;
    public Navigation navigation;

    void Start()
    {
        
        float startY = 200.0f;
        float x = 0;
        float z = 0;

        double sum = 0;
        for (int i = 0; i < scores.Count; ++i)
            sum += scores[i];
        title.GetComponent<Text>().text = string.Format("Final Score : {0}", Math.Round(sum, 2));
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

            score.GetComponent<Text>().text = Math.Round(scores[i], 2).ToString();
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
        upDatePlayerProgress();
    }

    public void BackButton()
    {
        results = new List<bool>();
        scores = new List<double>();
        SceneManager.LoadScene("PlanetScene");
    }

    public static void AddRecord(bool result, double score)
    {
        Debug.Log(results.Count + 1);
        results.Add(result);
        scores.Add(score);
    }

    void upDatePlayerProgress()
    {
        var numQuery = results.Where(result => result == true);

        if (numQuery.Count() > 5) // Pass
        {
            if (isLatestLevel())
            {
                if (playerData.planetProgress < 2) // Not last planet
                {
                    playerData.planetProgress += 1;
                }
                else if (playerData.solarSystemProgress < Multiverse.getSolarSystems(playerData.universePogress).Count) // Check if player didnt exceed max solar systems of the universe 
                {
                    playerData.solarSystemProgress += 1;
                    playerData.planetProgress = 0;
                }
                else if (playerData.universePogress < Multiverse.getUniverses().Count)// Check if player has not exceeded total num universes
                {
                    playerData.universePogress += 1;
                    playerData.solarSystemProgress = 0;
                    playerData.planetProgress = 0;
                } 
            }
        }
    }

    bool isLatestLevel()
    {
        if (navigation.universeSelected == playerData.universePogress && navigation.solarSystemSelected == playerData.solarSystemProgress && playerData.planetProgress == navigation.planetSelected)
            return true;
        return false;
    }

}
