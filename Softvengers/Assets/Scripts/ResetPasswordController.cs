using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordController : MonoBehaviour
{
    public GameObject CurrentPassword;
    public GameObject NewPassword;
    public GameObject ReNewPassword;
    private string currPass;
    private string newPass;
    private string reNewPass;
   
    public void updatePassword()
    {
        currPass = CurrentPassword.GetComponent<InputField>().text;
        newPass = NewPassword.GetComponent<InputField>().text;
        reNewPass = ReNewPassword.GetComponent<InputField>().text;
        Debug.Log(currPass);
        Debug.Log(newPass);
        Debug.Log(reNewPass);
        if (newPass != "" && reNewPass != "")
        {


            if (string.Compare(newPass, reNewPass) == 0)
            {
                Debug.Log("True");
            }
            else
            {
                Debug.Log("False");
            }
        }
    }
    
}
