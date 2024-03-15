using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandAttackLookAtCards : BodyMovementAttack
{
    List<CardUI> bosscardUIs;
    Boss b;
    BossHandGoToPosition bossHandGoToPositionL, bossHandGoToPositioneR;
    BossHeadFocusFace bossHeadFocusFace;
    List<BossCard> bossCards ;
    public void SetUpHandAttackLookAtCards(BossHandGoToPosition l, BossHandGoToPosition r, BossHeadFocusFace  bossHeadFocusFace, Boss b)
    {
        this.b = b;
        bossHandGoToPositioneR = r;
        bossHandGoToPositionL = l;
        this.bossHeadFocusFace = bossHeadFocusFace;
        

    }
    public override void Attack()
    {
        coroutines = new IEnumerator[1];
        StartCoroutine(coroutines[0] = LookAtCards());
    }
    public IEnumerator LookAtCards()
    {
        
        bossCards = CardManager.Instance.getCurBossCard();
        if (rb == null)
        {
            Debug.LogError("rigidbody is null!");
        }
        GetBossPart(b, typeof(BossHand), bossHandGoToPositioneR);
        GetBossPart(b, typeof(BossHand), bossHandGoToPositionL);
        GetBossPart(b, typeof(BossHead), bossHeadFocusFace);
        for(int i = 0; i < bossCards.Count; i++)
        {
            bossCards[i].BossCardFollowPos(new Vector3(0,0,0));
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    public void SpawnCards()
    {

    }


}
