using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Roach : PathFindingEnemy
{ 
    private ParticleSystem par;
    private Enemy colEnemy;
    
    public void SetUp()
    {
        SetUpEnemy(1, 4, 13, FindBarrier());
        SetPathFinding();
    }
    private Transform FindRoach()
    {
        Roach[] roach = FindObjectsOfType<Roach>();
        if (roach.Length <= 0)
        {
            return null;
        }
        Transform leastdistTrans = null;
        float leastdist = 10000;
        for (int i = 0; i < roach.Length; i++)
        {
            if (!roach[i].gameObject.activeSelf || roach[i].gameObject == this.gameObject)
            {
                continue;
            }
            float distance = Vector2.Distance(transform.position, roach[i].transform.position);
            if (distance < leastdist)
            {
                leastdist = distance;
                leastdistTrans = roach[i].transform;
            }
        }
        return leastdistTrans;
    }
    public override void AlcholismCard()
    {
        target = FindRoach();
        base.AlcholismCard();
    }
    private IEnumerator Eat()
    {
        
        yield return new WaitForSeconds(.5f);
        
        while (colEnemy.hp > 0 || colEnemy.gameObject.activeSelf)
        {
            par = GetComponentInChildren<ParticleSystem>();
            par.Play();
            PlayChildAnimation("eat");
            colEnemy.hp -= 1;
            yield return new WaitForSeconds(1.5f);
        }
            DisappearingSpriteChild();
            PlayAnimation("full");
            speed /= 4;

            yield return new WaitForSeconds(3f);
            PlayAnimation("dead");
            yield return new WaitForSeconds(1.5f);
            DisappearingSprite();
            par = ChildManager.Instance.MakeChildObject("Prefab/Particle_Systems/particle_system_Enemy_Death", transform).GetComponent<ParticleSystem>();
            par.Play();
            yield return new WaitUntil(() => !par.IsAlive(true));
            Destroy(gameObject);
            StopAllCoroutines();
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            //change target.
            //change
          
            colEnemy = collision.GetComponent<Enemy>();
            Attack();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == colEnemy)
        {
            StopCoroutine("Eat");
        }
    }
    public override void Attack()
    {
        StartCoroutine(Eat());
    }

    public void OnEnable()
    {
        SetUp();
        Gameplay_Stage_Manager.Instance.getGameplay_Stage().AddEnemy(this);
    }
    private void FixedUpdate()
    {
        Move();
    }
    public override void Move()
    {
        AI();
    }



}