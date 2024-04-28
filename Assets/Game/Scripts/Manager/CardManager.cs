using System;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public System.Collections.Generic.List<CardDetail> AllCardsDetail;
    public System.Collections.Generic.List<CardDetail> SelectedCards = new System.Collections.Generic.List<CardDetail>();
    public System.Collections.Generic.List<Card> cards = new System.Collections.Generic.List<Card>();
    public Card OpenCard;

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

        if (isEqualCard(OpenCard.GetThisCardDetail(), card.GetThisCardDetail()))
            MatchFound(OpenCard, card);
        else
            DontMatch(OpenCard, card);

        OpenCard = null;
    }


    public bool isEqualCard(CardDetail First, CardDetail Sec) => First.type.Equals(Sec.type) && First.no == Sec.no;

    public void MatchFound(Card First, Card Sec)
    {
        First.RemoveThisCard();
        Sec.RemoveThisCard();
        Gamemanager.instance.soundManager.MatchCard();
        Gamemanager.instance.scoreManager.IncrementScore(2);
        Gamemanager.instance.cardManager.SaveData();
        CheckgameOver();
    }

    public void DontMatch(Card First, Card Sec)
    {
        First.CloseCard();
        Sec.CloseCard();
        Gamemanager.instance.soundManager.MitchMatch();
        Gamemanager.instance.cardManager.SaveData();
    }



    public void SaveData()
    {
        var data = new SaveData();
        data.raw = Gamemanager.instance.gridManager.GetRaw();
        data.col = Gamemanager.instance.gridManager.GetCol();
        data.score = Gamemanager.instance.scoreManager.score;
        for (int i = 0; i < cards.Count; i++)
        {
            data.cards.Add(cards[i].GetCardData());
        }
        Gamemanager.instance.saveGame.Save(data);

    }


    private void CheckgameOver()
    {
        var isover = true;
        foreach (var card in cards) if (!card.GetIsCardHide()) isover = false;

        if (!isover) return;
        ResetAllCard();
    }

    public void ResetAllCard()
    {
        foreach (var card in cards) Destroy(card.gameObject);
        cards.Clear();
        SelectedCards.Clear();
        PlayerPrefs.DeleteAll();
        Gamemanager.instance.uiManager.OnGameOver();
    }




    public CardDetail GetCardDetail(string type, int no)
    {
        cardType cardType = converToEnum<cardType>(type);
        return AllCardsDetail.Find((x) => x.type == cardType && x.no == no);
    }


    public cardType converToEnum<cardType>(string enumValue)
    {
        return (cardType)Enum.Parse(typeof(cardType), enumValue);
    }


    public void SetCardData()
    {
        SelectedCards.Clear();

        if (cards.Count % 2 != 0) return;
        int count = cards.Count / 2;

        for (int i = 0; i < count; i++)
        {
            var card = AllCardsDetail[UnityEngine.Random.Range(0, AllCardsDetail.Count)];

            if (SelectedCards.Find((x) => x == card) != null)
            {
                i--;
            }
            else
            {
                SelectedCards.Add(card);
            }
        }
        SetCardDataTolist(cards.ToArray());

    }

    public void SetCardDataTolist(Card[] allcards)
    {
        var list = allcards.ToList<Card>();

        foreach (var selectedcard in SelectedCards)
        {
            for (int i = 0; i < 2; i++)
            {
                int no = UnityEngine.Random.Range(0, list.Count);
                list[no].SetCardDetail(selectedcard);
                list.RemoveAt(no);
            }
        }
        SaveData();
        CheckgameOver();
    }


}

[System.Serializable]
public class CardDetail
{
    public cardType type;
    public int no;
    public Sprite cardsprite;
}

public enum cardType
{
    Club,
    Diamond,
    Heart,
    Spade
}
