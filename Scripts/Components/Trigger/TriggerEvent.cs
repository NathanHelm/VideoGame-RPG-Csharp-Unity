using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : Ilayering
{
    [SerializeField]
    bool islayeringObject = true;
    [SerializeField]
    bool setUpTriggerD = true;
    [SerializeField]
    string eventKey;
    private BoxCollider2D box;
    public BoxCollider2D[] Box { get; set; } = new BoxCollider2D[1];


    public virtual void Action()
    {
        //meant to be overriden
    }
    private void setNPC(NPC nPC)
    {
        NPC_Dictionary.Instance.setDialogueNPC(nPC);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //runs event if there's a collision
        if (other.CompareTag("Player"))
        {
            if (getTrigger() == true)
            {
                AddBoxCollider();
            }
            setNPC(this as NPC);
            if (eventKey != "")
            {
                EventDictionary.Instance.EnqueueEvents(eventKey);
                IEvent.e.PlayEvent();
            }
            Action();

        }
    }

    private void FixedUpdate()
    {
        if (islayeringObject)
        {
            ChangeLayering();
        }
    }

    private void OnEnable()
    {
    }
    public bool getTrigger()
    {
        return setUpTriggerD;
    }
    public bool setTrigger(bool b)
    {
        setUpTriggerD = b;
        return setUpTriggerD;
    }
    private BoxCollider2D[] AddBoxCollider()
    {
        //add box collider to gameobject if needed
        Box = GetComponents<BoxCollider2D>();
        bool bTrig = false;
        BoxCollider2D boxTrig;
        foreach(BoxCollider2D single in Box)
        {
            if (single.isTrigger == true)
            {
                bTrig = true; 
            }
        }
        if(bTrig == false)
        {
            boxTrig = gameObject.AddComponent<BoxCollider2D>();
            boxTrig.isTrigger = true;
            return Box;
        }
        else
        {
            return Box;
        } 
    }

   public void setKey(string key)
   {
     eventKey = key;
   }
    public string getKey()
    {
        return eventKey;
    }
  




}
