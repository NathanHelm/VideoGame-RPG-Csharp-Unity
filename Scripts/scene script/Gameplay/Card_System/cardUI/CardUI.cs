using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardUI : MonoBehaviour 
{
    [SerializeField]
    Card cardSelected;
    public IEnumerator selectedCoroutine = null;
    protected RectTransform rectTransform;
    protected TextMeshProUGUI text, textInfo;
    Image img;
    private Sprite icon;
    public string cardSelectedStage = "";
    Vector3 target = new Vector3(300, 70);

    public void setCard(Card c)
    {
        cardSelected = c;
        CardSelection.Instance.getCardUICounter().IncreaseCount();
        CardSelection.Instance.AddUICard(this);
        SetUpCardUI();
        SpawnMovement(); //set the card's position to the default position.
    }
    public void setBossCard(BossCard c)
    {
        cardSelected = c;
    }
    public virtual void PlayCardUI()
    {
      
    }
    public void SetUpCardUI()
    {
        img = GetComponent<Image>();
        TextMeshProUGUI[] textArray = GetComponentsInChildren<TextMeshProUGUI>();
        text = textArray[0];
        textInfo = textArray[1];
       // img.sprite = cardSelected.icon;
        icon = cardSelected.icon;
        text.text = cardSelected.cardName;
        textInfo.text = cardSelected.slogan;
        cardSelectedStage = cardSelected.getCardSelectedStage();
        rectTransform = GetComponent<RectTransform>();
    }
    public void SpawnMovement()
    {
        FindPosition();
        //descale...
        //move to card count area.
        //static cord.
    }
    public void Movement(Vector3 pos, Transform t)
    {
        StartCoroutine(StartMovement(pos, t));
    }
    public void CustomMovement()
    {
        StartCoroutine(selectedCoroutine = StartCustomMovement());
    }
    public IEnumerator SpawnMovementCoroutine()
    {
       
        while (true)
        {

            yield return new WaitForFixedUpdate();
        }
    }
    public void FindPosition()
    {
        StartCoroutine(MovementManager.Instance.FollowPlayerCoroutine(rectTransform, ispositionFound(), target, 7f));
        rectTransform.localScale *= 10;
        StartCoroutine(MovementManager.Instance.Scale(rectTransform, new Vector3(rectTransform.localScale.x/10, rectTransform.localScale.y/10, 0), 0.1f));
    }
    public bool ispositionFound()
    {
        float dist = Vector2.Distance(rectTransform.localPosition, target);
        if (dist < 1f)
        {
            Debug.Log("distance done ");
            return false;
        }
        else
        {
            return true;
        }
    }
    public void HalfAlphaColor(float multipliedby)
    {
       Debug.Log("mulitplied by" + multipliedby);
       Color c = GetComponent<Image>().color;
       float num = multipliedby;
        GetComponent<Image>().color = new Color(1, 1, 1, num);


    }
    public IEnumerator StartMovement(Vector3 pos, Transform t)
    {
        StartCoroutine(MovementManager.Instance.FollowPlayerCoroutine(rectTransform, MovementManager.Instance.ispositionFound(rectTransform, pos),pos, 10));
        yield return null;
    }
    public IEnumerator StartCustomMovement()
    {
        cardSelected.CardUIMovement(rectTransform);
        yield return null;
    }
    public Card getSelectedCard()
    {
        return cardSelected;
    }
    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }
    public Sprite getIcon()
    {
        return icon;
    }

}
