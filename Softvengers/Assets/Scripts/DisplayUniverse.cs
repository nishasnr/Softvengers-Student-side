using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUniverse : MonoBehaviour
{
    public GameObject universe;
    public int numUniverse = 6;
    public float radius = 50.0f;

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
        
        float increment = (float)(360.0 / numUniverse);

        for (float angle=0.0f; angle<360.0; angle += increment)
        {
            float x = radius * Mathf.Sin(0.01745f * angle);
            float z = radius * Mathf.Cos(0.01745f * angle);
            Instantiate(universe, new Vector3(x, 0, z), Quaternion.identity);
        }
    }
}
