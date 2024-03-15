using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMovement : MonoBehaviour
{
    private void Start()
    {
        MovementManager.Instance.Wobble(transform,true, Random.Range(Mathf.PI/20, Mathf.PI/10),0.5f);
    }
}
