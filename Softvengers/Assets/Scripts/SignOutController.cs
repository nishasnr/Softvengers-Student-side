using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignOutController : MonoBehaviour
{
    public void SignOut()
    {
        SecurityToken.Email = "";
        SecurityToken.Token = "";
        SceneManager.LoadScene("LoginUI");
    }
}
