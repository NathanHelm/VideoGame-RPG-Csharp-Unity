using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCardSelectionStage : CardSelectionStage
{
    public override void LookAtStage()
    {
        CameraManager.Instance.CameraChanger(CameraManager.Instance.getPriorityCamIndex(), CameraManager.Instance.getIndex("Look_At_Boss"));
    }
    public override void Interact(Sprite cardIcon)
    {
        SpawnBox(null, null);
    }

}
