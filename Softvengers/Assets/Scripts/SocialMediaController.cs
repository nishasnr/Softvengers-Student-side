using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaController : MonoBehaviour
{
    float score;
    string challengeName;
    string textToDisplay;

    private void Start()
    {
        score = ChallengeResultManager.getScore();
        challengeName = ChallengeResultManager.getChallengeName();
        textToDisplay = "I scored " + score.ToString() + " in challenge " + challengeName + ". Try and beat my score. ";
    }

    public void openTwitter()
    {
        Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(textToDisplay) + "&amp;lang=en");
    }

    public void openReddit()
    {
        string title = "Softvengers challenge!";
        Application.OpenURL("http://reddit.com/submit?text=" + WWW.EscapeURL(textToDisplay)+"&title="+WWW.EscapeURL(title));
    }

}
