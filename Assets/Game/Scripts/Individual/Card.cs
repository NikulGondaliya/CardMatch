using System;
using System.Collections;
using UnityEngine;


public class Card : MonoBehaviour
{
    private Sprite backSprite;
    private Sprite frontSprite;
    private UnityEngine.UI.Image Image;
    private float cardfliptime = .5f;
    private bool isOpen = false;
    private bool IsHide = false;
    private bool isClick = false;
    private CardDetail cardDetail;
    [HideInInspector] public Transform transform;
    private CanvasGroup CanvasGroup;
    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        transform = GetComponent<Transform>();
        Image = GetComponent<UnityEngine.UI.Image>();
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => this.OnClick());
    }

    public bool GetIsCardHide() => IsHide;
    public bool GetIsCardOpen() => isOpen;
    public CardDetail GetThisCardDetail() => cardDetail;


    public void SetCardDetail(CardDetail cardDetail)
    {
        this.cardDetail = cardDetail;
        frontSprite = cardDetail.cardsprite;
    }
    public savecardDetail GetCardData()
    {
        return new savecardDetail(Enum.GetName(typeof(cardType), cardDetail.type), cardDetail.no, isOpen, IsHide);
    }

    public void SetWholeCard(savecardDetail card)
    {
        IsHide = card.ishide;
        isOpen = card.isopen;
        cardDetail = Gamemanager.instance.cardManager.GetCardDetail(card.type, card.no);
        frontSprite = cardDetail.cardsprite;
        if (IsHide) RemoveThisCard();
        else if (isOpen) StartCoroutine(OpenCard());
    }


    private void Start()
    {
        backSprite = Gamemanager.instance.cardManager.GetBackgroundSprite();
        Image.sprite = backSprite;
    }

    public void OnClick()
    {
        if (isClick) return;
        else isClick = true;
        if (!isOpen) StartCoroutine(OpenCard());
    }

    private IEnumerator OpenCard()
    {
        Gamemanager.instance.soundManager.FlipCard();
        float timer = 0f;
        while (timer < cardfliptime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 90f, timer / cardfliptime);
            transform.rotation = Quaternion.Euler(0, alpha, 0);
            yield return null;
        }
        Image.sprite = frontSprite;
        timer = 0f;
        while (timer < cardfliptime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(90, 0f, timer / cardfliptime);
            transform.rotation = Quaternion.Euler(0, alpha, 0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isOpen = true;
        isClick = false;
        Gamemanager.instance.cardManager.CompareCard(this);
    }

    public void CloseCard() => StartCoroutine(CloseThisCard());
    private IEnumerator CloseThisCard()
    {
        isOpen = false;
        float timer = 0f;
        while (timer < cardfliptime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 90f, timer / cardfliptime);
            transform.rotation = Quaternion.Euler(0, alpha, 0);
            yield return null;
        }
        Image.sprite = backSprite;
        timer = 0f;
        while (timer < cardfliptime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(90, 0f, timer / cardfliptime);
            transform.rotation = Quaternion.Euler(0, alpha, 0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        isOpen = false;
        isClick = false;
    }



    public void RemoveThisCard()
    {
        CanvasGroup.blocksRaycasts = false;
        IsHide = true;
        StartCoroutine(RemoveCard());
    }
    private IEnumerator RemoveCard()
    {
        var time = cardfliptime / 2;
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / time);
            CanvasGroup.alpha = alpha;
            yield return null;
        }
        CanvasGroup.alpha = 0f;
        CanvasGroup.blocksRaycasts = false;
        IsHide = true;
    }
}

