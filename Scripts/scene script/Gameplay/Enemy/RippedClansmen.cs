using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippedClansmen : Enemy, IHeavyEnemy
{
    Rigidbody2D rb;
    public override void Attack()
    {
        StartCoroutine(ChargeAttack());
    }
    public void OnEnable()
    {
        SetUpEnemy(2, 10, 3, getplayerTransform());
        rb = GetComponent<Rigidbody2D>();
        Attack();
    }
    public IEnumerator ChargeAttack()
    {
        PlayAnimation("idle");
        yield return new WaitForSeconds(3f);
        PlayAnimation("charge");
        yield return new WaitForSeconds(1f);
        StartCoroutine(Jump());
    }
    public IEnumerator Jump()
    {
        PlayAnimation("jump");
        ChangeLayering("NoHit");
       
        float offset = 10;
        float speed = 15;
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 prevTarg = target.position;
        Vector3 movepos = new Vector3(direction.x, Mathf.Abs(direction.y) * offset, 0);
        rb.AddForce(movepos * speed * rb.mass * rb.drag, ForceMode2D.Impulse);
        yield return new WaitUntil(() => rb.velocity.y < 0.1);
        direction = (prevTarg - transform.position).normalized;
        rb.AddForce(direction * speed * rb.mass * rb.drag * 5, ForceMode2D.Impulse);
        while (Vector3.Distance(transform.position, prevTarg) > 5f)
        {
            direction = (prevTarg - transform.position).normalized;
            rb.AddForce(direction * speed * rb.mass * rb.drag * 5, ForceMode2D.Force);
            PlayAnimation("jump");
            yield return new WaitForFixedUpdate();
        }
        PlayAnimation("land");
        ChangeLayering("Enemy_Pathfinding");

        StartCoroutine(ChargeAttack());
        yield return null;
    }

}
