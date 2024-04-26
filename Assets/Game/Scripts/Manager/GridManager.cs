using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    public int raw, col;
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private Card cardPrefab;
    private Transform cardParant;

    private CardManager cardManager;



    private void Awake()
    {
        cardParant = grid.transform;
    }

    void Start()
    {
        Gamemanager.instance.gridManager = this;
        cardManager = Gamemanager.instance.cardManager;
    }
    public void CardGeneratorFormSavedata()
    {
        cardManager = Gamemanager.instance.cardManager;
        SaveData data = Gamemanager.instance.saveGame.GetData();
        raw = data.raw;
        col = data.col;
        Gamemanager.instance.scoreManager.score = data.score;
        grid.constraintCount = col;

        if (data.cards.Count == 0)
        {
            Gamemanager.instance.uiManager.OnGameOver();
            return;
        }

        for (int i = 0; i < data.cards.Count; i++)
        {
            var card = Instantiate(cardPrefab, cardParant);
            card.name = i.ToString();
            card.SetWholeCard(data.cards[i]);
            cardManager.cards.Add(card);
        }

        cardManager.SetCardData();
    }


    public void CardGenerator(int row,int column)
    {
        raw = row;
        col = column;
        Gamemanager.instance.scoreManager.score = 0;
        CardGenerator();
    }

    public void CardGenerator()
    {
        var totalobject = raw * col;
        grid.constraintCount = col;
        for (int i = 0; i < totalobject; i++)
        {
            var card = Instantiate(cardPrefab, cardParant);
            card.name = i.ToString();
            cardManager.cards.Add(card);

        }
        cardManager.SetCardData();
    }
}
