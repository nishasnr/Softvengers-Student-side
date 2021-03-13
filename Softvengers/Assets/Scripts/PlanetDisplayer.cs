using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlanetDisplayer : MonoBehaviour
{

    public List<GameObject> planets;

    // Start is called before the first frame update
    void Start()
    {
        float x = -4.0f;
        float z = 7.0f;
        float y = -0.3f;
        List<string> difficulties = new List<string>() {
            {"Easy"},
            {"Medium"},
            {"Hard"}
        };
        for (int i=0; i<3; ++i)
        {
            GameObject planeto = Instantiate(planets[i], new Vector3(x, y, z), Quaternion.identity);
            x += 4;
            Transform canvas = planeto.transform.Find("Canvas");

            Transform button = canvas.transform.Find("Button");
            int planetID = i;
            button.GetComponent<Button>().onClick.AddListener(() => { Debug.Log("Planet Selected"); });

            ColorBlock colorVar = button.GetComponent<Button>().colors;
            if (NavigationController.IsPlanetLocked(planetID))
            {
                print("Unlocked");
                colorVar.highlightedColor = new Color(0f, 255f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(0f, 255f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(0f, 255f / 255f, 10f / 255f, 150f / 255f);

            }
            else
            {
                print("Locked");
                colorVar.highlightedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 150f / 255f);
            }
            button.GetComponent<Button>().colors = colorVar;
            Debug.Log(button.GetComponent<Button>().colors.highlightedColor);
            Transform text = button.transform.Find("Text");
            text.GetComponent<Text>().text = difficulties[planetID];
        }
    }
}
