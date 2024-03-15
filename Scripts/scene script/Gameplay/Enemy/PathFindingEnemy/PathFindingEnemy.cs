using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class PathFindingEnemy : Enemy, IPathFinding_Enemy
{
    AIDestinationSetter aIDestinationSetter;
    AIPath aIPath;
    Seeker seeker;
    protected Rigidbody2D rb;
    private string layerName = "Enemy_Pathfinding";
    public void AI()
    {
        BasicAI();
    }
    protected void SetPathFinding()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 3;
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aIPath = GetComponent<AIPath>();
        if(aIDestinationSetter == null)
        {
            aIDestinationSetter = gameObject.AddComponent<AIDestinationSetter>().GetComponent<AIDestinationSetter>();
            seeker = gameObject.AddComponent<Seeker>().GetComponent<Seeker>();
        }
        if(aIPath == null)
        {
            aIPath = gameObject.AddComponent<AIPath>().GetComponent<AIPath>();
        }
        aIPath.orientation = OrientationMode.YAxisForward;
        aIPath.enableRotation = false;
        aIDestinationSetter.target = target;
        aIPath.maxSpeed = speed;
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log("knockback!");
            EnemyKnockBack(collision.collider.transform, transform,collision.gameObject.GetComponent<Rigidbody2D>(), 200f);
            EnemyKnockBack(transform, collision.collider.transform, rb, 300);
            //enemy trans - target trans
        }
    }
    private void EnemyKnockBack(Transform enemyTrans,Transform col, Rigidbody2D rb, float knockbackforce)
    {
        Vector2 direction = (enemyTrans.position - col.position).normalized;
        
        Vector2 knockback = direction * knockbackforce;
        Debug.Log("direction" + direction + "knockback" + knockback);
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }
    private void GetCover()
    {
        //double check to see if this code works...
        if (this is not Healer && FindHealer() != null) // as long as your not a healer your are straight chilling.
        {
            aIDestinationSetter.target = FindHealer();
        }
        else if (FindBarrier() != null)
        {
            aIDestinationSetter.target = FindBarrier(); //findbarrier() -> finds closest barrier, findclosestvector() -> find vector SIDE that farthest from the player.
        }
        else
        {
            aIDestinationSetter.target = target;
        }
    }

    private Transform FindHealer()
    {
       Healer[] healers = FindObjectsOfType<Healer>();
       if(healers.Length <= 0)
       {
            return null;
       }
       Transform leastdistTrans = healers[0].transform;
       float leastdist = Vector2.Distance(transform.position, healers[0].transform.position); ;
       for(int i = 0; i < healers.Length; i++)
       {
            float distance = Vector2.Distance(transform.position, healers[i].transform.position);
            if(distance < leastdist)
            {
                leastdist = distance;
                leastdistTrans = healers[i].transform;
            }
       }
       return leastdistTrans;
    }
    protected Transform FindBarrier()
    {
        
        Blocker[] barrier = FindObjectsOfType<Blocker>();
        if (barrier.Length <= 0)
        {
            return null;
        }
        Transform leastdistTrans = barrier[0].findclosestVector(transform.position);
        float leastdist = Vector2.Distance(transform.position, barrier[0].transform.position); 
        for (int i = 0; i < barrier.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, barrier[i].transform.position);
            if (distance < leastdist)
            {
                leastdist = distance;
                leastdistTrans = barrier[i].findclosestVector(transform.position);
            }
        }
        return leastdistTrans;
    }
    protected Transform FindClosestComponent(Type t)
    {
       
        Component[] trans = (Component[])FindObjectsOfType(t);
        Transform t1 = null;
        float leastdist = 10000;
        foreach (Component c in trans)
        {
            if(!c.gameObject.activeSelf || c.gameObject == this.gameObject)
            {
                continue;
            }
            float distance = Vector2.Distance(transform.position, c.transform.position);
            if (distance < leastdist)
            {
                leastdist = distance;
                t1 = c.transform;
            }
        }
        return t1;

    }
  
   

    private void BasicAI()
    {
        double halfhealth = maxhp / 2;
        if(hp <= halfhealth)
        {
            GetCover();
        }
        else
        {
            aIDestinationSetter.target = target;
        }
      
    }


}
