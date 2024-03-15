using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
[System.Serializable]
public class DialogueObject : MonoBehaviour
{
    public Sprite DisplayCharacter { get; set; }
    public float TypeSpeed { get; set; }
    public string DialogueLine { get; set; }
    public GenericEventMethod[] genericEventMethods { get; set; }
    public bool loopEvent = false;
    public DialogueObject (string dialogueL, string dC, float tS)
    {
        this.DialogueLine = dialogueL;
        this.DisplayCharacter = Resources.Load<Sprite>("Sprites/AvatarSprites/" + dC);
        this.TypeSpeed = tS;
    }
    public DialogueObject(string dialogueL, float tS)
    {
        this.DialogueLine = dialogueL;
        this.TypeSpeed = tS;
    }
    public DialogueObject (string dialogueL, string dC)
    {
        this.DisplayCharacter = Resources.Load<Sprite>("Sprites/AvatarSprites/" + dC);
        this.DialogueLine = dialogueL;
    }
    public DialogueObject (string dialogueL)
    {
        this.DialogueLine = dialogueL;
    }
    //events
    public DialogueObject(string line,GenericEventMethod[] custom,bool loopEvent)
    {
        this.DialogueLine = line;
        this.loopEvent = loopEvent;
        genericEventMethods = custom;
    }
    public DialogueObject(string line,string dC, GenericEventMethod[] custom, bool loopEvent)
    {
        this.DisplayCharacter = Resources.Load<Sprite>("Sprites/AvatarSprites/" + dC);
        this.DialogueLine = line;
        this.loopEvent = loopEvent;
        genericEventMethods = custom;
    }
    public DialogueObject(string line, float tS, GenericEventMethod[] custom, bool loopEvent)
    {
        this.DialogueLine = line;
        this.TypeSpeed = tS;
        this.genericEventMethods = custom;
        this.loopEvent = loopEvent;
    }
    public void Enqueue()
    {
        try
        {
            if (genericEventMethods.Length > 0)
            {
                GenericEventMethod[] eventMethods = new GenericEventMethod[] { };
                if (loopEvent)
                {
                   eventMethods = genericEventMethods; 
                }

                IEvent.e.pushEvents(genericEventMethods);

                if (loopEvent)
                {
                    genericEventMethods = eventMethods;
                }
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log("assigned event is null" + e);
        }
    }
    //sprites
    public Sprite GetSprite()
    {
        //run in dialogue manager, used to display the dialogue's sprite...
        return DisplayCharacter;
    }
    
    


}

