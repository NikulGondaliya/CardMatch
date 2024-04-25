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
    private bool isHide = true;
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


    private void Start()
    {
        Image.sprite = backSprite;
    }

    public void OnClick()
    {
        if (isClick) return;

        isClick = true;

        if (isHide)
            StartCoroutine(OpenCard());
        //else
        //    StartCoroutine(CloseCard());

    }

    private IEnumerator OpenCard()
    {
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
        isHide = false;
        isClick = false;
    }

    public void CloseCard()
    {
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
        isHide = true;
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
        while(a> -.1f)
        { 
            yield return new WaitForSeconds(cardfliptime / 10f);
            CanvasGroup.alpha = a;
            a -= .1f;
        }
    }



}
