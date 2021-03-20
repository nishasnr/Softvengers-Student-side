using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateChallengeScreen : MonoBehaviour
{
    public int nextOptionsid;
    public GameObject OptionsCard;
    // Start is called before the first frame update

    public Dictionary<int, List<string>> Universe_SS_mapping = new Dictionary<int, List<string>>()
    {
        { 1, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 0, new List<string>{"SS1","SS2","SS3","SS4"} },
        { 2, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 3, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 4, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} },
        { 5, new List<string>{"Solar System 1","Solar System 2","Solar System 3","Solar System 4"} }
    };

    void Start()
    {
        nextOptionsid = 1;
        print(Universe_SS_mapping[0][0]);

    }

    public void OnAddClick()
    {
        GameObject AddButton = EventSystem.current.currentSelectedGameObject;
        AddButton.transform.localPosition -= new Vector3(0,195f,0);
        GameObject firstCard = GameObject.Find("0");
        if(nextOptionsid%4==0)
        {
            RectTransform scroll=gameObject.GetComponent<RectTransform>();
            scroll.sizeDelta += new Vector2(0, 1000);
            foreach(Transform child in gameObject.transform)
            {
                Debug.Log(child.name);
                child.localPosition+= new Vector3(0,500,0);
            }
        }
        var newOptionsCard = Instantiate(OptionsCard,gameObject.transform,false);
        newOptionsCard.transform.localPosition = new Vector3(firstCard.transform.localPosition.x,AddButton.transform.localPosition.y+20f, 0);
        newOptionsCard.name = nextOptionsid.ToString();
        Button btn = newOptionsCard.GetComponentInChildren<Button>();
        btn.onClick.AddListener(OnRemoveClick);
        Dropdown UniDD = newOptionsCard.transform.Find("Universe Dropdown").gameObject.GetComponent<Dropdown>();
        UniDD.onValueChanged.AddListener(delegate {
            changeSSOptions(UniDD);
        });
        nextOptionsid += 1;

    }

    public void OnRemoveClick()
    {
        GameObject RemoveButton = EventSystem.current.currentSelectedGameObject;
        GameObject toRemove = RemoveButton.transform.parent.gameObject;
        int ind= int.Parse(toRemove.transform.name);
        Destroy(toRemove);
        foreach (Transform child in gameObject.transform)
        {
            if (child.name=="AddButton")
            {
                child.localPosition += new Vector3(0,195,0);
            }
            else if(int.Parse(child.name)>ind)
            {
                child.localPosition += new Vector3(0, 175, 0);
            }
        }
    }

    public void changeSSOptions(Dropdown change)
    {
        GameObject currentDropdown = change.gameObject;
        GameObject currentCard = currentDropdown.transform.parent.gameObject;
        GameObject SSObject = currentCard.transform.Find("Solar System Dropdown").gameObject;
        Dropdown SSDropdown = SSObject.GetComponent<Dropdown>();
        SSDropdown.ClearOptions();
        
        foreach (string SS in Universe_SS_mapping[change.value])
        {
            Dropdown.OptionData NewData = new Dropdown.OptionData();
            NewData.text = SS;
            SSDropdown.options.Add(NewData);
        }

        SSDropdown.value = 0;
        SSDropdown.captionText.text = Universe_SS_mapping[change.value][0];




    }

    public void OnSubmitClick()
    {
        List <List<string>> submitInfo= new List<List<string>>();
        string[] attri = new string[4] { "Universe Dropdown", "Solar System Dropdown", "Qnum", "Dl" };
                                           
        foreach (Transform child in transform)
        {
            if (child.name=="AddButton")
            {
                continue;
            }
            List<string> tempList = new List<string>();
            for (int i = 0;i< 4;i++)
            {

                GameObject DropDownObj = child.Find(attri[i]).gameObject;
                Dropdown DropdownComp = DropDownObj.GetComponent<Dropdown>();
                tempList.Add(DropdownComp.captionText.text);
            }

            submitInfo.Add(tempList);

        }

        foreach (List<string> subList in submitInfo)
        {
            foreach(string val in subList)
            {
               print(val);
            }
        }
    }
}
