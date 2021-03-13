using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MultiverseProgSetup : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject Badge;
    public int numBar = ProgressController.numUniverse;
    public int screenWidth = 640;
    public int headingWidth = 30;

    // Start is called before the first frame update
    void Start()
    {
       
        int decrementFactor = ((screenWidth - 4* headingWidth) / numBar);
        int startPos = screenWidth / 2 - 4* headingWidth;
        Vector3 curpos = new Vector3(0,startPos,0);
        Dictionary<int, string> badge_img = new Dictionary<int, string>()
        {
            {0,"mind_stone" },
            {1,"power_stone"},
            {2,"reality_stone"},
            {3,"soul_stone"},
            {4,"space_stone"},
            {5,"time_stone"}
        };

        Dictionary<int, Dictionary<string, string>> Multiverse_prog_info = ProgressController.Multiverse_prog_info;

        for (int x = 0; x < numBar; x++)
        {
            var newBar = Instantiate(progressBar, curpos, Quaternion.identity);
            newBar.transform.SetParent(gameObject.transform,false);
            newBar.name = x.ToString();
            Button btn = newBar.GetComponent<Button>();
            btn.onClick.AddListener(MultiverseBarClick);

            Text text = newBar.GetComponentInChildren<Text>();
            text.text = Multiverse_prog_info[x]["name"]+":    " + ((float.Parse(Multiverse_prog_info[x]["value"])) * 100).ToString() + " %"; ;

            Slider slider = btn.GetComponentInChildren<Slider>();
            slider.value = float.Parse(Multiverse_prog_info[x]["value"]);

            if (slider.value == 1)
            {
                var newBadge = Instantiate(Badge, curpos + new Vector3(550, 0, 0), new Quaternion(0, 0, 90, 1));

                var sprite = Resources.Load<Sprite>("Images/" + badge_img[x]);
                Image image = newBadge.GetComponent<Image>();
                image.sprite = sprite;


                newBadge.transform.SetParent(gameObject.transform, false);
                var rot = newBadge.GetComponent<RectTransform>();
                rot.localRotation = new Quaternion(0, 0, Mathf.Sin(0.01745f * 90), 1);

            }


            if (slider.value < 1)
            {

                if (slider.value > 0.4)
                {
                    Image sliderfill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponentInChildren<Image>();
                    sliderfill.color = calcColor(float.Parse(Multiverse_prog_info[x]["value"]));
                }

                else
                {
                    Image sliderfill = slider.transform.Find("Fill Area").transform.Find("Fill").GetComponentInChildren<Image>();
                    sliderfill.color = calcColor(float.Parse(Multiverse_prog_info[x]["value"]));
                }
            }

            curpos -= new Vector3(0, decrementFactor, 0);

        }

         
        Debug.Log("Created");
    }

    void MultiverseBarClick()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        ProgressController.selectedUniverse = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene("UniverseProgress");
    }


    Gradient createGrad()
    {
        Gradient gradient;
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color(0.61f, 0.92f, 0.72f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(0.18f, 0.89f, 0.10f);
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        return gradient;

    }

    Color calcColor(float val)
    {
        Gradient gradient = createGrad();

        // What's the color at the relative time 0.25 (25 %) ?
        return gradient.Evaluate(val);


    }

}
