using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    private static float score;

    public static float GetScore()
    {
        return score;
    }

    public static void SetScore(float newScore)
    {
        score = newScore;
    }

    public static void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }

    public static void NullScore()
    {
        score = 0f;
    }
}
