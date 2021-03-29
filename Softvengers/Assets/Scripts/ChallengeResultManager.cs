using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeResultManager : MonoBehaviour
{
    private static float score = 0;
    private static string challenge = "Challenge 1";
    public GameObject resultRecord;

    private void Start()
    {
        float x = 0;
        float y = 200.0f;
        float z = 0;

        GameObject title = Instantiate(resultRecord, new Vector3(x, y, z), Quaternion.identity);
        title.transform.SetParent(GameObject.FindGameObjectWithTag("ChallengeResultPage").transform, false);
        Transform challengeName = title.transform.Find("ChallengeName");
        Transform res = title.transform.Find("Score");
        challengeName.GetComponent<Text>().text = "CHALLENGE NAME";
        res.GetComponent<Text>().text = "SCORE";


        y -= 50.0f;
        GameObject result = Instantiate(resultRecord, new Vector3(x, y, z), Quaternion.identity);
        result.transform.SetParent(GameObject.FindGameObjectWithTag("ChallengeResultPage").transform, false);
        challengeName = result.transform.Find("ChallengeName");
        res = result.transform.Find("Score");
        challengeName.GetComponent<Text>().text = challenge;
        res.GetComponent<Text>().text = score.ToString();

    }

    public static void UpdateScore(float val)
    {
        score += val;
    }

    public static float getScore()
    {
        return score;
    }

    public static void setChallengeName(string s)
    {
        challenge = s;
    }

    public static string getChallengeName()
    {
        return challenge;
    }

    void updateProgress()
    {
        //TODO Set the current challenge as completed in database
    }


}
