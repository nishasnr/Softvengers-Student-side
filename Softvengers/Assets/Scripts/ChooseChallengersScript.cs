using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//senderScore
//senderTime
//challenegID
//[] receivers

[System.Serializable]
public class sendChallengers
{
    public string challengeID;
    public float senderTime;
    public float senderScore;
    public string[] receivers;
}


public class ChooseChallengersScript : MonoBehaviour
{

    /*public Canvas info;
    public bool info_a = false;
    public GameObject scroll;
    public GameObject toggleTemplate;
    public GameObject infoContent;
    public List<string> selectedStud;
    public List<string> existStud = new List<string> { "A", "B", "C", "D" };
    public List<GameObject> stud_list = new List<GameObject>();
    public void student_list_pop()
    {
        if (info_a == false)
        {
            info_a = true;
            info.enabled = true;
        }
        else if (info == true)
        {
            info_a = false;
            info.enabled = false;
            foreach (var item in stud_list)
            {
                Destroy(item);
            }
            foreach (Transform child in infoContent.transform)
            {
                selectedStud = new List<string>();
                if (child.name=="ChallengerSelectionText")
                {
                    continue;
                }
                else
                {
                    
                }
            }

        }
    }


    public void setInfopopup()
    {
        //302,-79,0
        /*foreach (Transform child in infoContent.transform)
        {
            print(child.name+child.localPosition);
        }
        Scrollbar s = scroll.GetComponent<Scrollbar>();
        s.value = 1;

        Vector3 curPos = new Vector3(200, -79, 0);
       
        foreach (var item in existStud)
        {

            var newPC = Instantiate(toggleTemplate, infoContent.transform, false);
            stud_list.Add(newPC);
            newPC.name = item;
            newPC.transform.localPosition = curPos;
            foreach (Transform child in newPC.transform)
            {
                
                if(child.name=="Label")
                {
                    Text cText = child.GetComponentInChildren<Text>();
                    cText.text = item; 

                }
            
                

            }
            
            curPos -= new Vector3(0, 40, 0);

            

        }
    }*/

    public GameObject infoContent;
    public List<string> existStud = new List<string> { "A", "B", "C", "D" };
    public GameObject toggleTemplate;

    public void Start()
    {
        //302,-79,0
        foreach (Transform child in infoContent.transform)
         {
         print(child.name+child.localPosition);
         }
        //Scrollbar s = scroll.GetComponent<Scrollbar>();
        //s.value = 1;

         Vector3 curPos = new Vector3(0, -50, 0);
       

         foreach (var item in existStud)
         {

          var newPC = Instantiate(toggleTemplate, infoContent.transform, false);
          newPC.name = item;
          newPC.transform.localPosition = curPos;
         foreach (Transform child in newPC.transform)
         {

             if(child.name=="Label")
           {
            Text cText = child.GetComponentInChildren<Text>();
            cText.text = item; 

             }



           }

          curPos -= new Vector3(0, 40, 0);



          }
     }

    public void onSend()
    {
        List<string> selectedStud = new List<string>();
        foreach (Transform child in infoContent.transform)
        {
            
            if (child.name == "ChoosePlayers")
            {
                continue;
            }
            else
            {
                Toggle tog = child.GetComponent<Toggle>();
                if (tog.isOn== true)
                selectedStud.Add(child.name);
            }
        }

        foreach(string item in selectedStud)
        {
            print(item);
           // selectedStud.Add(item);
        }
        sendChallengers sendObj = new sendChallengers();
        sendObj.receivers = selectedStud.ToArray();
        sendObj.senderScore = ChallengeResultManager.getScore();
        sendObj.senderTime = ChallengeGameController.endTime - ChallengeGameController.startTime;
        //sendObj.senderTime = 10.1f;
        sendObj.challengeID = CreateChallengeScreen.challengeID;
        string json = JsonUtility.ToJson(sendObj);
        print(json);
        StartCoroutine(ServerController.Put("http://localhost:5000/student/challenge/sendChallenge", json,
             result =>
             {
                 if (result != null)
                 {
                     //need to convert into complete object to store challenge id
                     Debug.Log(result);
                     //CreateChallengeResult createchallengeResult = JsonUtility.FromJson<CreateChallengeResult>(result);
                     //challengeID = createchallengeResult.challengeID;
                     //print(challengeID);
                 }
                 else
                 {
                     Debug.Log("Error in sending!");

                 }
             }
        ));
       

    }




}
