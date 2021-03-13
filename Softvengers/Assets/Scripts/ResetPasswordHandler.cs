using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CurrentPassword;
    public GameObject NewPassword;
    public GameObject ReNewPassword;
    private string currPass;
    private string newPass;
    private string reNewPass;
    public void updatePassword()
    {
        currPass = CurrentPassword.GetComponent<InputField>().text;
        newPass= NewPassword.GetComponent<InputField>().text;
        reNewPass= ReNewPassword.GetComponent<InputField>().text;
        if(string.Compare(newPass,reNewPass)==0 && newPass!="" && currPass!="")
        {
            Debug.Log("True");
        }
        else
        {
            Debug.Log("False");
        }

    }
}
