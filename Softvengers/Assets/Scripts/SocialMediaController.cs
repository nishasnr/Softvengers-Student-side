using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaController : MonoBehaviour
{
      
    public void openTwitter()
    {
        double sum = 0;
        for (int i = 0; i < ResultManager.scores.Count; ++i)
            sum += ResultManager.scores[i];

        string textToDisplay = "I scored " + sum.ToString() + ". Try and beat my score. ";
        Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(textToDisplay) + "&amp;lang=en");
    }
}
