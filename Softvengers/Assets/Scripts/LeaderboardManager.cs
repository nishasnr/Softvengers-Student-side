using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;


public class LeaderboardManager : MonoBehaviour
{
   
    private Transform entryContainer;
    private Transform entryTemplate;
    private Dictionary<int, List<string>> leaders;

    public LeaderboardManager()
    {
        leaders = new Dictionary<int, List<string>>(){
            { 1, new List<string>(){"Player1", "1000" } },
            { 2, new List<string>(){"Player2", "900" } },
            { 3, new List<string>(){"Player3", "800" } },
            { 4, new List<string>(){"Player4", "700" } },
            { 5, new List<string>(){"Player5", "600" } },
            { 6, new List<string>(){"Player6", "500" } },
            { 7, new List<string>(){"Player7", "400" } },
            { 8, new List<string>(){"Player8", "300" } },
            { 9, new List<string>(){"Player9", "200" } },
            { 10, new List<string>(){"Player10", "100" } }
        };
    }

    private Transform assignRank(Transform entryTransform, int val)
    {
        Transform rank = entryTransform.Find("Rank");
        Text t = rank.GetComponent<Text>();
        t.text = val.ToString();
        return entryTransform;
    }

    private Transform assignUsername(Transform entryTransform, string name)
    {
        Transform username = entryTransform.Find("Username");
        Text t = username.GetComponent<Text>();
        t.text = name;
        return entryTransform;
    }

    private Transform assignScore(Transform entryTransform, string val)
    {
        Transform score = entryTransform.Find("Score");
        Text t = score.GetComponent<Text>();
        t.text = val;
        return entryTransform;
    }

    private void Awake()
    {
        entryContainer = transform.Find("Container");
        entryTemplate = entryContainer.Find("Entry_template");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 120f;

        for (int i=1; i<=10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform = assignRank(entryTransform, i);
            entryTransform = assignUsername(entryTransform, leaders[i][0]);
            entryTransform = assignScore(entryTransform, leaders[i][1]);
            entryTransform.gameObject.SetActive(true);

        }
    }
    

}
