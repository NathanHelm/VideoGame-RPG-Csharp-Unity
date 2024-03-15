using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection : Controls<CardUI>, IAct
{
    Skip_CardUI skipButton;
    List<CardUI> cardUIs = new List<CardUI>();
    private List<CardUI> bossCardUIs = new List<CardUI>();
    private List<BossCard> bossCards = new List<BossCard>();
    Stack<CardSelectionStage> cardSelectionStages = new Stack<CardSelectionStage>();
    Stack<Card> storedCards = new Stack<Card>();
    Dictionary<string,CardSelectionStage> nameToStage = new Dictionary<string,CardSelectionStage>();
    public static CardSelection Instance;
    private int inputAmount = 0;
    int originalCardIndexLength;
    IEnumerator cardSelectCoroutine;
    //card count
    
    UIScript uIScript;
    CardUICounter cardUICounter;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<CardSelection>().GetComponent<CardSelection>();
        }
        uIScript = FindObjectOfType<UIScript>().GetComponent<UIScript>();
        cardUICounter = FindObjectOfType<CardUICounter>().GetComponent<CardUICounter>();
    }


    public void AddUICard(CardUI c)
    {
        cardUIs.Add(c);
    }

    
    private void AddStages()
    {
        //lowercase
        nameToStage.TryAdd("enemy", FindObjectOfType<EnemyCardSelectionStage>());
        nameToStage.TryAdd("stage", FindObjectOfType<StageCardSelectionStage>());
        nameToStage.TryAdd("item", FindObjectOfType<ItemCardSelectionStage>());
        nameToStage.TryAdd("boss", FindObjectOfType<BossCardSelectionStage>());
    }
    private void AddBossCards()
    {
        //customly gets the gets the bosscards in the scene then adds to a card UI
        bossCards = CardManager.Instance.getCurBossCard();
        for(int i = 0; i < bossCards.Count; i ++)
        {
            bossCardUIs.Add(bossCards[i].SpawnCardUI());
        }
        
    }
    private void SpawnSkipButton()
    {
        GameObject g = Resources.Load<GameObject>("Prefab/UI/Card/skip");
        skipButton = Instantiate(g,uIScript.transform).GetComponent<Skip_CardUI>();
        skipButton.SetUpSkipCardUI();
    }
    private void SetUpCardBossCardUI()
    {
        //sets up spawned UI cards.
        for (int i = 0; i < bossCardUIs.Count; i++)
        {
            bossCardUIs[i].setBossCard(bossCards[i]);
            bossCardUIs[i].SetUpCardUI();
        }
    }
    private void CardSelect()
    {
        AddBossCards();
        Debug.Log("cards selection method is going");
        inputAmount = 0;
        AddStages();
        originalCardIndexLength = cardUIs.Count;
        SpawnSkipButton();
        cardUIs.Add(skipButton);
        StartCoroutine(CardSelectionCoroutine());
       
    }
    public IEnumerator CardSelectionCoroutine()
    {
        //all cards form into a line [upscaling]
        float width = -200;
        for(int i = 0; i < cardUIs.Count; i++)
        {
           Debug.Log("card ui names" + cardUIs[i].gameObject.name);
          cardUIs[i].Movement(new Vector3(width, -300 ,0),transform);
          width += (float)(cardUIs[i].GetRectTransform().rect.width + 2);
            Debug.Log("width " + width);
        }
        
        yield return new WaitUntil(()=> cardUIs[cardUIs.Count - 1].ispositionFound());
        //once all cards are set, perform custom movement...
        StartCoroutine(cardSelectCoroutine = selectOptionsCoroutine(cardUIs, cardUIs.Count - 1));
        yield return null;
    }
    public IEnumerator BossCardSelection()
    {
        while(cardUIs.Count > 0)
        {
            RemoveAndDestroyUICard(cardUIs,cardUIs[0]);
        }
       
        SetUpCardBossCardUI();
        GetCardSelectionStage("boss").LookAtStage();
        float width = -200;
        //boss card animation
        for (int i = 0; i < bossCardUIs.Count; i++)
        {
            bossCardUIs[i].Movement(new Vector3(width, -300, 0), transform);
            width += (float)(bossCardUIs[i].GetRectTransform().rect.width + 2);
        }
        yield return new WaitUntil(() => bossCardUIs[bossCardUIs.Count - 1].ispositionFound());
        yield return new WaitForSeconds(.5f);
        Boss b = Gameplay_Stage_Manager.Instance.getBoss();
        b.LookAtCards();
        yield return new WaitForSeconds(1.5f);
        //a quick glance at the boss.
        //some text system... (to be used down the line.)
        Debug.Log("boss card uis " + bossCardUIs.Count);
        int j = 0;
        while(bossCardUIs.Count > 0)
        {
            bossCardUIs[0].HalfAlphaColor(0.5f);
            nameToStage[bossCardUIs[0].cardSelectedStage].LookAtStage();
            nameToStage[bossCardUIs[0].cardSelectedStage].Interact(bossCardUIs[0].getIcon());
            PlayCard(bossCardUIs[0]);
            yield return new WaitForSeconds(.9f);
            //Debug.Log("bosscards " + j);
            RemoveAndDestroyUICard(bossCardUIs,bossCardUIs[j]);
            GetCardSelectionStage("boss").LookAtStage();
            
            yield return new WaitForSeconds(.2f);
        }
        //boss plays his hand relitevely fast.
        //after boss plays all of his cards.
        //reuse the gameplay loop...
        yield return null;
    }
    public override void beforeInput()
    {
      
          
        
    }
    public override void doSomething(int indx)
    {
        try
        {
            if (cardUIs.Count > 1) // []
            {
                cardUIs[previndx].HalfAlphaColor(2f);
            }
            cardUIs[index].HalfAlphaColor(0.5f);

            if (cardUIs[index] != null && cardUIs[index].selectedCoroutine != null)
            {
                cardUIs[index].StopCoroutine(cardUIs[index].selectedCoroutine);
            }

            nameToStage[cardUIs[index].cardSelectedStage].LookAtStage();
            cardUIs[index].StartCustomMovement();
        }
        catch(Exception e)
        {
            Debug.LogError("index out of bounds!" + e);
        }
    }
    public override bool condition()
    {
        // you have no card left to use...            //you've inputted more than the round permits...     //you skip.
        if(inputAmount > originalCardIndexLength-1 || inputAmount > 3 || !skipButton.getSkipTheRound())
        {
            Debug.Log("skip the round!" + skipButton.getSkipTheRound());

            StartCoroutine(BossCardSelection());
            return false;
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            nameToStage[cardUIs[index].cardSelectedStage].Interact(cardUIs[index].getIcon()); //spawns the box
            //EnableCard(cardUIs[index].getSelectedCard()); -> todo re-add when onenable does not host this code.
            PlayCard(cardUIs[index]);
            
           // storedCards.Push(cardUIs[index].getSelectedCard());// -> adds cards for eventual usage
            RemoveAndDestroyUICard(cardUIs,cardUIs[index]);
            ++inputAmount;
            cardUICounter.DecreaseCount();
            StopCoroutine(cardSelectCoroutine);
            StartCoroutine(CardSelectionCoroutine());
            Debug.Log("pressed return" + skipButton.getSkipTheRound() + " iA:" + inputAmount + " r:" + CardManager.Instance.getRounds());
        }
        return true;
    }
    private void PlayCard(CardUI c)
    {
        c.PlayCardUI();
        if (c.getSelectedCard() != null && c.getSelectedCard() is CardGameSetting)
        {
            Gameplay_Stage_Manager.Instance.AddPacket(c.getSelectedCard() as CardGameSetting);
        }
        else
        {
            Debug.LogError("either card is null or its is not of type 'CardGameSetting' ");
        }
    }
    private void RemoveAndDestroyUICard(List<CardUI> cUI,CardUI c)
    {
        //Destroy(c.getSelectedCard().gameObject);
        cUI.Remove(c);
        Destroy(c.gameObject);
        index = 0;

    }
    private void EnableCard(Card c)
    {
        c.gameObject.SetActive(true);
    } //implement later... 
    public CardSelectionStage GetCardSelectionStage(string s)
    {
        return nameToStage[s];
    }
    public IEnumerator PlayAct(bool stopOrPlayAct)
    {
        //add after boss attack apart of the gameplay loop
        CardSelect();
        Debug.Log("card selection act " + bossCardUIs.Count);
        yield return new WaitUntil(()=> bossCardUIs.Count <= 0);
        Debug.Log("moving on");
        yield return new WaitForSeconds(2);
        Act_Manager.Instance.setEnable(true);
    }
    public CardUICounter getCardUICounter()
    {
        return cardUICounter;   
    }
   
}
