using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private System.Collections.Generic.List<CardDetail> AllCardsDetail;
    [SerializeField] private System.Collections.Generic.List<CardDetail> SelectedCards = new System.Collections.Generic.List<CardDetail>();
    [SerializeField] private System.Collections.Generic.List<Card> cards = new System.Collections.Generic.List<Card>();
    [SerializeField] private Card OpenCard;
    [SerializeField] private Sprite CardBackGround;


    private void Start() => Gamemanager.instance.cardManager = this;
    public void SetCardList(System.Collections.Generic.List<Card> cards) => this.cards = cards;
    public Sprite GetBackgroundSprite() => CardBackGround;
    public bool isEqualCard(CardDetail First, CardDetail Sec) => First.type.Equals(Sec.type) && First.no == Sec.no;
    public cardType converToEnum<cardType>(string enumValue) => (cardType)System.Enum.Parse(typeof(cardType), enumValue);

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
        var cardlist = new System.Collections.Generic.List<savecardDetail>();
        for (int i = 0; i < cards.Count; i++)
        {
            cardlist.Add(cards[i].GetCardData());
        }
        Gamemanager.instance.saveGame.Save(new SaveData(Gamemanager.instance.gridManager.GetRaw(), Gamemanager.instance.gridManager.GetCol(), Gamemanager.instance.scoreManager.score, cardlist));
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
        var cardType = converToEnum<cardType>(type);
        return AllCardsDetail.Find((x) => x.type == cardType && x.no == no);
    }
    public void SetCardData()
    {
        SelectedCards.Clear();

        if (cards.Count % 2 != 0) return;
        int count = cards.Count / 2;
        for (int i = 0; i < count; i++)
        {
            var card = AllCardsDetail[UnityEngine.Random.Range(0, AllCardsDetail.Count)];
            if (SelectedCards.Find((x) => x == card) != null) i--;
            else SelectedCards.Add(card);
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
