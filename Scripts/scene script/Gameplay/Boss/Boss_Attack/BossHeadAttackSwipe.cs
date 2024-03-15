using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadAttackSwipe : BossHandAttackSwipe
{
    public override void Attack()
    {
        GetBossPart(boss, typeof(BossHead),this);
    }
    public override void StopTransformation()
    {
        base.StopTransformation();
    }
    public override void Transformation()
    {
        Debug.Log("boss head attack transformation coruoitne");
        coroutines = new IEnumerator[1];
        ChangeLayering(bossBodyPartMovementIsAttachedTo, "Enemy_Pathfinding");
        StartCoroutine(coroutines[0] = SwipeCoroutine("idle", "attack"));
    }
 
    public void SetUpBossHeadSwipe(Boss b, float offset)
    {
        boss = b;
        this.offset = offset;
    }
}
