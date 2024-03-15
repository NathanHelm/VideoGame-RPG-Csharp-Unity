using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Transform playerTransform;

    public void HandMovementFunction()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        MovementManager.Instance.MotherCaught(transform, playerTransform);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        HandMovementFunction();
    }
    //

}
