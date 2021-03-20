using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SentChallenge : MonoBehaviour
{
    public string selectedChallengeid;
    public GameObject SentChallengetemp;
    public GameObject infoContent;
    public GameObject leaderboardTemp;
    public GameObject scroll;
    public Canvas info;
    public bool info_a = false;
    public List<GameObject> lb_list=new List<GameObject>();
    // Start is called before the first frame update

    public static Dictionary<string, Dictionary<string, string>> SC_info =
 new Dictionary<string, Dictionary<string, string>>()
 {
        {
            "01",
            new Dictionary<string, string>
            {
                {"name", "The Snap"},
                {"sender", "Thanos"},
                {"your_score","100" },
                {"your_time","3 min" },
            }
        },
        {
            "02",
            new Dictionary<string, string>
            {
                {"name", "Ragnarok"},
                {"sender", "Hella"},
                {"your_score","200" },
                {"your_time","2 min" }
            }
        },

 };

    Dictionary<string, List<string>> topic_info =
new Dictionary<string, List<string>>()
{
    { "01", new List<string>{"Requirements Engineering","Implementation"} },
    { "02", new List<string>{"SS1","SS2","SS3","SS4"} },
   
};


    public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> leaderboard_info =
 new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
 {
     {
         "01",
         new Dictionary<string, Dictionary<string, string>>
         {
             { "Thanos",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","300"},
                 {"time","3 min"},
             }

             },

             { "Hella",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","200"},
                 {"time","1 min"},
             }

             },

             { "Scarlet Witch",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","200"},
                 {"time","2 min"},
             }

             },

             { "Vision",
             new Dictionary<string,string>()
             {
                 {"attempted","0"},
                 {"score","0"},
                 {"time","0"},
             }

             },
         }
     },


     {
         "02",
         new Dictionary<string, Dictionary<string, string>>
         {
             { "Falcon",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","400"},
                 {"time","3 min"},
             }

             },

             { "Bucky",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","300"},
                 {"time","1 min"},
             }

             },

             { "Black Widow",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","200"},
                 {"time","2 min"},
             }

             },

             { "Spiderman",
             new Dictionary<string,string>()
             {
                 {"attempted","1"},
                 {"score","100"},
                 {"time","1 min"},
             }

             },
         }
     }


 };


    void Start()
    { 

        Vector3 curPos = new Vector3(-12, -86, 0);
        foreach (var item in SC_info)
        {

            var newPC = Instantiate(SentChallengetemp, gameObject.transform, false);
            newPC.name = item.Key;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = "Score: " + item.Value["your_score"];
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = "Time: " + item.Value["your_time"];
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
                    sText.text = item.Key;
                }
                else if (child.name == "Cname")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.Value["name"];
                }

            }
            curPos -= new Vector3(0, 65, 0);

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
            foreach(var item in lb_list)
            {
                Destroy(item);
            }
        }
    }

    public void setSelectedID(Button btn)
    {
        selectedChallengeid = btn.transform.parent.gameObject.name;
    }

    public void setInfopopup()
    {
        //302,-79,0
        /*foreach (Transform child in infoContent.transform)
        {
            print(child.name+child.localPosition);
        }*/
        Scrollbar s = scroll.GetComponent<Scrollbar>();
        s.value = 1;

        Vector3 curPos = new Vector3(302, -79, 0);
        int rank = 1;
        foreach (var item in leaderboard_info[selectedChallengeid])
        {

            var newPC = Instantiate(leaderboardTemp, infoContent.transform, false);
            lb_list.Add(newPC);
            newPC.name = item.Key;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                if (child.name == "PlayerName")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.Key;
                }
                else if (child.name == "Time")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.Value["time"];
                    if (item.Value["score"] == "0" && item.Value["time"] == "0")
                    {
                        cText.text = "-";
                    }
                }
                else if (child.name == "Score")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item.Value["score"];
                    if(item.Value["score"]=="0" && item.Value["time"] == "0")
                    {
                        cText.text = "-";
                    }
                }
                else if(child.name=="Rank")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = rank.ToString();
                
                }

                


            }
            rank += 1;
            curPos -= new Vector3(0, 40, 0);

            foreach(Transform child in infoContent.transform)
            {
                if (child.name == "ChallengeTopics")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    string topics = "";
                    child.localPosition = curPos - new Vector3(0, 70, 0);
                    foreach (var topic in topic_info[selectedChallengeid])
                    {
                        topics += topic+ "\n";
                    }
                    cText.text = topics;


                }

                else if(child.name == "TopicsHeading")
                {
                    child.localPosition = curPos;
                }
            }

        }


        /*Scrollbar s = scroll.GetComponent<Scrollbar>();
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
            else if (child.name == "ScoreMessage")
            {
                Text cText = child.GetComponentInChildren<Text>();
                if (int.Parse(RC_info[selectedChallengeid]["c_score"]) <= int.Parse(RC_info[selectedChallengeid]["your_score"]))
                {
                    cText.text = "Good Job!";
                }
                else
                {
                    cText.text = "Better luck next time";
                }
            }
            else if (child.name == "Topic List")
            {
                Text cText = child.GetComponentInChildren<Text>();
                string topics = "";
                foreach (var item in topic_info[selectedChallengeid])
                {
                    topics += item + "\n";
                }
                cText.text = topics;


            }
        }*/

    }
}
