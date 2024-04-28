using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public int avgFrameRate;
    public TMPro.TMP_Text display_Text;

    private void Start() => Application.targetFrameRate = 60;

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        display_Text.text = "FPS : " + avgFrameRate.ToString();
    }
}