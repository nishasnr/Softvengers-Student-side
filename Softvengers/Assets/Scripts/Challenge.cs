using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    public static string challengeID;
    public static PlayerType playerType;
}

public enum PlayerType
{
    Challenger,
    Challengee
}