using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHead : BossBodyPart
{
    public void SetUpBossHead(List<BodyMovement> bodyMovement, Vector3 spawn, string bossName)
    {
        RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Gameplay/BossAnimator/" + bossName + "/head");
        SetUp("Prefab/Boss/BodyParts/Head/Boss_Head", bodyMovement,spawn,runtimeAnimatorController);
    }
}
