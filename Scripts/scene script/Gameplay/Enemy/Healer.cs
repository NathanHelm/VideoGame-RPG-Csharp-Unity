using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : PathFindingEnemy
{
    protected float healingTime = 3;
    Dictionary<int, IEnumerator> enemyToCoroutine = new Dictionary<int, IEnumerator>();
    private void OnEnable()
    {
        SetUpEnemy(1, 3, 5, getplayerTransform());
        SetPathFinding();
    }
    public override void Attack()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerManager.instance.setPlayerHealth(-(attackDamage));
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Enemy_Pathfinding"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            IEnumerator e = null;
            StartCoroutine(e = Healing(enemy));
            enemyToCoroutine.TryAdd(collider.gameObject.GetHashCode(), e);

        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy_Pathfinding"))
        {
          StopCoroutine(enemyToCoroutine[collider.gameObject.GetHashCode()]);
        }
    }
    public IEnumerator Healing(Enemy e)
    {
        while (true)
        {
            yield return new WaitForSeconds(healingTime);
            e.hp += 1;
        }
    }
    public override void Move()
    {
        AI();
    }
}
