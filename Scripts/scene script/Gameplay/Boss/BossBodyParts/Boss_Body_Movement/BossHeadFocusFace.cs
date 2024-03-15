using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossHeadFocusFace : BodyMovement
{
    public void Face()
    {
        PlayAnimation("focus");
    }
    public override void Transformation()
    {
        Face();
    }
}