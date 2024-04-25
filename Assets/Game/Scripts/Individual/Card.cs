using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite backSprite;
    public Sprite frontSprite;

    private Image Image;
    private float cardfliptime = .5f;
    public bool isOpen = false;
    public bool IsHide = false;
    public bool isClick = false;
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
        details.isopen = isOpen;
        details.ishide = IsHide;
        return details;
    }


    public void SetWholeCard(savecardDetail card)
    {
        IsHide = card.ishide;
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
            Debug.Log("this is open " + name);
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

    public void CloseCard()
    {
        isOpen = false;
        StartCoroutine(CloseThisCard());
    }


    private IEnumerator CloseThisCard()
    {

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
        //Image.color = new Color32(1, 1, 1, 0);
        StartCoroutine(RemoveCard());
    }
    private IEnumerator RemoveCard()
    {
        var c = cardfliptime / 2;
        float timer = 0f;
        while (timer < c)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / c);
            CanvasGroup.alpha = alpha;
            yield return null;
        }
        CanvasGroup.alpha = 0f;

        CanvasGroup.blocksRaycasts = false;
        IsHide = true;

    }








}
