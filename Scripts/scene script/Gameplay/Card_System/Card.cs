using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    
    //consider this, this card should have two indications. One -> it should have data that is easily transferable
    //is has access to interfaces applied to all cards.
    //private IGameSetting[] gameSetting; -> should I add this to every card script, feels unessicary and unopimized...
    public string path;
    public string cardName;
    public string slogan;
    public Sprite icon;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshProName;
    protected Rigidbody2D rb;
    protected string cardSelectionStage;

    public void SetUpCard(string path,string cardName, string slogan, string cardSelectionStage)
    {
        //dont need to get the filename of the current gameplay_stage.
       this.path = path;
       this.cardName = cardName;
       this.slogan = slogan;
       this.cardSelectionStage = cardSelectionStage;
        textMeshProName = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //icon = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 3;
        rb.angularDrag = 3;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
         //sets the text
        textMeshProName.text = cardName;

    }

    public Card SpawnCard(Vector2 pos)
    {
        Debug.Log("card file name " + path);
        GameObject g = Resources.Load<GameObject>(path);
        GameObject spawnCard = Instantiate(g);
        spawnCard.transform.position = pos;
        return spawnCard.GetComponent<Card>();
    }
    public CardUI SpawnCardUI()
    {
        GameObject gUI = Resources.Load<GameObject>("Prefab/UI/Card/cardUI");
        UIScript uIScript = FindObjectOfType<UIScript>().GetComponent<UIScript>();
        CardUI cardUI = Instantiate(gUI,uIScript.transform).GetComponent<CardUI>();
        cardUI.transform.position = new Vector3(100, 0, 0);
        /*
        Card addedComp = cardUI.gameObject.AddComponent<Card>();
        addedComp = this;
        */
        return cardUI;
    }
    


    public virtual void CardMovement()
    {
        Debug.LogError("card movement not added.");
    }
    public virtual void CardUIMovement(RectTransform rect)
    {
        //has the ability to be specialized.
    }
    public virtual IEnumerator CardUIAttack()
    {
        Debug.LogError("not iherited card attack system");
        yield return null;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("captured the card!");
            AddToCardUI(); //spawn ui card and adds specialized data to it.
        }
    }
    public void AddToCardUI()
    {
        SpawnCardUI().setCard(this);
        gameObject.SetActive(false);
    }
    public string getPath()
    {
        return path;
    }
    public void setPath(string path)
    {
        this.path = path;
    }
    public string getCardSelectedStage()
    {
        return cardSelectionStage;
    }
    //
}
