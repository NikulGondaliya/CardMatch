using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int Score;

    public int score
    {
        get { return Score; }
        set
        {
            Score = value;
            Gamemanager.instance.uiManager.SetScore(Score);
        }
    }


    public void IncrementScore(int increment)
    {
        Score += increment;
    }

    public void DecripmentScore(int decripment)
    {
        Score -= decripment;
    }

}
