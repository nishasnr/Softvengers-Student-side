﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Player playerData;
    public void authenticateDetails()
    {
        string name = usernameField.text;
        string password = passwordField.text;;
        Login player = new Login(name, password);

        Debug.Log(player.password);
        StartCoroutine(ServerController.Post("http://localhost:5000/student/login", player.stringify(),
            result =>
            {
                if (result != null)
                {
                    LoginResult loginResult = JsonUtility.FromJson<LoginResult>(result);

                    if (loginResult.message)
                    {
                        SecurityToken.Token = loginResult.token;

                        StartCoroutine(ServerController.Post("http://localhost:5000/student/details/getStudent", player.stringify(),
                           
                            playerDataResult =>
                            {
                                Progress progress = JsonUtility.FromJson<Progress>(playerDataResult);
                                playerData.universePogress = progress.conqueredUniverse;
                                playerData.solarSystemProgress = progress.conqueredSolarSystem;
                                playerData.planetProgress = progress.conqueredPlanet;
                                playerData.colorChoice = progress.avatar;
                                Debug.Log(playerData.universePogress);
                                Debug.Log(playerData.solarSystemProgress);
                                Debug.Log(playerData.planetProgress);

                                SceneManager.LoadScene("HomePageScene");
                            }                          
                            ));
                    }

                    else
                    {
                        Debug.Log("Invalid Credentials");
                    }
                }
            }
            ));
    }
}


public struct LoginResult
{
    public bool message;
    public string token;
}

public struct Progress
{
    public int conqueredUniverse;
    public int conqueredSolarSystem;
    public int conqueredPlanet;
    public int avatar;
}