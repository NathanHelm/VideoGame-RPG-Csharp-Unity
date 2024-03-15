using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int health;
    int difficulty; //make a game manager
    [SerializeField]
    protected List<BossBodyPart> bossBodyParts = new List<BossBodyPart>();
    protected List<IBossAttack> bossAttacks = new List<IBossAttack>();
    Stack<IBossAttack> attacks = new Stack<IBossAttack>();
    Stack<IBossAttack> curAttacks = new Stack<IBossAttack>();
    BossUpperB bossUpperB;
    BossLowerB bossLowerB;
    private List<BossHand> bossHands = new List<BossHand>();
    Gameplay_Stage_Manager gameplay_Stage_Manager;
    Animator bossAnimator;
    public string bossName = "";
    IBossAttack lookAtCards;
    bool isBossStateEnable = true;
    private List<BossCard> bossCards = new List<BossCard>();
    private Vector2 bossCardPosition;
    private IEnumerator bossCoroutine;


    public void SetUp(int health, List<IBossAttack> bossAttacks, List<BossBodyPart> bossBodyParts, IBossAttack lookAtCards, Vector2 bossCardPosition, string bossName)
    {
        this.bossName = bossName; 
        gameplay_Stage_Manager = Gameplay_Stage_Manager.Instance;
        this.health = health;
        this.bossBodyParts = bossBodyParts;
        this.bossAttacks = bossAttacks;
        this.lookAtCards = lookAtCards;
        bossCards = CardManager.Instance.getBossCards();
        this.bossCardPosition = bossCardPosition;
        PushAttacks();
    }

    public List<BossBodyPart> GetBossBodyPart()
    {
        return bossBodyParts;
    }
    public virtual void SetCustomBoss()
    {
        Debug.LogError("boss class not inherited");
    }
    public void AddBodyPart(BossBodyPart bHand)
    {
        bossBodyParts.Add(bHand);
    }
    public void AddAttack(IBossAttack bAttack)
    {
        bossAttacks.Add(bAttack);
    }
    public void LookAtCards()
    {
        lookAtCards.Attack();
    }
    public void StopAllBodyMovement()
    {
        for(int i = 0; i < bossBodyParts.Count; i++)
        {
            for (int j = 0; j < bossBodyParts[i].bodyMovements.Count; j++)
            {
                bossBodyParts[i].bodyMovements[j].StopTransformation();
            }
        }
    }
    public void SpawnBodyParts()
    {
        //spawn the bosses body parts.
        //transitions all bosses body part 'code' from main gameobject to its other 'body parts.'
        List<BossBodyPart> bBP = new List<BossBodyPart>();
        while(bossBodyParts.Count > 0)
        {
            GameObject g = Resources.Load<GameObject>(bossBodyParts[0].getPath());
            GameObject spawnObj = Instantiate(g);
            BossBodyPart temp = null;

            //ok hoe, for every new body part the boss has is REQUIRED

            if (bossBodyParts[0] is BossHand)
            {
                temp = spawnObj.AddComponent<BossHand>();
                bossHands.Add((BossHand)temp);
            }
            else if(bossBodyParts[0] is BossHead)
            {
                temp = spawnObj.AddComponent<BossHead>();
                
            }
            else if(bossBodyParts[0] is BossUpperB)
            {
                temp = spawnObj.AddComponent<BossUpperB>();
            }
            else if(bossBodyParts[0] is BossLowerB)
            {
                temp = spawnObj.AddComponent<BossLowerB>();
            }
            else
            {
                Debug.LogError("did not add bodypart instance to gameobject");
            }
            temp.SetUp(bossBodyParts[0].getPath(), bossBodyParts[0].bodyMovements,
            bossBodyParts[0].getSpawnPos(),bossBodyParts[0].getAnimationController());
            temp.AddComponents();
            temp.SetUpBodyMovement();
            temp.Move(); //considering that movement happens immedietly after spawning, this is what we want. 
            bossBodyParts.RemoveAt(0);
            bBP.Add(temp);
        }
        bossBodyParts = bBP;

//        Debug.LogError("boss body part ->" +  bossBodyParts[0].gameObject.name);

    }
    void PushAttacks()
    {
        //attack every attack the boss has into a stack.
        //every attack.pop() plays the gameObject's 'attack.'
        for (int i = 0; i < bossAttacks.Count; i++)
        {
            attacks.Push(bossAttacks[i]);
        }
    }
    public void MoveBodyParts()
    {
       for(int i = 0; i < GetBossBodyPart().Count; i ++)
        {
            GetBossBodyPart()[i].Move();
        }
    }
    public IEnumerator AttackStateCoroutine()
    {
        //courotine for Attackstate 'state'
        curAttacks = attacks;
        while (true)
        {
            Debug.Log("attack state continuing");
            curAttacks.Pop().Attack();
            yield return new WaitForSeconds(14f);
        }
    }

    public void AttackState()
    {
        //meant for the gameplay manager, the "state that the boss is in during the 'act.'"
        Debug.Log("start Attack State");
        StartCoroutine(bossCoroutine = AttackStateCoroutine());
    }
    public BossHand GetBossHand(int bosshandIndex)
    {
       return bossHands[bosshandIndex];
    }
    public List<BossCard> GetBossCards()
    {
        return bossCards;
    }
    public Vector2 GetBossCardPosition()
    {
        return bossCardPosition;
    }
    public IEnumerator getBossCoroutine()
    {
        return bossCoroutine;
    }

    public void setIsBossStateEnable(bool isbossstateenable)
    {
        isBossStateEnable = isbossstateenable;
    }

}
