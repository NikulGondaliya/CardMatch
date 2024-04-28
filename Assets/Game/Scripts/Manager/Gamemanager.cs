
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public UiManager uiManager;
    public GridManager gridManager;
    public CardManager cardManager;
    public SoundManager soundManager;
    public ScoreManager scoreManager;
    public SaveGame saveGame;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        if (saveGame.isLastDataAvailable())
        {
            uiManager.selectPanel.SetActive(false);
            gridManager.CardGeneratorFormSavedata();
        }
    }
    public void StartGame() => gridManager.CardGenerator();
    public void ResetGame() => cardManager.ResetAllCard();
}
