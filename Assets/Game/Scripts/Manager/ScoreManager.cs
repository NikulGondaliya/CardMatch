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
        }
    }

}
