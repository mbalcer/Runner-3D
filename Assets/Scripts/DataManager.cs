using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static string nickname = "";
    private static int score = 0;

    public static void SetNickname(string value)
    {
       nickname = value;
    }

    public static string GetNickname()
    {
        return nickname;
    }

    public static void SetScore(int value)
    {
        score = value;
    }
    
    public static int GetScore()
    {
        return score;
    }
}