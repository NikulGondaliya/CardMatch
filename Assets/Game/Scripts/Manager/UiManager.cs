using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject selectPanel;

    [SerializeField] private System.Collections.Generic.List<UnityEngine.UI.Button> RowButtons;
    [SerializeField] private System.Collections.Generic.List<UnityEngine.UI.Button> ColumnButtons;
    [SerializeField] private UnityEngine.UI.Button startButton;
    [SerializeField] private TMPro.TMP_Text ScoreText;
    [SerializeField] private int row, column = 0;

    private void Start()
    {
        Gamemanager.instance.uiManager = this;
        startButton.interactable = false;
    }
    public void OnRaw(int no)
    {
        row = no;
        Gamemanager.instance.soundManager.ClickButton();
        if (column != 0) CheckCardCount();
    }
    public void OnRawClick(UnityEngine.UI.Button button)
    {
        foreach (var btn in RowButtons) btn.interactable = true;
        button.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void OnColumn(int no)
    {
        column = no;
        Gamemanager.instance.soundManager.ClickButton();
        if (row != 0) CheckCardCount();
    }
    public void OnColumnClick(UnityEngine.UI.Button button)
    {
        foreach (var btn in ColumnButtons) btn.interactable = true;
        button.interactable = false;
    }

    public void StartBtnClick()
    {
        selectPanel.SetActive(false);
        Gamemanager.instance.soundManager.ClickButton();
        Gamemanager.instance.gridManager.CardGenerator(row, column);
    }

    public void ResetButtonClick()
    {
        Gamemanager.instance.soundManager.ClickButton();
        Gamemanager.instance.ResetGame();
    }

    public void CheckCardCount() => startButton.interactable = (row * column) % 2 == 0;
    public void SetScore(int score) => ScoreText.text = "Points: " + score.ToString();
    public void OnGameOver() => selectPanel.SetActive(true);



}
