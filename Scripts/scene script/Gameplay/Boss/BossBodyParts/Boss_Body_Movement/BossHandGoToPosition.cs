using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandGoToPosition : BodyMovement
{
    Vector3 pos;
    float customSpd = 0;
    public void setUpHandToPosition(Vector3 pos, float customSpd)
    {
        this.pos = pos;
        this.customSpd = customSpd;
    }
    public override void Transformation()
    {
        coroutines = new IEnumerator[1];
        ChangeLayering(bossBodyPartMovementIsAttachedTo, "NoHit");
        StartCoroutine(coroutines[0] = GoTo());
    }
    public IEnumerator GoTo()
    {
        Debug.Log("movement coroutine is goning");
        
        rb.drag = 1;
        PlayAnimation("idle");

        while (true)
        {
            FlipSprite();
            float distance = Vector3.Distance(bodyPartTransform.position, pos);
            Vector3 direction = (pos - bodyPartTransform.position).normalized;
            Debug.Log("grab stage direction" + direction);
            rb.AddForce(direction * 100 * customSpd);
            if (distance < 1)
            {
                rb.velocity = new Vector2(0, 0);
                PlayAnimation("grab");
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
