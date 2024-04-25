using System.Collections;
using System.Collections.Generic;
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
    public List<Card> cards;

    void Start()
    {
        cardParant = grid.transform;
        grid.constraintCount = col;
        CardGenerator();
    }



    public void CardGenerator()
    {
        var totalobject = raw * col;
        for (int i = 0; i < totalobject; i++)
        {
            var card = Instantiate(cardPrefab, cardParant);

            cards.Add(card);

        }

    }
}
