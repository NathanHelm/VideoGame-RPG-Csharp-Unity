using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCardSelectionStage : CardSelectionStage
{
    public override void Interact(Sprite icon)
    {
        
    }
    public override void LookAtStage()
    {
        CameraManager.Instance.CameraChanger(CameraManager.Instance.getPriorityCamIndex(), CameraManager.Instance.getIndex("Look_At_Boss"));
    }
}
