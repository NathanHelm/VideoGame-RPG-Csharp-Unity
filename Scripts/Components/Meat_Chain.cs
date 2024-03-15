using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat_Chain : MonoBehaviour
{
    // Start is called before the first frame update
    HingeJoint2D[] hingeJoint2D;
    List<Rigidbody2D> r;
    void Start()
    {
        hingeJoint2D = GetComponentsInChildren<HingeJoint2D>();
        for(int i = 0; i < hingeJoint2D.Length; i++)
        {
            Rigidbody2D rigid = hingeJoint2D[i + 1].gameObject.GetComponent<Rigidbody2D>();
            hingeJoint2D[i].connectedBody = rigid;
        }
        
    }
}
