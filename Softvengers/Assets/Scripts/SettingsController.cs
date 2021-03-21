using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public Player playerData;

    public void changeAvatar(int avatarID)
    {
        playerData.colorChoice = avatarID;
    }

    public void changeSound()
    {

    }
}
