using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int raw, col;
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private Card cardPrefab;
    private Transform cardParant;
    
    private CardManager cardManager;



    private void Awake()
    {
        cardParant = grid.transform;
        grid.constraintCount = col;
    }

    void Start()
    {
        Gamemanager.instance.gridManager = this;    
        cardManager = Gamemanager.instance.cardManager;
        CardGenerator();
    }



    public void CardGenerator()
    {
        var totalobject = raw * col;
        for (int i = 0; i < totalobject; i++)
        {
            var card = Instantiate(cardPrefab, cardParant);

            cardManager.cards.Add(card);

        }

        cardManager.SetCardData();


    }
}
