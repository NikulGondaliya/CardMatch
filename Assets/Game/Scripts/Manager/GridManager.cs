using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Card cardPrefab;
    private int raw;
    private int col;
    private Transform cardParant;


    private void Awake() => cardParant = grid.transform;
    void Start() => Gamemanager.instance.gridManager = this;
    public int GetRaw() => raw;
    public int GetCol() => col;


    public void CardGeneratorFormSavedata()
    {
        SaveData data = Gamemanager.instance.saveGame.GetData();
        if (data.cards.Count == 0)
        {
            Gamemanager.instance.uiManager.OnGameOver();
            return;
        }
        raw = data.raw;
        col = data.col;
        Gamemanager.instance.scoreManager.score = data.score;
        grid.constraintCount = col;
        var cards = new System.Collections.Generic.List<Card>();
        for (int i = 0; i < data.cards.Count; i++)
        {
            var card = Instantiate(cardPrefab, cardParant);
            card.name = i.ToString();
            card.SetWholeCard(data.cards[i]);
            cards.Add(card);
        }
        Gamemanager.instance.cardManager.SetCardList(cards);
    }


    public void CardGenerator(int row, int column)
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
        var cards = new System.Collections.Generic.List<Card>();
        for (int i = 0; i < totalobject; i++)
        {
            var card = Instantiate(cardPrefab, cardParant);
            card.name = i.ToString();
            cards.Add(card);
        }
        var cardManager = Gamemanager.instance.cardManager;
        cardManager.SetCardList(cards);
        cardManager.SetCardData();
    }
}
