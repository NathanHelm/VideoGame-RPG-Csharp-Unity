using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUpperB : BossBodyPart
{
    public void SetUpBossUpperB(List<BodyMovement> bodyMovement, Vector3 spawn, string bossName)
    {
        RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Gameplay/BossAnimator/" + bossName + "/upperb");
        SetUp("Prefab/Boss/BodyParts/upperB/Boss_UpperB", bodyMovement, spawn, runtimeAnimatorController);
    }
    public override void Customize()
    {
        setSprite("BehindPlayer");
    }
}
