using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStageMovement : BodyMovement
{
    Vector3 stageVector;
    bool isStageGrabbed = false;
    public override void Transformation()
    {
        ChangeLayering(bossBodyPartMovementIsAttachedTo, "NoHit");
        GrabStage();
    }
    public override void StopTransformation()
    {
        base.StopTransformation();
    }

    public void GrabStageMovementSetUp(Vector3 stageVec)
    {
        this.stageVector = stageVec;
    }
    public void GrabStage()
    {
        coroutines = new IEnumerator[1];
        Debug.Log("movement is going");
        StartCoroutine(coroutines[0] = GrabStageCoroutine());
    }
    public bool getIsStageGrabbed()
    {
        return isStageGrabbed;
    }
    public bool setIsStageGrabbed(bool b)
    {
        return isStageGrabbed = b;
    }
    public void setSpeed(int speed)
    {
        this.speed = speed;
    }
    public IEnumerator GrabStageCoroutine()
    {
        Debug.Log("movement coroutine is goning");
        rb.drag = 1;
        PlayAnimation("idle");
        while (true)
        {
            
            FlipSprite();
            float distance = Vector3.Distance(bodyPartTransform.position, stageVector);
            Vector3 direction =  (stageVector-bodyPartTransform.position).normalized;
            Debug.Log("grab stage direction" + direction);
            rb.AddForce(direction * 100 * speed);
            if (distance < 1)
            {
                Debug.Log(distance + " distance is < than one");
                rb.velocity = new Vector2(0,0);
                PlayAnimation("grab");
                setIsStageGrabbed(true);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
   
}
