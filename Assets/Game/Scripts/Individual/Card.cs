using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite backSprite;
    public Sprite frontSprite;

    private Image Image;
    private float cardfliptime;
    private bool isOpen = false;
    private bool IsHide = false;
    private bool isClick = false;
    public CardDetail cardDetail;
    private Transform transform;
    private CanvasGroup CanvasGroup;
    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        transform = GetComponent<Transform>();
        Image = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(() => this.OnClick());

    }

    public void SetCardDetail(CardDetail cardDetail)
    {
        this.cardDetail = cardDetail;
        frontSprite = cardDetail.cardsprite;
    }


    public savecardDetail GetCardData()
    {
        savecardDetail details = new savecardDetail();
        details.type = (int)cardDetail.type;
        details.name = cardDetail.name;
        details.isclick = isClick;
        details.isopen = isOpen;
        details.ishide = IsHide;
        return details;
    }


    public void SetWholeCard(savecardDetail card)
    {
        IsHide = card.ishide;
        isClick = card.isclick;
        isOpen = card.isopen;


        cardDetail = Gamemanager.instance.cardManager.GetCardDetail(card.type, card.name);
        frontSprite = cardDetail.cardsprite;

        if (IsHide)
        {
            RemoveThisCard();
            return;
        }

        if (isOpen)
        {
            Gamemanager.instance.cardManager.OpenCard = this;
            StartCoroutine(OpenCard());

        }


    }


    private void Start()
    {
        Image.sprite = backSprite;
    }

    public void OnClick()
    {
        if (isClick) return;

        isClick = true;

        if (!isOpen)
            StartCoroutine(OpenCard());

    }

    private IEnumerator OpenCard()
    {
        Gamemanager.instance.cardManager.SaveData();
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(cardfliptime / 90);
            transform.rotation = Quaternion.Euler(0, i, 0);
        }
        Image.sprite = frontSprite;
        for (int i = 90; i >= 0; i--)
        {
            yield return new WaitForSeconds(cardfliptime / 90);
            transform.rotation = Quaternion.Euler(0, i, 0);
        }
        Gamemanager.instance.cardManager.CompareCard(this);
        isOpen = true;
        isClick = false;
    }

    public void CloseCard()
    {
        isOpen = false;
        StartCoroutine(CloseThisCard());
    }


    private IEnumerator CloseThisCard()
    {
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(cardfliptime / 90);
            transform.rotation = Quaternion.Euler(0, i, 0);
        }
        Debug.Log("BackSprite");
        Image.sprite = backSprite;
        for (int i = 90; i >= 0; i--)
        {
            yield return new WaitForSeconds(cardfliptime / 90);
            transform.rotation = Quaternion.Euler(0, i, 0);
        }

        isClick = false;
    }


    public void RemoveThisCard()
    {
        //Image.color = new Color32(1, 1, 1, 0);
        StartCoroutine(RemoveCard());
    }
    private IEnumerator RemoveCard()
    {
        float a = 1;
        while (a > -.1f)
        {
            yield return new WaitForSeconds(cardfliptime / 10f);
            CanvasGroup.alpha = a;
            a -= .1f;
        }
        CanvasGroup.blocksRaycasts = false;
        IsHide = true;

    }








}
