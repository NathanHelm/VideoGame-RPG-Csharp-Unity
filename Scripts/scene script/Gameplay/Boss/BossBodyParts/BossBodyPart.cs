using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyPart : MonoBehaviour
{
    protected string path = "";
    public List<BodyMovement> bodyMovements = new List<BodyMovement>(); //when seting up script, script asks ->
    protected Animator anim;
    public Rigidbody2D rigidbody2D;
    protected BoxCollider2D boxCollider2D;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 spawnPosition;
    protected RuntimeAnimatorController runtimeAnimatorController;
    private string spriteLayer = "AheadPlayer";
    int currentAttack = 0;

    //"what movement do you want to set this object to?"
    public void SetUpBodyMovement()
    {
        for(int i = 0; i < bodyMovements.Count; i++)
        {
            bodyMovements[i].SetUp(transform, rigidbody2D, anim, spawnPosition,spriteRenderer,runtimeAnimatorController, this);
        }
    }

    public virtual void Move()
    {
        bodyMovements[currentAttack].Transformation(); //todo replace this.
    }
    public void StopMovement()
    {
      bodyMovements[currentAttack].StopTransformation();
    }
    public void AddAndPlayMove(BodyMovement bodyMovement)
    {
        StopMovement();
        bodyMovement.SetUp(transform, rigidbody2D, anim, spawnPosition, spriteRenderer, runtimeAnimatorController,this);
        bodyMovements[currentAttack] = bodyMovement;
        Move();
    }
    public void SetUp(string path, List<BodyMovement> bodyMovements, Vector3 spawnPos, RuntimeAnimatorController runtimeAnimatorController)
    {
        //set to instantiated object
        this.path = path;
        this.bodyMovements = bodyMovements;
        spawnPosition = spawnPos;
        this.runtimeAnimatorController = runtimeAnimatorController;
        Customize();
    }
    public virtual void Customize()
    {
        //directed for unique data that will transfer to the instantiated
    }
    protected void setSprite(string s)
    {
        this.spriteLayer = s;
    }
    public void AddComponents()
    {
        anim = gameObject.AddComponent<Animator>();
        rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        rigidbody2D.gravityScale = 0;
        spriteRenderer.sortingLayerName = spriteLayer;
        anim.runtimeAnimatorController = runtimeAnimatorController;
        
 
    }
    public RuntimeAnimatorController getAnimationController()
    {
        return runtimeAnimatorController;
    }
    public int getCurrentAttack()
    {
        return currentAttack;
    }

    public string getPath()
    {
        return path;
    }

    public Vector3 getSpawnPos()
    {
        return spawnPosition;
    }
    public void Destroy()
    {
        Destroy();
    }
    


}
