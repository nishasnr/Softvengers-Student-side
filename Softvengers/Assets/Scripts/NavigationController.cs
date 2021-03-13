using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class NavigationController
{
    // ENUM FOR PROCEED TYPE
    private static int UniverseSelected = 0;
    private static int SolarSystemSelected = 0;

    private static bool isPaused = false;

    private static int playerUniverse = 1;
    private static int playerSS = 0;
    private static int playerPlanet = 0;
    ///
    /// Get multiverse dictionary
    ///

    private static Dictionary<string, List<string>> multiverse = new Dictionary<string, List<string>>()
    {
        {
            "Requirement Engineering", new List<string>()
            {
                "Requirement Elicitation", "Requirement Analysis", "Requirement Specification", "Requirement Verification"
            }
        },

        {
            "Software Design", new List<string>()
            {
                "Design Principles", "Design Process", "Design Strategies"
            }
        },

        {
            "Software Implementation", new List<string>()
            {
                "Abstraction", "Encapsulation", "Inheritance", "Polymorphism"
            }
        },
        {
            "Software Testing", new List<string>()
            {
                "Test Cases", "Black Box", "Regression Test", "Conformance Test", "Performance Test"
            }
        },
        {
            "Software Deployment", new List<string>()
            {
                "G", "H", "I"
            }
        },
        {
            "Software Maintenance", new List<string>()
            {
                "G", "H", "I"
            }
        }
    };

    public static void selectUniverse(int universeID)
    {
        // Check if universe is unlocked
        if (universeID <= playerUniverse)
        {
            UniverseSelected = universeID;
            // Load Scene

            //SceneManager.LoadScene("SolarSystemScene");
            LoadScene("SolarSystemScene");
        }

        else
        {
           Debug.Log("Locked");
        }
    }

    public static void selectSolarSystem(int solarSystemID)
    {
        if (UniverseSelected < playerUniverse)
        {
            
            SolarSystemSelected = solarSystemID;
            LoadScene("PlanetScene");
        }
        else if(UniverseSelected == playerUniverse && solarSystemID <= playerSS)
        {
           
            SolarSystemSelected = solarSystemID;
            LoadScene("PlanetScene");
        }

        else
        {
            Debug.Log("Locked");
        }
    }


    public static void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public static void DisplayUnlocked()
    {

    }

    public static void DisplayLocked()
    {

    }

    

    public static List<string> getUniverses()
    {
        return multiverse.Keys.ToList();
    }

    public static List<string> getSolarSystem()
    {
        return multiverse[multiverse.Keys.ElementAt(UniverseSelected)];
    }

    public static bool IsUniverseLocked(int universeID)
    {
        if (universeID <= playerUniverse)
            return true;
        return false;
    }

    public static bool IsSolarSystemLocked(int solarSystemID)
    {
        if (UniverseSelected < playerUniverse)
            return true;
        if (UniverseSelected == playerUniverse && solarSystemID <= playerSS)
            return true;
        return false;
    }

    public static bool IsPlanetLocked(int planetID)
    {
        if (UniverseSelected < playerUniverse)
            return true;
        if (UniverseSelected == playerUniverse && SolarSystemSelected < playerSS)
            return true;

        if (UniverseSelected == playerUniverse && SolarSystemSelected == playerSS && planetID <= playerPlanet)
            return true;
        return false;
    }


}
