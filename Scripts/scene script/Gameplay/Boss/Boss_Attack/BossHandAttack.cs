using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandAttack : BossAttack
{
    protected BossBodyPart bBP;
    protected float speed;
    protected Rigidbody2D rb2D;
    public void GetBossHand(BossBodyPart bossBodyPart, float speed)
    {
        bBP = bossBodyPart;
        this.speed = speed;
        rb2D = bBP.GetComponent<Rigidbody2D>();
    }
   
}
