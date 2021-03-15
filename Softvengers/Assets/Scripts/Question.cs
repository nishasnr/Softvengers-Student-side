using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string questionName;
    public List<Option> options;

    public Question(string questionName, List<Option> options)
    {
        this.questionName = questionName;
        this.options = options;
    }
}
