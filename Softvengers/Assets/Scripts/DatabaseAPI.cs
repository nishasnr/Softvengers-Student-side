using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class DatabaseAPI : MonoBehaviour
{

    public GameObject username;
    public GameObject password;


    public void checkUser(){

        string Name = username.GetComponent<Text>().text;
        string pass = password.GetComponent<Text>().text;
        Debug.Log(Name);
        Debug.Log(pass);
        Login player = new Login(Name, pass);
        StartCoroutine(Post("http://localhost:3000/auth/login", player));
    }


    public IEnumerator Post(string url, Login player)
    {
        var jsonData = player.stringify();
        Debug.Log(jsonData);

        using(UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
                {
                Debug.Log(www.error);
                }
            else
            {
            if (www.isDone)
            {
                // handle the result
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                Debug.Log(result);
            }
            else
            {
                //handle the problem
                Debug.Log("Error! data couldn't get.");
            }
            }
        }
    }
}
