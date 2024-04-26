
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

        if (OpenCard.cardDetail == card.cardDetail)
        {
            OpenCard.RemoveThisCard();
            OpenCard = null;
            card.RemoveThisCard();
            Gamemanager.instance.soundManager.MatchCard();
            Gamemanager.instance.scoreManager.IncrementScore(2);
            Gamemanager.instance.cardManager.SaveData();
            CheckgameOver();
        }
        else
        {
            OpenCard.CloseCard();
            OpenCard = null;
            card.CloseCard();
            Gamemanager.instance.soundManager.MitchMatch();
            Gamemanager.instance.cardManager.SaveData();
        }
    }


    public void SaveData()
    {
        var data = new SaveData();
        data.raw = Gamemanager.instance.gridManager.raw;
        data.col = Gamemanager.instance.gridManager.col;
        data.score = Gamemanager.instance.scoreManager.score;
        for (int i = 0; i < cards.Count; i++)
        {
            data.cards.Add(cards[i].GetCardData());
        }
        Gamemanager.instance.saveGame.Save(data);

    }


    private void CheckgameOver()
    {
        bool isover = true;
        foreach (var card in cards)
        {
            if (!card.IsHide)
                isover = false;
        }

        if (!isover)
        {
            return;
        }
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        SelectedCards.Clear();
        PlayerPrefs.DeleteAll();
        Gamemanager.instance.uiManager.OnGameOver();
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

        SaveData();
        CheckgameOver();
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
