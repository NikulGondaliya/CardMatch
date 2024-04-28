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
    public void IncrementScore(int increment) => score += increment;
    public void DecripmentScore(int decripment) => score -= decripment;
}
