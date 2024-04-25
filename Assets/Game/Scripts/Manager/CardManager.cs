
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<CardDetail> AllCardsDetail;



    public Card OpenCard;

    public List<Card> cards = new List<Card>();
    public List<CardDetail> SelectedCards = new List<CardDetail>();

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
        SaveData();
    }


    public void SaveData()
    {
        var data = new SaveData();
        data.raw = Gamemanager.instance.gridManager.raw;
        data.col = Gamemanager.instance.gridManager.col;

        for (int i = 0; i < cards.Count; i++)
        {
            data.cards.Add(cards[i].GetCardData());
        }

        Gamemanager.instance.saveGame.Save(data);

    }




    public CardDetail GetCardDetail(int type, string name)
    {
        return AllCardsDetail.Find((x) => x.name == name && x.type == (cardType)type);
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
        SetCardData(cards.ToArray());

    }


    public void SetCardData(Card[] allcards)
    {
        var list = allcards.ToList<Card>();

        foreach (var selectedcard in SelectedCards)
        {
            for (int i = 0; i < 2; i++)
            {
                int no = Random.Range(0, list.Count);
                list[no].SetCardDetail(selectedcard);
                list.RemoveAt(no);
            }

        }
        //Debug.Log("Count = " + cards.Count);
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
