using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public Player playerData;
    public Slider slider;
    private float soundLevel;

    public void ChangeAvatar(int avatarID)
    {
        playerData.colorChoice = avatarID;
    }

    public void ChangeSound()
    {
        this.soundLevel = slider.GetComponent<Slider>().value;
        Debug.Log(this.soundLevel);
    }
}
