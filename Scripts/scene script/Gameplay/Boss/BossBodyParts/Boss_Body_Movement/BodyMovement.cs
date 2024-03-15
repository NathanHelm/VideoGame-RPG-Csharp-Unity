using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMovement : MonoBehaviour
{
    //body part (take head body part) MUST add the bodycomponents to the game.
    protected Transform bodyPartTransform;
    protected Rigidbody2D rb;
    protected IEnumerator[] coroutines;
    protected float speed = 1;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected BossBodyPart bossBodyPartMovementIsAttachedTo;
    public virtual void Transformation()
    {

    }
    public virtual void StopTransformation()
    {
        if (coroutines != null)
        {
            foreach (IEnumerator e in coroutines)
            {
                Debug.Log("stop transformation" + gameObject.name);
                StopCoroutine(e);
            }
        }
    }
    public void PlayAnimation(string s)
    {
        try
        {
            anim.Play(s);
        }
        catch(Exception e)
        {
            Debug.LogError("cannot find the animation/animator your looking for " + e);
        }
    }
    public void FlipSprite()
    {
        Debug.Log("flipped sprite" + (FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform.position.x - bodyPartTransform.position.x));
        if(FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().transform.position.x - bodyPartTransform.position.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
       
    }
    protected void ChangeLayering(BossBodyPart bossBodyPart, string layer)
    {
        bossBodyPart.gameObject.layer = LayerMask.NameToLayer(layer);
    }
    public virtual void SetUp(Transform t, Rigidbody2D rb, Animator animator, Vector3 spawnPos, SpriteRenderer spriteRenderer, RuntimeAnimatorController runtimeAnimatorController, BossBodyPart bossBodyPart)
    {
        //here are a couple of ideas ->
        bossBodyPartMovementIsAttachedTo = bossBodyPart;
        this.bodyPartTransform = 
        this.bodyPartTransform = t;
        this.rb = rb;
        anim = animator;
        anim.runtimeAnimatorController = runtimeAnimatorController;
        //set up animation
        bodyPartTransform.position = spawnPos;
        this.spriteRenderer = spriteRenderer;
        PlayAnimation("idle");
        ChangeLayering(bossBodyPartMovementIsAttachedTo,"NoHit");
    }
  


}
