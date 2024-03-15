using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CardSelectionStage : MonoBehaviour
{
    //card selection stage belong to the scene. can be taken from other scripts to use its functions.
    //can spawn a box with the required sprite data.




    //move camera to a directed place in the stage...
    //spawn enemies + blocker
    List<GameObject> blockData = new List<GameObject>();
   public virtual void Interact(Sprite cardIcon)
   {
       //on card interaction we take you to a different level in the stage...

   }
    public void DestroyBoxes()
    {
        int i = 0;
        while(blockData.Count > 0)
        {
            Destroy(blockData[i]);
            i++;
        }
        blockData = new List<GameObject>();
    }
   
    public void SpawnBox(Sprite sprite, ParticleSystem particleSystem)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/cardSelectionStage/block");
        particleSystem.Play();
        GameObject g = Instantiate(prefab);
        g.transform.position = new Vector3(499, 559, 0);
        SpriteRenderer s = g.GetComponent<SpriteRenderer>();
        blockData.Add(g);
        s.sprite = sprite;
        Destroy(g.GetComponent<PolygonCollider2D>());
        g.AddComponent<PolygonCollider2D>();

    }
    public virtual void LookAtStage()
    {
        Debug.LogError("look at stage not overriden!");
    }
    protected ParticleSystem playPar()
   {
        //new system implement plz
        GameObject g = Resources.Load<GameObject>("Prefab/Particle_Systems/particle_system_Spawn");
        ParticleSystem par = Instantiate(g).GetComponent<ParticleSystem>();
        par.transform.position = new Vector3(499, 559, 0);
        par.Play();
        return par;
   }
}
