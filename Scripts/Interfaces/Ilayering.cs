using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ilayering : DataPersistence
{
    private SpriteRenderer s;
    private Transform p;
    string layer1 = "AheadPlayer";
    string layer2 = "BehindPlayer";
    public void SetPlayerTransform()
    {
        p = FindObjectOfType<PlayerMovement>().transform;
        s = GetComponent<SpriteRenderer>();
    }
    public void ChangeLayering()
    {
        if (s != null)
        {
            if (p.transform.position.y > gameObject.transform.position.y)
            {
               
                s.sortingOrder = 1;
                s.sortingLayerName = layer1;
            }
            else
            {
                //play it safe so that every NPC is still layered above MOST sprites
                s.sortingOrder = 10;
                s.sortingLayerName = layer2;
            }
        }

       
    }

}
