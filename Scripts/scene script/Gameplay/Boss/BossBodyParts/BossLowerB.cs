using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLowerB : BossBodyPart
{
    public void SetUpBossLowerB(List<BodyMovement> bodyMovement, Vector3 spawn, string bossName)
    {
        RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Gameplay/BossAnimator/" + bossName + "/lowerb");
        SetUp("Prefab/Boss/BodyParts/lowerB/Boss_LowerB", bodyMovement, spawn, runtimeAnimatorController);
    }
    public override void Customize()
    {
        setSprite("Default");
    }
}
