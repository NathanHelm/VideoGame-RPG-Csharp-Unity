using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brother_Boss : Boss
{
    
    public override void SetCustomBoss()
    {
        //body parts
        bossName = "Brother_Boss";


        //bosscardOne.SetUpCard("Prefab/Cards/heavyenemy", "Spawn Strong Enemy","Spawns strong enemies!", "enemy");

        BossHead bossHead = gameObject.AddComponent<BossHead>();
        BossHand lbossHand = gameObject.AddComponent<BossHand>();
        BossHand rbossHand = gameObject.AddComponent<BossHand>();
        IdleBodyMovement idleBodyMovement_lowerB = gameObject.AddComponent<IdleBodyMovement>();
        IdleBodyMovement idleBodyMovement_upperB = gameObject.AddComponent<IdleBodyMovement>();
        IdleBodyMovement idleBodyMovement = gameObject.AddComponent<IdleBodyMovement>();
        BossHandAttackSwipe bossHandAttackSwiper = gameObject.AddComponent<BossHandAttackSwipe>();
        BossHandAttackSwipe bossHandAttackSwipel = gameObject.AddComponent<BossHandAttackSwipe>();
        GrabStageMovement lgrabStageMovement = gameObject.AddComponent<GrabStageMovement>();
        BossHeadAttackSwipe bossHeadAttackSwipe = gameObject.AddComponent<BossHeadAttackSwipe>();
        BossUpperB bossUpperB = gameObject.AddComponent<BossUpperB>();
        BossLowerB bossLowerB = gameObject.AddComponent<BossLowerB>();
        GrabStageMovement rgrabStageMovement = gameObject.AddComponent<GrabStageMovement>();
        BossHandAttackSpawn bossHandAttackSpawn = gameObject.AddComponent<BossHandAttackSpawn>();
        BossHandTiltStage bossHandTiltStage = gameObject.AddComponent<BossHandTiltStage>();
        BossHandAttackLookAtCards bossHandAttackLookAtCards = gameObject.AddComponent<BossHandAttackLookAtCards>();
        BossHandGoToPosition bossHandGoToPositionl = gameObject.AddComponent<BossHandGoToPosition>();
        BossHandGoToPosition bossHandGoToPositionr = gameObject.AddComponent<BossHandGoToPosition>();
        BossHeadFocusFace bossHeadFocusFace = gameObject.AddComponent<BossHeadFocusFace>();


        Vector2[] bounds = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getStageBoundsCords();
        Vector2[] enemySpawnPositions = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getEnemyCords();
        Enemy spawnEnemy = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies()[0];
        GameStage_Environment gameStage_Environment = Gameplay_Stage_Manager.Instance.getGameplay_Stage().GetGameStage_Environment();


        //look at cards
        bossHandGoToPositionl.setUpHandToPosition(new Vector3(0, 0, 0), 10);
        bossHandGoToPositionr.setUpHandToPosition(new Vector3(0, 0, 0), 10);
        

        bossHeadAttackSwipe.SetUpBossHeadSwipe(this, 42);
        bossHandAttackSwipel.SetUpBossHandSwipe(this, -42);
        bossHandAttackSwiper.SetUpBossHandSwipe(this, 42);
        lgrabStageMovement.GrabStageMovementSetUp(bounds[0]);
        rgrabStageMovement.GrabStageMovementSetUp(bounds[1]);
        idleBodyMovement.SetUpBodyMovementIdle(3, Time.deltaTime, 5);
        idleBodyMovement_upperB.SetUpBodyMovementIdle(2, Time.deltaTime/2, 3);
        idleBodyMovement_lowerB.SetUpBodyMovementIdle(2.5f, Time.deltaTime, 3);
        bossHandAttackSpawn.SetUpBossHandAttackSpawn(this, spawnEnemy, enemySpawnPositions);
        



        bossLowerB.SetUpBossLowerB(new List<BodyMovement>() { idleBodyMovement_lowerB },new Vector3(-1.2f, -14.3f, 0) ,bossName);
        bossUpperB.SetUpBossUpperB(new List<BodyMovement>() { idleBodyMovement_upperB }, new Vector3(-1.2f,27.2f,0) ,bossName);
        lbossHand.SetUpBossHand(new List<BodyMovement>() { lgrabStageMovement }, new Vector3(43,39,0), bossName);
        rbossHand.SetUpBossHand(new List<BodyMovement>() { rgrabStageMovement }, new Vector3(0, 0, 0), bossName);
        bossHead.SetUpBossHead(new List<BodyMovement>() { idleBodyMovement }, new Vector3(-1.2f, 36.5f,0),bossName);

        bossHandTiltStage.SetUpHandTiltStage(lgrabStageMovement, rgrabStageMovement, this , gameStage_Environment, 60);
        bossHandAttackLookAtCards.SetUpHandAttackLookAtCards(bossHandGoToPositionl,bossHandGoToPositionr,bossHeadFocusFace,this);
        
        //todo set up boss. 
        //
        SetUp(100, new List<IBossAttack>() { bossHandTiltStage,bossHeadAttackSwipe, bossHandAttackSwipel, bossHandAttackSpawn }, new List<BossBodyPart>() { lbossHand, rbossHand, bossHead, bossLowerB, bossUpperB}, bossHandAttackLookAtCards, new Vector3(-1.2f, 36.5f + (bossHead.transform.localScale.y / 2), 0), bossName);


    }
   
}
