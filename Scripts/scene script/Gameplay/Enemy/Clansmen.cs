using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clansmen : PathFindingEnemy
{
    Rigidbody2D rigidbody2D;
    List<Roach> roaches = new List<Roach>();
    IEnumerator enumerator;
    private Animator spawnFluidAnimation;
    private Animator clansmenAnimator;
    bool isenableAI = true;

    private void SetUp()
    {
        SetUpEnemy(1, 3, 10, getplayerTransform());
        SetPathFinding();
        rigidbody2D = GetComponent<Rigidbody2D>();
        clansmenAnimator = GetComponent<Animator>();
        spawnFluidAnimation = GetComponentInChildren<Animator>();
        StartCoroutine(enumerator = SpawnRoach(0.3f));
    }
    private void FixedUpdate()
    {
        Move();
        Attack();
        
    }
    public override void Move()
    {
        if (isenableAI)
        {
            AI();
        }
    }
    public override void Attack()
    {
       float distance = Vector3.Distance(transform.position, target.position);
//        Debug.Log("clansmen attack distance" + distance);
        if(distance < 3 && roaches.Count < 3 && enumerator == null)
        {
            StartCoroutine(enumerator = SpawnRoach(3));
        }
        else if(distance < 14 && enumerator == null)
        {
            
            StartCoroutine(enumerator = SpawnRoach(7));
        }
        
      
    }
    private IEnumerator SpawnRoach(float timer)
    {
        Debug.Log("spawning the roach!");
        yield return new WaitForSeconds(timer);
        isenableAI = false;
        SetSpawnAnimations();
        yield return new WaitForSeconds(1);
        GameObject g = Resources.Load<GameObject>(enemyPath + "roach");
        Roach r = Instantiate(g).GetComponent<Roach>();
        r.transform.position = transform.position;
        Debug.Log("Adding the roach!");
        roaches.Add(r);
        SetDefaultAnimation();
        isenableAI = true;
        enumerator = null;

        yield return null;
    }
    private void SetSpawnAnimations()
    {
        spawnFluidAnimation.Play("Roach_Spawn");
        clansmenAnimator.Play("Clansmen_Spawn");
    }
    private void SetDefaultAnimation()
    {
        spawnFluidAnimation.Play("Idle");
        clansmenAnimator.Play("Idle");
    }
    private void OnEnable()
    {
        SetUp();
    }
}
