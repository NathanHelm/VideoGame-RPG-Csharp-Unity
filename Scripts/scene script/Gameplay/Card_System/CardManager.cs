using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    List<Card> cards = new List<Card>(); //gathers every card...
    List<Card> curCards = new List<Card>();
    List<IGameSetting> gameSettingsCurObj = new List<IGameSetting>();
    List<Enemy> enemies;
    List<BossCard> bossCards = new List<BossCard>();
    List<BossCard> curBossCard = new List<BossCard>();
    [SerializeField]
    Gameplay_Stage gameplay_Stage;
    Vector2[] cardPositions;
    Vector2 bossCardsPosition;
    int rounds = 0;
    public static CardManager Instance;
    public void OnEnable()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<CardManager>().GetComponent<CardManager>();
        }
    }
    public void SetBossCards()
    {
        bossCardsPosition = Gameplay_Stage_Manager.Instance.getBoss().GetBossCardPosition();
    }
    public List<BossCard> getBossCards()
    {
        return bossCards;
    }
    //important
    public void getAllCards()
    {
        //this will have to change.
        Gameplay_Object_Data[] temp = Resources.LoadAll<Gameplay_Object_Data>("Gameplay/Cards");
        for(int i = 0; i < temp.Length; i++)
        {
            GameObject g = Resources.Load<GameObject>(temp[i].Gameplay_Object_Path);
            Card card = g.GetComponent<Card>();
            card.setPath(temp[i].Gameplay_Object_Path);
            card.icon = temp[i].EnemySprite;
            if (card is BossCard)
            {
                bossCards.Add((BossCard)card);
            }
            else
            {
                cards.Add(card); //we get the card.
            }
        }
    }
    public void spawnCards()
    {
        curCards = new List<Card>();
       int spawnAmount = 2;
        if(rounds > 3)
        {
            spawnAmount = 2;
        }
       if(rounds > 5)
        {
            spawnAmount = 3;
        }
        if(rounds > 7)
        {
            spawnAmount = 4;
        }
      for(int i = 0; i < spawnAmount; i++)
        {
            int randomindx = (int)Random.Range(0, cards.Count-1);
            Debug.Log("random index " + randomindx);
            int randomSpawnPosindx = Random.Range(0, cardPositions.Length - 1);
            Vector3 spawnposition = cardPositions[randomSpawnPosindx];
            curCards.Add(cards[randomindx].SpawnCard(spawnposition));
        }
        spawnEnemyCards();

    }
    private void spawnEnemyCards()
    {
        curBossCard = new List<BossCard>();
        for(int i = 0; i <= 3; i++)
        {
            int randomindx = (int)Random.Range(0, bossCards.Count - 1);

            curBossCard.Add((BossCard)bossCards[randomindx].SpawnCard(bossCardsPosition)); //need a vector 2 position for where the boss currently is... 
          
        }
    }

    public IEnumerator spawnCardCoroutine()
    {
       //to be used.
        yield return null;
    }
    public void setAllGameSettingInterface()
    {
        List<Gameplay_Object> gameplay_Objects = Gameplay_Stage_Manager.Instance.getGameplay_Stage().GetCurGameplay_Objects();
        for (int i = 0; i < gameplay_Objects.Count; i++)
        {
           if(gameplay_Objects[i] is IGameSetting)
            {
                Debug.Log("gameplay obj found and added");
                gameSettingsCurObj.Add((IGameSetting)gameplay_Objects[i]);
            }
        }
    }
    public void setManager(int round, Vector2[] cardSpawnPositions)
    {
        this.cardPositions = cardSpawnPositions; 
        rounds = round;
        gameplay_Stage = Gameplay_Stage_Manager.Instance.getGameplay_Stage();
        getAllCards();
        setAllGameSettingInterface();
        setAllEnemies();
        SetBossCards();
    }

    //card related stuff
    


    
    public void setAllEnemies()
    {
        enemies = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies();
    }
    public List<IGameSetting> getGameSettingInterfaces()
    {
        return gameSettingsCurObj;
    }
    public List<BossCard> getCurBossCard()
    {
        return curBossCard;
    }
    public List<Enemy> getEnemies()
    {
        return enemies;
    }
    public Gameplay_Stage getGameplay_Stage()
    {
        return gameplay_Stage;
    }
    public List<Item> getItems()
    {
        return gameplay_Stage.getCurItems();
    }
    public List<Card> getCurrentCards()
    {
        return cards;
    }
    public int getRounds()
    {
        return rounds;
    }

}
