using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKeyword
{
/*
    Vector3 targetPos;
    [SerializeField]
    Vector2 tPos1;
    [SerializeField]
    float torque,thrust;
    Rigidbody2D rb;
    Rigidbody2D p;
    Animator boss;
    [SerializeField]
    float speed = 3f;
    bool isMove = false;
    bool damage = false;
    bool enableAnimation = false;
    private void Awake()
    {
        //gameObject.SetActive(false);
    }
    void OnEnable()
    {
        damage = false;
        speed = 2f;
        boss = FindObjectOfType<Boss>().GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        p = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody2D>();
        StartCoroutine(MoveStart());
        // v = rb.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddTorque(Mathf.Abs(p.transform.position.x)*2);
    }
    public IEnumerator Move (Rigidbody2D r)
    {
        for (float i = 0; i <= 10; i += 1*Time.deltaTime*.005f)
        {
            transform.localScale = new Vector3(i, i, 0);
        }
        while (true)
        {
            if (r.position.y < -17)
            {
                isMove = false;
            }
            if (r.position.y > 22.6)
            {
                isMove = true;
                BossAttackAnimation(boss, true);

            }
            else if (r.position.y < 22)
            {
                BossAttackAnimation(boss, false);
            }
            if (isMove == true)
            {
                r.AddForce(Vector3.down * (thrust*3));
                // r.angularDrag = 0;
                //Debug.Log("running 2");
            }
            else
            {
                r.AddForce(Vector3.up * thrust*2);
                // r.angularDrag = 0;
                // Debug.Log("running 1");
            }
            if(damage == true)
            {
                //code stops if character takes damagexs
                break;
            }
            yield return new WaitForFixedUpdate();
            
        }
        yield return null;
    }
    public IEnumerator MoveStart()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, tPos1, Time.deltaTime*speed);
            if (rb.position == tPos1)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Move(rb));
        Debug.Log("done");
        yield return null;
             

    }
    public override void Damage()
    {
        base.Damage();
        damage = true;

        Debug.Log("dmg");
    }
    Animator BossAttackAnimation(Animator bs,bool attack)
    {
        enableAnimation = attack;
        if (rb.position.x > 1)
        {
            bs.SetBool("enable", enableAnimation);
          //  bs.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            bs.SetBool("enable", enableAnimation);
           // bs.GetComponent<SpriteRenderer>().flipX = true;
        }
        
        //lol
        
        return bs;
    }
    
*/
  

}
