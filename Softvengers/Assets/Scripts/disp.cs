using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;

public class DisplayUniverse : MonoBehaviour
{
    public GameObject universe;
    public GameObject ss;

    private Dictionary<string, List<string>> multiverse = new Dictionary<string, List<string>>()
    {
        {
            "Uni1", new List<string>()
            {
                "A", "B", "C"
            }
        },

        {
            "Uni2", new List<string>()
            {
                "D", "E", "F"
            }
        },

        {
            "Uni3", new List<string>()
            {
                "G", "H", "I"
            }
        }
    };


    
    public float radius = 50.0f;

    int currentUni = -1;
    int currentSS = -1;
    int currentP = -1;
    

    /**
     * Displays the appropriate number of universes in the game
     * 
     * Formula is based on:
     *      x = r * sin(t)
     *      z = r * cos(t)
     *      
     *      Note: t should be in radians
     *      1 degree = 0.01745 radians
     */
    void Start()
    {
        int numUniverse = multiverse.Count();
        float increment = (float)(360.0 / numUniverse);
        int i = 0;
        for (float angle=0.0f; angle<360.0; angle += increment)
        {
            float x = radius * Mathf.Sin(0.01745f * angle);
            float z = radius * Mathf.Cos(0.01745f * angle);
            GameObject galaxy = Instantiate(universe, new Vector3(x, 0, z), Quaternion.identity);
            Transform canvas = galaxy.transform.Find("Canvas");
            Transform button = canvas.transform.Find("Button");
            int j = i;
            button.GetComponent<Button>().onClick.AddListener(() => { changeScene(j); });
            Transform text = button.transform.Find("Text");
            text.GetComponent<Text>().text = i.ToString();
            i++;
        }
    }

    public void changeScene(int universeId)
    {
        StartCoroutine("LoadSolarSystemScene", multiverse[multiverse.Keys.ElementAt(universeId)]);
        //SceneManager.LoadScene("SolarSystemScene");
        //CoroutineAction();
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SolarSystemScene"));
        //List<string> solarSystems = multiverse[multiverse.Keys.ElementAt(universeId)];
        
        
        
    }
    public IEnumerator CoroutineAction()
    {
        yield return new WaitForSeconds(2);                        // do some actions after 5 frames
    }

    private object StartCoroutine(object p)
    {
        throw new NotImplementedException();
    }

    public void ChangeToSolarSystem(int universeId)
    {

    }

    public void ChangeToUniverseScene()
    {
        
        //LoadUniverse();
    }

    public void ChangeToPlanet()
    {

    }


    void LoadUniverse()
    {
        
    }

    IEnumerator LoadSolarSystemScene(List<string> solarSystems)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SolarSystemScene", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene);
        int numSolarSystem = solarSystems.Count();
        float increment = (float)(360.0 / numSolarSystem);
        int i = 0;
        for (float angle = 0.0f; angle < 360.0; angle += increment)
        {
            float x = (radius - 10) * Mathf.Sin(0.01745f * angle);
            float z = (radius - 10) * Mathf.Cos(0.01745f * angle);
            GameObject solarsys = Instantiate(ss, new Vector3(x, 0, z), Quaternion.identity);
            SceneManager.MoveGameObjectToScene(solarsys, SceneManager.GetSceneByName("SolarSystemScene"));

            Transform canvas = solarsys.transform.Find("Canvas");
            
            Transform button = canvas.transform.Find("Button");
       
            int j = i;
            //button.GetComponent<Button>().onClick.AddListener(() => { changeScene(j); });
            Transform text = button.transform.Find("Text");


            text.GetComponent<Text>().text = solarSystems[i];

            i++;
        }
    }
}
