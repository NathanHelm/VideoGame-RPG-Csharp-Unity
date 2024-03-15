using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guts_Chain : MonoBehaviour
{
    [SerializeField]
    GameObject littleblocks;
    [SerializeField]
    int littleblocksamount = 10;
    private List<Transform> lilblock = new List<Transform>();
    private Transform target;
    [SerializeField]
    Transform final;
    float circleD = 0;
    private void Start()
    {

        target = FindObjectOfType<PlayerMovement>().transform;
        littleblocks = Resources.Load<GameObject>("Scene0/guts_inkling");
        for (int i = 0; i <= littleblocksamount; i++)
        {
            GameObject single = Instantiate(littleblocks);
            circleD = .05f;
            single.transform.position = new Vector3(transform.position.x,transform.position.y - circleD * i ,0);
            lilblock.Add(single.transform);
        }
        final = lilblock[littleblocksamount];
        
    }
    private void FixedUpdate()
    {
        
        float z = Mathf.Sqrt((Mathf.Pow(final.position.x - target.position.x,2)) + (Mathf.Pow(final.position.y - target.position.y, 2)));
        Debug.Log("z: " + z);
        Debug.Log("Distance: "+ Vector3.Distance(final.position, target.position));
        float speed = Time.deltaTime * 2f;

        //change every block...
                int i = littleblocksamount - 1;//don't add head

                final.position = Vector3.MoveTowards(final.position, target.position, speed * littleblocksamount * 0.1f);
                Transform lilb = lilblock[i];
                lilb.position = Vector3.MoveTowards(lilb.position, final.position, speed * i * 0.1f);
                --i;
                while (i >= 0)
                {
                int indxPlusOne = i + 1;
              
                lilblock[i].position = Vector3.MoveTowards(lilblock[i].position, lilblock[indxPlusOne].position, speed * i * 0.1f );
        
                --i;
           
                }

        
    }
}
