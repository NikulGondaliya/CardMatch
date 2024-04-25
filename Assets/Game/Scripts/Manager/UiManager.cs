using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject selectPanel;
    //public GameObject gameOverPanel;
    
    
    public List<Button> RowButtons;
    public List<Button> ColumnButtons;
    public Button startButton;
    public TMPro.TMP_Text ScoreText;
    public int row, column = 0;

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
    public void OnRawClick(Button button)
    {
        foreach (var btn in RowButtons)
        {
            btn.interactable = true;
        }
        button.interactable = false;
    }

    public void OnColumn(int no)
    {
        column = no;
        if (row != 0)
        {
            CheckCardCount();
        }
    }
    public void OnColumnClick(Button button)
    {
        foreach (var btn in ColumnButtons)
        {
            btn.interactable = true;
        }
        button.interactable = false;
    }

    public void CheckCardCount()
    {
        Debug.Log("Raw = " + row + "   " + column);
        if ((row * column) % 2 == 0)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void SetScore(int score)
    {
        ScoreText.text = score.ToString();
    }
    public void StartBtnClick()
    {
        selectPanel.SetActive(false);
        Debug.Log("Click");
        Gamemanager.instance.gridManager.CardGenerator(row,column);
    }

    public void OnGameOver()
    {
        selectPanel.SetActive(true);
    }

}
