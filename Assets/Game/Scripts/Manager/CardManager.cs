using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<CardDetail> AllCardsDetail;



    public Card OpenCard;

    public List<Card> cards;
    public List<CardDetail> SelectedCards;

    private void Start()
    {
        Gamemanager.instance.cardManager = this;
    }


    public void CompareCard(Card card)
    {
        if (OpenCard == null)
        {
            OpenCard = card;
            return;
        }

        if (OpenCard.cardDetail == card.cardDetail)
        {
            OpenCard.RemoveThisCard();
            card.RemoveThisCard();
        }
        else
        {
            OpenCard.CloseCard();
            card.CloseCard();
            OpenCard = null;
        }
    }











    public void SetCardData()
    {
        SelectedCards.Clear();

        if (cards.Count % 2 != 0) return;
        int count = cards.Count / 2;

        for (int i = 0; i < count; i++)
        {
            var card = AllCardsDetail[Random.Range(0, AllCardsDetail.Count)];

            if (SelectedCards.Find((x) => x == card) != null)
            {
                i--;
            }
            else
            {
                SelectedCards.Add(card);
            }
        }


        var allcards = cards;

        foreach (var selectedcard in SelectedCards)
        {
            for (int i = 0; i < 2; i++)
            {
                int no = Random.Range(0, allcards.Count);
                allcards[no].SetCardDetail(selectedcard);
                allcards.RemoveAt(no);
            }

        }








    }

}

[System.Serializable]
public class CardDetail
{
    public cardType type;
    public string name;
    public Sprite cardsprite;
}

public enum cardType
{
    club,
    diamond,
    heart,
    spade
}
