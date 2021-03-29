using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qsts : MonoBehaviour
{
    public int questionID;
    public string body;
    public string correctOption;
    public string[] wrongOptions;
    public int difficulty;

    public qsts(int questionID, string body, string correctOption, string[] wrongOptions, int difficulty)
    {
        this.questionID = questionID;
        this.body = body;
        this.correctOption = correctOption;
        this.wrongOptions = wrongOptions;
        this.difficulty = difficulty;
    }

    public string stringify()
    {

        return JsonUtility.ToJson(this);
    }

    public qsts Parse(string json)
    {
        return JsonUtility.FromJson<qsts>(json);
    }

}
