using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventDictionary : MonoBehaviour
{
    public static EventDictionary Instance = new EventDictionary();
    Dictionary<string, GenericEventMethod[]> keyValuePairs = new Dictionary<string, GenericEventMethod[]>(); //invoke
    /*
     *  add an event with a key value... 
     *  can be added anywhere, say you want a trigger to fire off a certain amount of events:
     *  simply create a keyValuePair give it the name "TriggerOne" and then in the inspector add key to string... 
    */
    public void AddLevel(string level)
    {


        keyValuePairs.Add("LEVEL0/TRIGGER1", new GenericEventMethod[]{
            new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Wounded_Player"),false,"Wounded_Player_Fall",null),
            new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeTriggerEvent,NPC_Dictionary.Instance.getNPC("Change_Animation_Trigger"),""),
        });
        keyValuePairs.Add("LEVEL1/TRIGGER1", new GenericEventMethod[] { new TwoParameterMethod<int, int>(CameraManager.Instance.CameraChanger, 0, 1), new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getNPC("Brother_1")), });
        keyValuePairs.Add("LEVEL1/TRIGGER2", new GenericEventMethod[]
        { new FiveParameterMethod<NPC, float, float, float, GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Mother"), -455.7f, 760.6f, 30f, null),
        new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getNPC("Mother_Trigger")),
            new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,2),

        });

        keyValuePairs.Add("LEVEL1/TRIGGER3", new GenericEventMethod[]
            {
                 new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog,NPC_Dictionary.Instance.getNPC("kid_barrier"))
            });
        keyValuePairs.Add("LEVEL1/TRIGGER4", new GenericEventMethod[]
          {
                 new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog,NPC_Dictionary.Instance.getNPC("Brother_is_mad_talk"))
          });

        keyValuePairs.Add("LEVEL1/TRIGGER5", new GenericEventMethod[]
        {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,6)
        });

        keyValuePairs.Add("LEVEL1/TRIGGER6", new GenericEventMethod[]
      {
                 new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog,NPC_Dictionary.Instance.getNPC("MotherTalk_scene3"))
      });
        keyValuePairs.Add("LEVEL1/TRIGGER7", new GenericEventMethod[]
         {
                  new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog,NPC_Dictionary.Instance.getNPC("Kid_Help_1"))
        });
        keyValuePairs.Add("LEVEL1/TRIGGER8", new GenericEventMethod[]
        {
                  new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog,NPC_Dictionary.Instance.getNPC("Kid_Help_2"))
       });
        keyValuePairs.Add("LEVEL1/TRIGGER9", new GenericEventMethod[]
        {
             new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getNPC("Clansmen_Trigger")),
        });
        keyValuePairs.Add("LEVEL1/TRIGGER10", new GenericEventMethod[]
      {
             new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getNPC("journal")),

      });

        keyValuePairs.Add("LEVEL1/CHANGEPOS1", new GenericEventMethod[]{
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,3.3f, -67.2f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS2", new GenericEventMethod[]{
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,-369.5f, 706.8f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS3", new GenericEventMethod[]{
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,-172.27f, 767.4f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS4", new GenericEventMethod[]{
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,-9.37f, 178.34f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS5", new GenericEventMethod[]{
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,261.39f, 947.28f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS6&7", new GenericEventMethod[]{
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,205.36f, 259.77f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS8", new GenericEventMethod[]{
            new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,679.4f, 947.2f)
        });
        keyValuePairs.Add("LEVEL1/CHANGEPOS9", new GenericEventMethod[]
            {
              new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,-69.61f, 430.87f)
            });

        keyValuePairs.Add("LEVEL2/TRIGGER1", new GenericEventMethod[]
        {
             new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog,NPC_Dictionary.Instance.getNPC("MotherTalk_scene3"))
        });
        keyValuePairs.Add("LEVEL2/TRIGGER2", new GenericEventMethod[]
        {
            new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getNPC("Mother_Trigger"))
        });
        keyValuePairs.Add("LEVEL2/TRIGGER3", new GenericEventMethod[]
       {
          new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getDialogueNPC())
       });

        keyValuePairs.Add("LEVEL3/TRIGGER1", new GenericEventMethod[]
        {
            //wow im dumb.
              new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getDialogueNPC())
        });
        keyValuePairs.Add("LEVEL3/CHANGEPOS1", new GenericEventMethod[]
       {
            new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,-15.23f, 681.88f)
       });
        keyValuePairs.Add("LEVEL3/CHANGEPOS2", new GenericEventMethod[]
      {
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,5.77f, 209.98f)
      });
        keyValuePairs.Add("LEVEL3/CHANGEPOS3", new GenericEventMethod[]
  {
           new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,238.6f, 610.5f)
  });
        keyValuePairs.Add("LEVEL4/TRIGGER1", new GenericEventMethod[]
        {
            new SingleParameterMethod<bool>(PlayerMovement.instance.setIsWalking, false),
            new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Player"),false,"Player_Sleeping", new GenericEventMethod[]
            {
               new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, NPC_Dictionary.Instance.getNPC("Kid_Fortress")), //after the animation, trigger the dialogue
               new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopTrigger, NPC_Dictionary.Instance.getNPC("Trigger_Tired_Animation")),
               new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopTrigger, NPC_Dictionary.Instance.getNPC("Kid_Fortress")), //after the animation, trigger the dialogue
           
            })
        });
       



    }
    public GenericEventMethod[] getGE(string key)
    {
        try
        {
            if (keyValuePairs.ContainsKey(key))
            {
                return keyValuePairs[key];
            }
            else
            {
                throw new NullReferenceException();
            }

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e + "key does not exist");
        }
        return null;

    }

    public void EnqueueEvents(string key)
    {
        try
        {
            if (keyValuePairs.ContainsKey(key))
            {
                GenericEventMethod[] genericEventMethods = keyValuePairs[key];
                Debug.Log("" + key + " has been used and found");
                for (int i = 0; i < genericEventMethods.Length; i++)
                {
                    IEvent.e.twoParamList.Enqueue(genericEventMethods[i]);
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        catch (NullReferenceException e)
        {

            Debug.LogWarning(e + "CANNOT FIND EVENT IN EVENT DICTIOANRY " + key);

        }

    }
    private void Start()
    {
        AddLevel("level1");
    }
    private void Awake()
    {
        Instance = FindObjectOfType<EventDictionary>();
    }


}
