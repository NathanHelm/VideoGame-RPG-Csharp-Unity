using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BossHandAttackSwipe : BodyMovementAttack
{
    protected Transform targetTrans;
    protected Boss boss;
    protected float offset;
    public override void Attack()
    {
        
        Debug.Log("attack");
        GetBossPart(boss, typeof(BossHand),this);
    }
    public override void Transformation()
    {
        coroutines = new IEnumerator[1];
        ChangeLayering(bossBodyPartMovementIsAttachedTo, "Enemy_Pathfinding");
        StartCoroutine(coroutines[0] = SwipeCoroutine("attack","grab"));
    }
    public override void StopTransformation()
    {
        base.StopTransformation();
    }
    public void SetUpBossHandSwipe(Boss b,float offset)
    {
        boss = b;
        this.offset = offset;
    }

    protected IEnumerator SwipeCoroutine(string rest, string attack)
    {
        Debug.Log("swipe coroutine");
        rb.drag = 1;
        targetTrans = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform;
        PlayAnimation(rest);
        float distF = 100;
        while (distF > 10f)
        {
            Debug.Log("distance swipe coroutine" + distF);
            FlipSprite();
            Vector3 newTargetPos = new Vector3(targetTrans.position.x + offset,targetTrans.position.y,0);
            Vector3 distance = (newTargetPos-bodyPartTransform.transform.position).normalized;
            distF = Vector2.Distance(newTargetPos, bodyPartTransform.transform.position);

            newTargetPos = new Vector3(targetTrans.position.x - offset, targetTrans.position.y, 0);
            distance = (newTargetPos - bodyPartTransform.transform.position).normalized;
            distF = Vector2.Distance(newTargetPos, bodyPartTransform.transform.position);




            rb.AddForce(distance * speed * 10 * rb.mass, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Push(attack));
        yield return null;
    }
    protected IEnumerator Push(string attack)
    {
        rb.drag = .5f;
        Vector3 freezePlayerPosition = new Vector3(targetTrans.position.x, targetTrans.position.y, 0);
        float distF = Vector3.Distance(freezePlayerPosition, bodyPartTransform.transform.position);
        PlayAnimation(attack);
        Vector3 freezedistance = (freezePlayerPosition - bodyPartTransform.transform.position).normalized;
        rb.AddForce(freezedistance * speed * 80 * rb.mass, ForceMode2D.Impulse);
        yield return new WaitUntil(() => rb.velocity.x < 1); //waits until full stop.
        yield return new WaitForSeconds(0.9f);
        Transformation();
    }
}
