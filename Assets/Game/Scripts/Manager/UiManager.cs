
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

        if (column != 0)
        {
            CheckCardCount();
        }
    }
    public void OnRawClick(UnityEngine.UI.Button button)
    {
        foreach (var btn in RowButtons)
        {
            btn.interactable = true;
        }
        button.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }



    public void OnColumn(int no)
    {
        column = no;
        if (row != 0)
        {
            CheckCardCount();
        }
    }
    public void OnColumnClick(UnityEngine.UI.Button button)
    {
        foreach (var btn in ColumnButtons)
        {
            btn.interactable = true;
        }
        button.interactable = false;
    }

    public void CheckCardCount()
    {
        startButton.interactable = (row * column) % 2 == 0;
    }

    public void SetScore(int score)
    {
        ScoreText.text = "Points: " + score.ToString();
    }
    public void StartBtnClick()
    {
        selectPanel.SetActive(false);
        Gamemanager.instance.gridManager.CardGenerator(row, column);
    }

    public void OnGameOver()
    {
        selectPanel.SetActive(true);
    }

}
