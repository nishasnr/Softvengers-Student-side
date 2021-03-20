using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceivedChallengeDisplay : MonoBehaviour
{
    public string selectedChallengeid;
    public GameObject PendingChallenge;
    public GameObject CompletedChallenge;
    public GameObject infoContent;
    public GameObject scroll;
    public Canvas accept;
    public Canvas decline;
    public Canvas info;
    public bool accept_a = false;
    public bool decline_a = false;
    public bool info_a = false;
    public List<string> pending = new List<string>();
    public List<string> completed = new List<string>();
    // Start is called before the first frame update

    public static Dictionary<string, Dictionary<string, string>> RC_info =
 new Dictionary<string, Dictionary<string, string>>()
 {
        {
            "01",
            new Dictionary<string, string>
            {
                {"name", "The Ultimate Challenge"},
                {"sender", "Thanos"},
                {"attempted","0" },
                {"c_score","200" },
                {"c_time", "7 min"},
                {"your_score","0" },
                {"your_time","0" },
            }
        },
        {
            "02",
            new Dictionary<string, string>
            {
                {"name", "Awesome Challenge"},
                {"sender", "Hella"},
                {"c_score","150" },
                {"c_time", "5 min"},
                {"attempted","0" },
                {"your_score","0" },
                {"your_time","0" }
            }
        },
        {
            "03",
            new Dictionary<string, string>
            {
                {"name", "Infinity Stones"},
                {"sender", "Iron Man"},
                {"attempted","1" },
                {"c_score","100" },
                {"c_time", "5 min"},
                {"your_score","100" },
                {"your_time","2 min" }
            }
        },
        {
            "04",
            new Dictionary<string, string>
            {
                {"name", "Gauntlet"},
                {"sender", "Vision"},
                {"c_score","300" },
                {"c_time", "4 min"},
                {"attempted","1" },
                {"your_score","50" },
                {"your_time","1 min" }
            }
        },

 };

    Dictionary<string, List<string>> topic_info =
new Dictionary<string, List<string>>()
{
    { "01", new List<string>{"Requirements Engineering","Implementation"} },
    { "02", new List<string>{"SS1","SS2","SS3","SS4"} },
    { "03", new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
    { "04", new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
};


    void Start()
    {
       
        foreach (var entry in RC_info)
        {
            if (entry.Value["attempted"] == "0")
            {
                pending.Add(entry.Key);
            }
            else
            {
                completed.Add(entry.Key);
            }
        }

        Vector3 curPos = new Vector3(-12, -86, 0);
        foreach (var item in pending)
        {
            
            var newPC = Instantiate(PendingChallenge, gameObject.transform, false);
            newPC.name = item;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                if (child.name == "AcceptButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(accept_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                }
                else if (child.name == "DeclineButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(decline_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                }
                else if (child.name == "InfoButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(info_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                    btn.onClick.AddListener(setInfopopup);
                }
                else if(child.name=="SnoText")
                {
                    Text sText= child.GetComponentInChildren<Text>();
                    sText.text = item;
                }
                else if(child.name=="Cname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = RC_info[item]["name"];
                }

            }
            curPos -=new Vector3(0, 65, 0);

        }


        foreach (var item in completed)
        {

            var newPC = Instantiate(CompletedChallenge, gameObject.transform, false);
            newPC.name = item;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = "Score: " + RC_info[item]["your_score"];
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = "Time: "+RC_info[item]["your_time"];
                }
                else if (child.name == "InfoButton")
                {
                    Button btn = child.GetComponentInChildren<Button>();
                    btn.onClick.AddListener(info_pop);
                    btn.onClick.AddListener(delegate {
                        setSelectedID(btn);
                    });
                    btn.onClick.AddListener(setInfopopup);

                }
                else if (child.name == "SnoText")
                {
                    Text sText = child.GetComponentInChildren<Text>();
                    sText.text = item;
                }
                else if (child.name == "Cname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = RC_info[item]["name"];
                }

            }
            curPos -= new Vector3(0, 65, 0);

        }


    }

    public void accept_pop()
    {
        if (accept_a == false)
        {
            accept_a = true;
            accept.enabled = true;
        }
        else if (accept_a == true)
        {
            accept_a = false;
            accept.enabled = false;
        }
    }

    public void decline_pop()
    {
        if (decline_a == false)
        {
            decline_a = true;
            decline.enabled = true;
        }
        else if (decline_a == true)
        {
            decline_a = false;
            decline.enabled = false;
        }
    }

    public void info_pop()
    {
        if (info_a == false)
        {
            info_a = true;
            info.enabled = true;
        }
        else if (info_a == true)
        {
            info_a = false;
            info.enabled = false;
        }
    }

    public void setSelectedID(Button btn)
    {
        selectedChallengeid = btn.transform.parent.gameObject.name;
    }

    public void setInfopopup()
    {
        Scrollbar s = scroll.GetComponent<Scrollbar>();
        s.value = 1;
        foreach (Transform child in infoContent.transform)
        {
             if (child.name == "Cname")
            {
                Text sText = child.GetComponentInChildren<Text>();
                sText.text = RC_info[selectedChallengeid]["sender"];
            }
            else if (child.name == "CTime")
            {
                Text cText = child.GetComponentInChildren<Text>();
                cText.text = RC_info[selectedChallengeid]["c_time"];
            }
            else if (child.name == "Cscore")
            {
                Text cText = child.GetComponentInChildren<Text>();
                cText.text = RC_info[selectedChallengeid]["c_score"];
            }
            else if(child.name=="ScoreMessage")
            {
                Text cText = child.GetComponentInChildren<Text>();
                if(RC_info[selectedChallengeid]["attempted"]=="0")
                {
                    cText.text = "Can you beat this score?";
                }
                else if(int.Parse(RC_info[selectedChallengeid]["c_score"])<=int.Parse(RC_info[selectedChallengeid]["your_score"]) )
                {
                    cText.text = "Good Job!";
                }
                else
                {
                    cText.text = "Better luck next time";
                }
            }
            else if(child.name=="Topic List")
            {
                Text cText = child.GetComponentInChildren<Text>();
                string topics = "";
                foreach(var item in topic_info[selectedChallengeid])
                {
                    topics += item + "\n";
                }
                cText.text = topics;


            }
        }

    }

    public void OnDeclineYes()
    {
        bool start_shift = false;
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "ContentHeading")
            {
                continue;
            }
            else if (child.name==selectedChallengeid)
            {
                Destroy(child.gameObject);
                start_shift = true;
            }
            else if(start_shift)
            {
                child.localPosition += new Vector3(0, 65, 0);
            }
        }
    }

    public void OnAcceptYes()
    {
        //add code to load new game scene and send challenge id to db
    }
}
