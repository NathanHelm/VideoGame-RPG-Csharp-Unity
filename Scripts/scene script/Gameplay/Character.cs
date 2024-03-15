using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Enemy
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyKnockBack(collision.collider.transform, transform, collision.gameObject.GetComponent<Rigidbody2D>(), 200f);
    }
    private void EnemyKnockBack(Transform enemyTrans, Transform col, Rigidbody2D rb, float knockbackforce)
    {
        Vector2 direction = (enemyTrans.position - col.position).normalized;

        Vector2 knockback = direction * knockbackforce;
        Debug.Log("direction" + direction + "knockback" + knockback);
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
}
