using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : BossBodyPart
{
    
    public void SetUpBossHand(List<BodyMovement> bodyMovement, Vector3 spawn, string bossName)
    {
        RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Gameplay/BossAnimator/" + bossName + "/hand");
        SetUp("Prefab/Boss/BodyParts/Hand/Boss_Hand", bodyMovement,spawn, runtimeAnimatorController);
    }
    
}
