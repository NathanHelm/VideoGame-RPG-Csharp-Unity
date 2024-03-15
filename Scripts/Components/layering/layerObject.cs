using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerObject : Ilayering
{
    private void Start()
    {
        SetPlayerTransform();
    }
    private void FixedUpdate()
    {
        ChangeLayering();
    }
}
