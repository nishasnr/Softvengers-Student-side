using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UniverseDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private float radius = 65.0f;
    public GameObject universe;
    void Start()
    {
        List<string> universes = NavigationController.getUniverses();
        int numUniverse = universes.Count();

        float increment = (float)(360.0 / numUniverse);
        int universeID = 0;
        for (float angle = 0.0f; angle < 360.0; angle += increment)
        {
            float x = radius * Mathf.Sin(0.01745f * angle);
            float z = radius * Mathf.Cos(0.01745f * angle);
            GameObject galaxy = Instantiate(universe, new Vector3(x, -5f, z), Quaternion.identity);
            Transform canvas = galaxy.transform.Find("Canvas");
            Transform button = canvas.transform.Find("Button");
            int currentUniverse = universeID;
            button.GetComponent<Button>().onClick.AddListener(() => { NavigationController.selectUniverse(currentUniverse); });
            ColorBlock colorVar = button.GetComponent<Button>().colors;
            if (NavigationController.IsUniverseLocked(universeID))
            {
                colorVar.highlightedColor = new Color(0f, 255f/255f, 10f/255f, 50f/255f);
                colorVar.selectedColor = new Color(0f, 255f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(0f, 255f / 255f, 10f / 255f, 150f / 255f);

            }
            else
            {
                colorVar.highlightedColor = new Color(255f/255f, 0f / 255f, 10f / 255f, 50f / 255f);
                colorVar.selectedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 0f / 255f);
                colorVar.pressedColor = new Color(255f / 255f, 0f / 255f, 10f / 255f, 150f / 255f);
            }
            button.GetComponent<Button>().colors = colorVar;
            Transform text = button.transform.Find("Text");
            text.GetComponent<Text>().text = universes[universeID];
            universeID++;
        }
    }

    public static void OnCancel()
    {

    }

    public static void OnProceed()
    {

    }

}
