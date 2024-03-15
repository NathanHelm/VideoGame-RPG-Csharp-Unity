using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandAttackSpawn : BodyMovementAttack
{
    private Boss boss;
    private Enemy spawnenemy;
    private Vector2[] gameplaySpawnPositions;
    public override void Transformation()
    {
        coroutines = new IEnumerator[1];
        ChangeLayering(bossBodyPartMovementIsAttachedTo, "Enemy_Pathfinding");
        StartCoroutine(coroutines[0] = SpawnAttack());
    }
    public override void Attack()
    {
        GetBossPart(boss, typeof(BossHand), this);

    }
    public void SetUpBossHandAttackSpawn(Boss b, Enemy spawnenemy, Vector2[] spawnPosition)
    {
        //set up custom parameters in this function.
        boss = b;
        this.spawnenemy = spawnenemy;
        this.gameplaySpawnPositions = spawnPosition;
    }
    public void spawnEnemyInStage(Vector3 pos)
    {
        GameObject prefab = Resources.Load<GameObject>(spawnenemy.path);
        spawnenemy = Instantiate(prefab).GetComponent<Enemy>();
        spawnenemy.transform.position = pos;
    }
    protected IEnumerator SpawnAttack()
    {
        //code finds all enemy 'spawn' positions
        //when distance is reached, perform open hand and spawn an enemy

        if(gameplaySpawnPositions.Length <= 0)
        {
            Debug.LogError("no spawn positions");
        }
        for(int i = 0; i < gameplaySpawnPositions.Length; i++)
        {
            rb.mass *= 2;
           float dist = Vector3.Distance(gameplaySpawnPositions[i], bodyPartTransform.position);
            while(dist > 3)
            {
                PlayAnimation("rest");
                dist = Vector3.Distance(gameplaySpawnPositions[i], bodyPartTransform.position);
                FlipSprite();
                Vector2 position = (gameplaySpawnPositions[i]-(Vector2)bodyPartTransform.position).normalized;
                rb.AddForce((position * 5 * rb.drag * rb.mass * 3 * speed) * 2f,ForceMode2D.Force);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(1);
            PlayAnimation("attack");
            spawnEnemyInStage(gameplaySpawnPositions[i]);

            yield return new WaitForSeconds(2);
        }
        yield return null;
        Transformation();
    }
   
}
