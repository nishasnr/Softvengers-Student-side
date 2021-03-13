using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySolarSystem : MonoBehaviour
{

    private float radius = 40.0f;
    public GameObject solarSystem;
    // Start is called before the first frame update
    void Start()
    {

        List<string> solarSystems = NavigationController.getSolarSystem();
        int numSolarSystem = solarSystems.Count();

        float increment = (float)(360.0 / numSolarSystem);
        int solarSystemID = 0;
        for (float angle = 0.0f; angle < 360.0; angle += increment)
        {
            float x = radius * Mathf.Sin(0.01745f * angle);
            float z = radius * Mathf.Cos(0.01745f * angle);
            GameObject galaxy = Instantiate(solarSystem, new Vector3(x, -4, z), Quaternion.identity);
            Transform canvas = galaxy.transform.Find("Canvas");
            Transform button = canvas.transform.Find("Button");
            int currentsolarSystem = solarSystemID;
            
            button.GetComponent<Button>().onClick.AddListener(() => { NavigationController.selectSolarSystem(currentsolarSystem); });

            ColorBlock colorVar = button.GetComponent<Button>().colors;
            if (NavigationController.IsSolarSystemLocked(currentsolarSystem))
            {
                colorVar.highlightedColor = new Color(0f, 255f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(0f, 255f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(0f, 255f / 255f, 10f / 255f, 150f / 255f);

            }
            else
            {
                colorVar.highlightedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 150f / 255f);
            }
            button.GetComponent<Button>().colors = colorVar;
            Transform text = button.transform.Find("Text");
            text.GetComponent<Text>().text = solarSystems[solarSystemID];
            solarSystemID++;
        }
    }

    public void Back()
    {
        NavigationController.LoadScene("UniverseScene");
    }

}
