using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    protected int dmg;
    protected float speed;
    protected Vector2 target;

    protected void SetUp(int dmg, float spd, Vector2 tpos)
    {
        this.dmg = dmg;
        this.speed = spd;
        target = tpos;
    }

    protected void SmashAttack()
    {

    }
    protected void SwingAttack()
    {

    }
    protected void SpawnAttack()
    {

    }
    
  
}
