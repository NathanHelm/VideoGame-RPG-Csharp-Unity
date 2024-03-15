using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovementAttack : BodyMovement, IBossAttack, IGameSetting
{
    
    public virtual void Attack()
    {
        throw new System.NotImplementedException();
    }
    protected void AddAttackComponents(BossBodyPart bossBodyPart)
    {
        CircleCollider2D c =  bossBodyPart.gameObject.AddComponent<CircleCollider2D>();
        c.radius *= 0.5f;
        bossBodyPart.rigidbody2D.mass = 50;
        bossBodyPart.rigidbody2D.drag = 1;
        bossBodyPart.rigidbody2D.angularDrag = 10;
        
    }
    public void GetBossPart(Boss b, Type t, BodyMovement bha)
    {
        List<BossBodyPart> bodyPart = b.GetBossBodyPart();

        for (int i = 0; i < bodyPart.Count; i++)
        {
            //finds a unique body part. dissaproves of duplicates
            Debug.Log("body movements" + bodyPart[i].bodyMovements[bodyPart[i].getCurrentAttack()].GetType() + "other type" + bha.GetType());
            if (bodyPart[i].GetType() == t && bodyPart[i].bodyMovements[bodyPart[i].getCurrentAttack()].GetType() != bha.GetType())
            {
                Debug.Log("found body part" + bodyPart[i].gameObject.name + bha.GetType());
                bodyPart[i].AddAndPlayMove(bha); //replaces movement component with attack component
                AddAttackComponents(bodyPart[i]); //adds unique code/data to the modified object
                break;
            }

//            Debug.LogError("cannot find body part that attack type is asking for \n or body attacks assigned to bodymovement > bodyparts bodymovements");
        }
    }
    //implented using interface
  


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerManager.instance.health -= 1;
          
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
           Enemy e  = collision.collider.GetComponent<Enemy>();
            e.hp -= 1;
        }
    }

    public void SpawnChildGameObject(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        GameObject g = Instantiate(prefab, transform);
        g.transform.localScale *= 2;
    }

    public void AlcholismCard()
    {
     //   anim.Play("drunk");
    }
}
