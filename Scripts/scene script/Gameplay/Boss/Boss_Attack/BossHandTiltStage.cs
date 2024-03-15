using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class BossHandTiltStage : BodyMovementAttack
{
    // Start is called before the first frame update
    //a couple of thoughts...
    //first, I need to get hand data when both hand are in correct area...
    //perform the attack
    Boss b;
    BossHand left;
    BossHand right;
    Vector2[] spawnPosition;
    GrabStageMovement grabStageMovement1;
    GrabStageMovement grabStageMovement2;
    Transform environmentalTransformation;
    List<Rigidbody2D> inGameGameplayObjectsRb = new List<Rigidbody2D>();
    GameStage_Environment gsE;
    float angle = 60;
    //add for to these enemies;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    Item[] items; 
    Enemy[] enemy;
    
    PlayerMovement playerMovement;

    public void SetUpHandTiltStage(GrabStageMovement g1, GrabStageMovement g2, Boss b, GameStage_Environment gse, float angle)
    {
        this.b = b;
        this.grabStageMovement1 = g1;
        this.grabStageMovement2 = g2;
        this.angle = angle;
        grabStageMovement1.setSpeed(10);
        grabStageMovement1.setSpeed(10);
    }
    public override void Attack()
    {
        gsE = FindObjectOfType<GameStage_Environment>().GetComponent<GameStage_Environment>();
        try
        {
            left = b.GetBossHand(0);
            right = b.GetBossHand(1);
        }
        catch(Exception e)
        {
            Debug.LogError(e + " either the boss is not set or the hands are not assigned?");
        }
        environmentalTransformation = gsE.transform;
        GetBossPart(b, typeof(BossHand), grabStageMovement1);
        GetBossPart(b, typeof(BossHand), grabStageMovement2);
        coroutines = new IEnumerator[2];
        StartCoroutine(coroutines[0] = WaitForStageGrab());
    }
    public void getEveryGamePlayObjectInScene()
    {
        // Game
        //how do we get every gameplay object in the scene?
        List<Gameplay_Object> temp = Gameplay_Stage_Manager.Instance.getGameplay_Stage().GetCurGameplay_Objects();
        for(int i = 0; i < temp.Count; i++)
        {
            Rigidbody2D rb = temp[i].GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                inGameGameplayObjectsRb.Add(rb);
            }
        }
    }
    public void ApplyVecForce(float ang)
    {
        //consider this.
        Rigidbody2D player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>().PlayerRigidBody;
        Vector3 force = new Vector3(Mathf.Cos((ang + 180)* Mathf.Deg2Rad), Mathf.Sin((ang + 180) * Mathf.Deg2Rad), 0);
        Debug.Log(force + " force" + (ang + 180) * Mathf.Deg2Rad + "< - angle");
        player.AddForce(force * 1000,ForceMode2D.Force); //this works!
        for(int i = 0; i < inGameGameplayObjectsRb.Count; i++)
        {
            inGameGameplayObjectsRb[i].AddForce(force * inGameGameplayObjectsRb[i].drag * 100, ForceMode2D.Force);
        }

    }
    public override void Transformation()
    {
       
    }
    private IEnumerator WaitForStageGrab()
    {
        yield return new WaitUntil(() => grabStageMovement1.getIsStageGrabbed() && grabStageMovement2.getIsStageGrabbed());
        StartCoroutine(coroutines[1] = TiltStage());
        yield return null;
    }
    private IEnumerator TiltStage()
    {
        
        Debug.Log("TILT THE STAGE!");
        getEveryGamePlayObjectInScene();
        float zRot = environmentalTransformation.eulerAngles.z;
        float offset = 0.2f;
        float freq = 1;
        bool isCosLThanZero = false;
       
        while (zRot < angle)
        {
            gsE.DisplayCam();
            zRot += Mathf.PI/20 * 0.5f;
            freq += 0.1f;
            environmentalTransformation.transform.eulerAngles = new Vector3(0, 0, zRot);
            left.transform.position = new Vector3(left.transform.position.x + Mathf.Cos(freq) * offset, left.transform.position.y + Mathf.Sin(freq) * offset, 0);
            right.transform.position = new Vector3(right.transform.position.x + Mathf.Cos(freq) * offset, right.transform.position.y + Mathf.Sin(freq) * offset, 0);
            ApplyVecForce(angle);
            yield return new WaitForFixedUpdate();
        }
       
    }
    
   
}
