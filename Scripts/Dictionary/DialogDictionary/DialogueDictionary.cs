using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDictionary : MonoBehaviour
{

    public static DialogueDictionary Instance;
    Dictionary<string, DialogueObject[]> dialogueData = new Dictionary<string,DialogueObject[]>();
    public void AddDialogue(string line,DialogueObject[] d)
    {
        dialogueData.Add(line,d);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<DialogueDictionary>();
        }
    }
    private void Begin()
    {
       
        AddLevel1("Level1");
        AddLevel2("Level2");
        AddLevel3("Level3");
        AddLevel4("Level4");
        AddLevelGameplay("Gameplay");
        AddTikTok("TikTok");
        AddTelephone("");
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        Begin();
    }
    private void Start()
    {
        StartCoroutine(LateStart());
    }
    public DialogueObject[] GetDialogueObjectData(string key)
    {
        try
        {
            if (dialogueData.ContainsKey(key))
            {

                return dialogueData[key];
            }
            else
            {
                return null;
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e + " CANNOT FIND DIALOGUE EVENT" + " " + key);
            return null;
        }
      
    }

    private void AddLevel1(string level)
    {
        AddDialogue("TELEPHONE", new DialogueObject[]
        {
            new DialogueObject("Dial a phone number?", new GenericEventMethod[]{  new TwoParameterMethod<string[], GenericEventMethod[][]>(OptionalDialogue.instance.PlayOptionDialogue, new string[] { "Dial", "Don't Dial"}, new GenericEventMethod[][] {
                  // new GenericEventMethod[] { new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue,NPC_Dictionary.Instance.getNPC("Telephone"), "LEVEL1/28/YES", null) } ,
                  new GenericEventMethod[] { new TwoParameterMethod<int,GenericEventMethod[]>(Level_Manager.Instance.ChangeScene, 3, null) },
                new GenericEventMethod[] { new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue, null, "TELEPHONE/NO", null) } }) },false)

        });
        AddDialogue("TELEPHONE/NO", new DialogueObject[]
         {
             new DialogueObject("CHO walks away.", new GenericEventMethod[] {

              new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getDialogueNPC(),"TELEPHONE", null),
             }, true),

         }) ;
    
        AddDialogue("LEVEL1/1", new DialogueObject[] {
         new DialogueObject("Little bit of salt, a little bit of pepper...", new GenericEventMethod[]{ new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomIn, 0.05f, 11f, 12f),
         new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("friend_of_kid_1_1"),false,"Friend_2_Dead",null)


         },true),

         new DialogueObject("Always season your eggs."),
         new DialogueObject("Hey Brother!"),
         new DialogueObject("Brother?"),
         new DialogueObject("You've gone quiet again.", level + "/Hummus-export"),
         new DialogueObject("CHO, you talk to that phone more than you talk to me.", level + "/Hummus-export"),
         new DialogueObject("...", level + "/Hummus-export"),
         new DialogueObject("Are you even listening to me?", level + "/Hummus-export"),
         new DialogueObject("Nobody listens nowadays... ", new GenericEventMethod[] { new SingleParameterMethod<float>(NPC_Manager.Instance.BlackToTrans, 2.5f)},false ),
         new DialogueObject("What is it with everyone! What happened to hope, to religion!", level + "/Hummus-export"),
         new DialogueObject("Don’t get me wrong I understand now desperate things look out there.", level + "/Hummus-export"),
         new DialogueObject("I go outside and see the Sun, its light dims more every passing day.", level + "/Hummus-export"),
         new DialogueObject("But ARISE, my Brother. Since when did people abandon their faith in ARISE!", level + "/Hummus-export"),
         new DialogueObject("Hm. Well on another note, breakfast is ready!", level + "/Hummus-export"),
         new DialogueObject("", new GenericEventMethod[]{new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,1,0),
         new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC,NPC_Dictionary.Instance.getNPC("Brother_1"),-521.6f,762.6f,80f,
            //change dialogue after movment...
            new GenericEventMethod[] {new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Brother_1"),"LEVEL1/4",null),
            new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC,NPC_Dictionary.Instance.getNPC("Brother_1"),-501.1f,770.35f,80f,null)
         }),

         new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeSprite,NPC_Dictionary.Instance.getNPC("Brother_1"),"Resources_Scene1_BrotherCatering"),
         new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Brother_1"),"",null),
         new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeTriggerEvent,NPC_Dictionary.Instance.getNPC("Brother_1"),"")
         },false)

        });

        AddDialogue("LEVEL1/2", new DialogueObject[] {
        new DialogueObject("Poster says:"),
        new DialogueObject("'Have Faith in ARISE.'")
        });

        AddDialogue("LEVEL1/3", new DialogueObject[]
        {
        new DialogueObject("The omelet is cooked to perfection."),
        new DialogueObject("Every strawberry seed is individually picked off.\nCHO has never seen strawberries so red. CHO didn’t know Ivan was such an excellent chef."),
        new DialogueObject("Thank you brother!")
        });
        AddDialogue("LEVEL1/4", new DialogueObject[]
        {
         new DialogueObject("Hm, I think I get it.", level + "/"),
         new DialogueObject("I think I understand how you're feeling...", level + "/"),
         new DialogueObject("A little... lost? Maybe a little confused?", level + "/"),
         new DialogueObject("I've been feeling that way too. Ever since Mother gave me a week to move out, things between you and I feel a little distant.", level + "/"),
         new DialogueObject("Maybe you can join me for communion at the temple today.\n There’s no pressure. You can just drop in and say hello to a couple old friends.",level + "/")
        });
        AddDialogue("LEVEL1/5", new DialogueObject[] //mother talk
        {

        new DialogueObject("Oh, you finally made it out of bed.", level + "/" , new GenericEventMethod[]{new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,2)},true),
        new DialogueObject("I heard Ivan cooking in the kitchen. He really should be spending that energy finding a new place to stay. ", level + "/"),
        new DialogueObject("And this 'religion' he preaches... do you really believe all that?", level + "/", new GenericEventMethod[]{ new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomIn, 0.005f, 11f, 15f)},true),


            new DialogueObject("If you want to go to his little 'preacher party', so be it. ", level + "/"),
        new DialogueObject("Just listen to your Mother while I'm still here. Reality can hurt, but reality is the truth. ", level + "/"),
        new DialogueObject("..."),
        new DialogueObject("Listen CHO - your brother cannot be trusted. Stay away from his senseless clan.", level + "/"),
        new DialogueObject("I've said too much... Our neighbour Darnel, he's looking for you.", level + "/"),
        new DialogueObject("",  new GenericEventMethod[]{
            new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,2,0),
             new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Mother_Trigger"),"", null),
             new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Mother"),"LEVEL1/5PT2", null),
            new TwoParameterMethod<NPC, string>(NPC_Manager.Instance.ChangeTriggerEvent,NPC_Dictionary.Instance.getNPC("Mother_Trigger"),""),
        },false)

        });

      
      
        AddDialogue("LEVEL1/5PT2", new DialogueObject[] {
            new DialogueObject("Talk to Darnel, he's looking for you.\nI think he's outside.")
        });

        AddDialogue("LEVEL1/6", new DialogueObject[]
      {
            new DialogueObject("Mother loves keeping track of time."),
            new DialogueObject("She's a very no-nonsense person."),
            new DialogueObject("Watch the time go by.\n Enjoy every second.")
      });
        AddDialogue("LEVEL1/7", new DialogueObject[]
        {
          new DialogueObject("A calender."),
          new DialogueObject("There is a red circle drawn on the 30th."),
          new DialogueObject("Today is the 29th.")
        });

        AddDialogue("LEVEL1/7.5", new DialogueObject[]
            {
                new DialogueObject("Don't touch my cigarettes CHO!"),
                new DialogueObject("I'm saving them for tomorrow."),
                new DialogueObject("Things are stressful enough as they are.")
            });


        AddDialogue("LEVEL1/8", new DialogueObject[] //mother dialogue #2
       {
         new DialogueObject("Focus gentlemen! We must locate it before he returns. IT'S VITAL! ", level + "/", new GenericEventMethod[]
         {
               new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,13),
         },true),
         new DialogueObject("...Um sir...", level + "/"),
         new DialogueObject("What is it?"),
         new DialogueObject("I found the journal. The great sun must be with me today!", level + "/"),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,13,8),
         },true),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,8,9),
         },true),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,9,10),
         },true),
         new DialogueObject("Well, open his journal!", level + "/",new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,10,13),
         },true),
         new DialogueObject("Let's see here... The journal is called 'Squirrel Hunting'.", level + "/"),
         new DialogueObject("Hm... Hard cover... Nice.", level + "/"),
         new DialogueObject("Wait, squirrel hunting?", level + "/"),
         new DialogueObject("'Squirrel Hunting' by Don T Hamster.", level + "/"),
         new DialogueObject("Published in-", level + "/"),
         new DialogueObject("Well then it's not the journal you idiot!", level + "/"),
         new DialogueObject("But what if the journal is about squirrels? ", level + "/"),
         new DialogueObject("Oh by the almighty sun!", level + "/"),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,13,8),
         },true),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,8,9),
         },true),
         new DialogueObject("... Yes, All hail ARISE.", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,9,10),
         },true),
         new DialogueObject("Remember dummy, we're looking for a journal that has a 10 digit phone number on the first page. Master Ivan was very-", level + "/", new GenericEventMethod[]
         {
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,10,0),
         },true),
         new DialogueObject("Oh... Hey....", level + "/"),
         new DialogueObject("What-"),
         new DialogueObject("AHH! Oh- Um, why hello CHO! How- erm, how long have you been standing there? Heh. Heh.", level + "/"),
         new DialogueObject("Well you see, we were tasked with retrieving one your brother's books.", level + "/"),
         new DialogueObject("Ivan's your brother?... really?.. Are you adopted?!"),
         new DialogueObject("That is not of importance right now! CHO, we are looking for a book... It's name was...", level + "/"),
         new DialogueObject("Squirrels."),
         new DialogueObject("That's right! We were looking for 'Squirrel Hunting' by..."),
         new DialogueObject("Don T Hampste-"),
         new DialogueObject("By Donny Hampton! Those critters always get in your gutters and make a mess.", level + "/"),
         new DialogueObject("Apoligies for any mi'SUN'derstanding CHO.", level + "/"),
         new DialogueObject("All...", level + "/", new GenericEventMethod[]
         {
           new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,8)
         },true),
         new DialogueObject("Hail..?", level + "/"),
         new DialogueObject("Sir... You know we must hail ARISE whenever the sun is mentioned-", level + "/", new GenericEventMethod[]
         {
           new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,8,9)
         },true),
         new DialogueObject("YES OF COURSE I KNOW THAT DON'T YOU THINK I-", level + "/", new GenericEventMethod[]
         {
              new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,9,10)
         },true),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {
              new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,10,8)
         },true),
          new DialogueObject("IT DOESN'T COUNT!", level + "/", new GenericEventMethod[]
          {
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,8,10)
          },true),
         new DialogueObject("All hail ARISE.", level + "/", new GenericEventMethod[]
         {

              new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,10,9)
         },true),
         new DialogueObject("FINE, \n...idiots...\n All hail ARISE.", level + "/", new GenericEventMethod[]
         {
              new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,9,0),
             },false),
         new DialogueObject("", new GenericEventMethod[]
         {
                  new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Clansmen_Trigger"),"",null)
         }, false)
       } 
       ) ;
        AddDialogue("LEVEL1/9", new DialogueObject[] //mother dialogue #2
        {
         new DialogueObject("Squirrel hunting.", level + "/"),
         new DialogueObject("That has hobby potential.", level + "/"),
        });

        AddDialogue("LEVEL1/10", new DialogueObject[]
        {
            //soph
        new DialogueObject("If you find a misplaced book around the house, returning it to us would be a great gesture.", level + "/"),
        });
        AddDialogue("LEVEL1/11", new DialogueObject[]
        {
            new DialogueObject("A Journal.", new GenericEventMethod[]
            {
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,14),
            },false),
            new DialogueObject("It says 'For CHO'."),
            new DialogueObject("CHO takes it.", new GenericEventMethod[]
            {
                new SingleParameterMethod<PageData>(Journal_Manager.Instance.AddPage, new PageData("translation -> 1",Resources.Load<Sprite>("NPCSprite/Resources_Scene1_BrotherCatering"))),
            },false),
            new DialogueObject("*Press tab to interact with the journal.*"),
             new DialogueObject("", new GenericEventMethod[]
             {
                 new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,14,0),
                  new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getNPC("journal"))
             },false),

        }
        );
        AddDialogue("LEVEL1/12", new DialogueObject[]
        {
            //soph
            new DialogueObject("Flashlights."),
            new DialogueObject("Only use them when it's dark outside.")
        });
        AddDialogue("LEVEL1/13", new DialogueObject[]
        {
            new DialogueObject("CHO! CHO!"),
            new DialogueObject("I found something!"),
            new DialogueObject("Try to keep up with me!"),
            new DialogueObject("",new GenericEventMethod[] {
            new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc,NPC_Dictionary.Instance.getNPC("kid_barrier")),
            new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,3),
             new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomIn,2,5,50),
             new FiveParameterMethod<NPC, float, float,float, GenericEventMethod[]>
            (NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Kid"), -27.72f, 64.7f, 17,
            new GenericEventMethod[] {
            new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,3,0),
            new FiveParameterMethod<NPC, float, float,float, GenericEventMethod[]>
            (NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Kid"), -165f, 64.7f, 25f, null),

             new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>
            (NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Kid"),true,"Kid_Move_Left",null),
            })},false)
        });
        AddDialogue("LEVEL1/14", new DialogueObject[]
        {
         new DialogueObject("CHO! I need to talk to you!",
         //
         new GenericEventMethod[]{
         new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,3)
         }
         ,
         true
         ),
         new DialogueObject("",
         new GenericEventMethod[]{
             new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,3,0)
         }, true)
        });
        AddDialogue("LEVEL1/15", new DialogueObject[]
        {
         new DialogueObject("CHO checks the man's pulse."),
         new DialogueObject("He's dead."),
         new DialogueObject("The kid didn't seem to mind.")
        });

        AddDialogue("LEVEL1/16", new DialogueObject[]
        {
            new DialogueObject("Check it out, it's a strawberry bush!"),
            new DialogueObject("What happend to all the fresh fruit? They are so hard to come by now!"),
            new DialogueObject("Does it have to do with the sun? My mom is worried."),
            new DialogueObject("She hugged me extra hard today when I mentioned it. "),
            new DialogueObject("Why does the world always hide stuff from kids?"),
            new DialogueObject("What else is this place hiding?",0.5f),
            new DialogueObject("",new GenericEventMethod[]{
                 //playermoves
                new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation, NPC_Dictionary.Instance.getNPC("Kid_1"),false,"Kid_Move_Left", null),
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Kid_1"),"LEVEL1/16/PT2",null),
                new FiveParameterMethod<NPC, float, float,float, GenericEventMethod[]>
                (NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Kid_1"), -164.21f, 11.84f, 10, new GenericEventMethod[]
                {
                  new TwoParameterMethod<NPC,GenericEventMethod[]>(NPC_Manager.Instance.StopAnimation, NPC_Dictionary.Instance.getNPC("Kid_1"),null)
                })
            },
            false)
        });
        AddDialogue("LEVEL1/16/PT2", new DialogueObject[]
        {
            new DialogueObject("What else is this place hiding?", 0.2f)
        });


        AddDialogue("LEVEL1/17", new DialogueObject[]
        {
             new DialogueObject("68 hours... \n5 minutes... \n19 seconds..."),
             new DialogueObject("68 hours... \n5 minutes... \n18 seconds..."),
             new DialogueObject("68 hours... \n5 minutes... \n17 seconds..."),
             new DialogueObject("Just counting down Xeno's final hours."),
             new DialogueObject("Couldn't care less what those ARISE Clansmen preach.")

        });

        AddDialogue("LEVEL1/18", new DialogueObject[]
        {
         new DialogueObject("The tree of Xeno."),
         new DialogueObject("The tree's vitality is a represenation of Xeno's power."),
          new DialogueObject("She is very wise but the branches have begun to rot and the leaves wither away."),
        });

        AddDialogue("LEVEL1/19", new DialogueObject[]
       {
        new DialogueObject("I thought there'd be chaos. It's awfully quiet."),
        new DialogueObject("With every passing day, more people convert to ARISE."),
        new DialogueObject("I think everyone is in disbelief. Or the religion your brother leads holds some truth."),
        new DialogueObject("I doubt it.")
       });
        AddDialogue("LEVEL1/20", new DialogueObject[]
       {
         new DialogueObject("Great sun! Thank you for gifting us with your glorious blood!"),
         new DialogueObject("All hail ARISE."),
         new DialogueObject("AHAHA!")
         
       });
        AddDialogue("LEVEL1/21", new DialogueObject[]
        {
         new DialogueObject("These Clansmen, they make vandalism no fun."),
         new DialogueObject("Oh Great sun thank you for sacrificing another body for our great cause!"),
         new DialogueObject("CHO, why are these Clansmen so involved with their religion?"),
         new DialogueObject("TJ get up.", new GenericEventMethod[]
         {
           new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("friend_of_kid_1_1"),false,"Idle",null)
         },false),
         new DialogueObject("OH!- OH my goodness the great sun has saved this poor boy!"), //get up animation.
         new DialogueObject("No. No it didn't."),
         new DialogueObject("And weren't you happy he was dead just a minute ago?"),
         new DialogueObject("Let's all dance in a circle and reciting ancient prayers. It will be fun!"),
         new DialogueObject("Can't believe we just wasted a liter of ketchup on this guy."),
         new DialogueObject("You think that man with the cane has any more ketchup bottles?"),
         new DialogueObject("If he forgets his line again I'm just there's a chance!"),
         new DialogueObject("Let's get outta here. These Clansmen freak me out."),
         new DialogueObject("", new GenericEventMethod[]{
             //move all characters ketchup scene....
           new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Kid_2"),false,"Kid_Move_Up",null),
           new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("friend_of_kid_1_1"),false,"Friend_1_Move_Left",null),
           new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("friend_of_kid_2_1"),false,"Friend_2_Move_Right",null),
          
           //
             new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Kid_2"), -40.2f, 300f, 40,null),
           new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("friend_of_kid_1_1"), -217f, 200f, 60,null), 
           new FiveParameterMethod<NPC, float, float, float, GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("friend_of_kid_2_1"), -185f, 201.37f, 60, null)},false)
         

       });
        AddDialogue("LEVEL1/22", new DialogueObject[]
       {
          new DialogueObject("Our leader must have his hedges looking perfect. PERFECT!"),
          new DialogueObject("PERFECT!"),
       });
        AddDialogue("LEVEL1/23", new DialogueObject[]
      {
         new DialogueObject("Excuse me sir, some hooligans have stolen the town's ketchup!\n What has this..."),
         new DialogueObject("What has this world..."),
         new DialogueObject("...this world.. uh"),
         new DialogueObject("Oh, you must forgive me Christopher. I have forgotten my assigned line! The brain tends to get a bit fuzzy with age."),
         new DialogueObject("Don't go just yet, I'm sure I can think of something..."),
         new DialogueObject("'Isn't this planet just beautiful!'", new GenericEventMethod[]
         {
             new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomIn,7f,85,100f)

         },false),
         new DialogueObject("But I must say, it's a little cold. A hot cup of tea would be lovely right now. You wouldn't happen to have a phone number for a cafe, would you?", new GenericEventMethod[]
         {
             new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomIn,0.07f,25f,25f),

         },false),
         new DialogueObject("Darn, I thought maybe you would with that telephone that's always stalking you."),
          new DialogueObject("Oh! What's that falling from the sky?", 0.5f, new GenericEventMethod[]{
              new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Anchor"), 30, null),
              new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Old_Man"),"LEVEL1/23/PT1",null)
          },false),
      }) ;
        AddDialogue("LEVEL1/23/PT1", new DialogueObject[]
        {
            new DialogueObject("For some reason the old man doesn't want to talk.")
        });
        AddDialogue("LEVEL1/24/QUESTION", new DialogueObject[]
       {
          new DialogueObject("Talk to the tree?", new GenericEventMethod[]
          {
              new TwoParameterMethod<string[], GenericEventMethod[][]>(OptionalDialogue.instance.PlayOptionDialogue, new string[] { "yes", "no"}, new GenericEventMethod[][] {
                   new GenericEventMethod[] { new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue,NPC_Dictionary.Instance.getNPC("main tree"), "LEVEL1/24/YES", null) } ,
                   new GenericEventMethod[] { new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue, NPC_Dictionary.Instance.getNPC("main tree"), "LEVEL1/24/NO", null) } })
          }, true)
       });
        AddDialogue("LEVEL1/24/YES", new DialogueObject[]
        {
            new DialogueObject("It doesn't feel like talking.")
        });
        AddDialogue("LEVEL1/24/NO", new DialogueObject[]
        {
            new DialogueObject("Only idiots talk to trees."),
            new DialogueObject("CHO walks away.",  new GenericEventMethod[]{
               new ThreeParameterMethod<NPC,string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("main tree"), "LEVEL1/24/QUESTION", null),
            },true)
        });
        AddDialogue("LEVEL1/24.5", new DialogueObject[]
        {
         new DialogueObject("Ketchup.")
        });
        AddDialogue("LEVEL1/24.5/PT2", new DialogueObject[]
       {
         new DialogueObject("Ketchup."),
         new DialogueObject("Probably.")
       });

        AddDialogue("LEVEL1/25", new DialogueObject[]
       {
          new DialogueObject("Hey CHO what’s up? Are you free to talk?"),
          new DialogueObject("Familiar children's whispers linger from somewhere above but it is not clear from where."),
           new DialogueObject("*Hehehe.*",0.01f),
          new DialogueObject("*Sh- Quiet!*",0.01f),
          new DialogueObject("*Ow! That’s my foot!*"),
          new DialogueObject("You want to know something crazy?!"),
          new DialogueObject("I captured a ghost!"),
          new DialogueObject("Oouuuuhhhh! (ghost noises)"),
          new DialogueObject("He’s astonished! Look at his face!", new GenericEventMethod[]
          {
              new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomInPriority,25,15,25)
          },false),
          new DialogueObject("ATTACK!", new GenericEventMethod[]
          {
                new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomInPriority,25,0,100),

              new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("egg_NPC_endTalk"), -68.5f, 381.57f, 80f,new GenericEventMethod[]
              {
               new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("egg_NPC_endTalk"),false,"Egg_splat",null),
               new SingleParameterMethod<CanvasComp>(UI_Manager.Instance.PlayParticleSystem,CanvasComp_Dictionary.Instance.getCanvasComp("egg_NPC_endTalk"))
              }),
              new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("egg_NPC_endTalk1"), -65.5f, 384.07f, 83f,new GenericEventMethod[]
              {
               new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("egg_NPC_endTalk1"),false,"Egg_splat",null),
               new SingleParameterMethod<CanvasComp>(UI_Manager.Instance.PlayParticleSystem,CanvasComp_Dictionary.Instance.getCanvasComp("egg_NPC_endTalk1"))
              }),
              new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("egg_NPC_endTalk2"), -59.5f, 381.57f, 85f,new GenericEventMethod[]
              {
               new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("egg_NPC_endTalk2"),false,"Egg_splat",null),
               new SingleParameterMethod<CanvasComp>(UI_Manager.Instance.PlayParticleSystem,CanvasComp_Dictionary.Instance.getCanvasComp("egg_NPC_endTalk2"))
              }),


          },false),
          new DialogueObject("Hey! That was my egg!"),
          new DialogueObject("Your eggs are on the right side of the carton?"),
          new DialogueObject("No! I’m on the right side! You're on the left side!"),
          new DialogueObject("Then how come I only have 3 eggs to your six!"),
          new DialogueObject("uh. guys?"),
          new DialogueObject("Remember when you said you wanted to save them to start a chicken farm?"),
          new DialogueObject("Y’know what! I don't even want to do this anymore!"),
          new DialogueObject("I’ll raise those chickens real good! A coup and everything! Just watch me!",

          new GenericEventMethod[]
          {
              //friend of kid 2 walks out 
              new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Friend1_Psych_Room"), -82.3f, 382.8f, 100f,null) //<- possibly make an animation.
          }

          ,false),
          new DialogueObject("...uh"),
          new DialogueObject("Secret hideout has been breached!"),
          new DialogueObject("", new GenericEventMethod[]
          {
            new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Kid_endTalk"),false,"Kid_Move_Up",null),
            new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Kid_endTalk"), -71.8f, 613f, 63f,null),

            new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("tree_moveLeft_NPC"), -87f, 397.6104f, 20f,null),
              new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("tree_glitch_moveRight_NPC"), -57f, 397.6104f, 20f,null)


          },false),
       }) ;


        AddDialogue("LEVEL1/26", new DialogueObject[]
        {
            new DialogueObject("-")
        });
        AddDialogue("LEVEL1/26/QUESTION", new DialogueObject[]
        {
            new DialogueObject("Talk to Telephone?"),
        });
        AddDialogue("LEVEL1/26/YES", new DialogueObject[]
        {
        });
        AddDialogue("LEVEL1/26/NO", new DialogueObject[]
        {
             new DialogueObject("CHO leaves...")
        });
        AddDialogue("LEVEL1/27", new DialogueObject[]
        {
         new DialogueObject("Secret fort has been breached!"),
         new DialogueObject("Hm. What now? find a new spot?"),
         new DialogueObject("Your not gonna tell anyone right?"),
         new DialogueObject("..."),
         new DialogueObject("Not much of a talker?"),
         new DialogueObject("..."),
         new DialogueObject("Yeah we're good", 0.01f),

        });
        AddDialogue("LEVEL1/28.5", new DialogueObject[]
            {
         new DialogueObject("When we get bored of throwing eggs at strangers, we spray them with ketchup."),
         new DialogueObject("The secret fortress is a great place to store this stuff."),
         new DialogueObject("Your our first outsider. Welcome.")
            });
        AddDialogue("LEVEL1/28/QUESTION", new DialogueObject[]
        {
          //kid talk
           new DialogueObject("Talk to the kid?", new GenericEventMethod[]
           {
               new TwoParameterMethod<string[], GenericEventMethod[][]>(OptionalDialogue.instance.PlayOptionDialogue, new string[] { "yes", "no"}, new GenericEventMethod[][] {
                   new GenericEventMethod[] { new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue,NPC_Dictionary.Instance.getNPC("kid_Psych_Room"), "LEVEL1/28/YES", null) } ,
                   new GenericEventMethod[] { new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue, NPC_Dictionary.Instance.getNPC("kid_Psych_Room"), "LEVEL1/28/NO", null) } })
           }, true)

        });
        AddDialogue("LEVEL1/28/YES", new DialogueObject[]
        {
          new DialogueObject("I don't know how I did it... the telephone just asked me to walk and I started walking in a direction I never thought I could go."),
          new DialogueObject("Then I ended up here."),
          new DialogueObject("My friends call it the Secret Fortress. \nMaybe they fell protected from the outside."),
          new DialogueObject("It's relaxing to get away from it all."),
          new DialogueObject("To get away from all the suffering."),
          new DialogueObject("I wonder how far this place goes on for?"),
          new DialogueObject("Maybe I'll keep walking and see how far I can go."),
          new DialogueObject("I wonder what else Xeno is hiding from me."),
          new DialogueObject("CHO."),
          new DialogueObject("If you wish to see the uncharted..."),
          new DialogueObject("call this number."),
          new DialogueObject("*The kid gives you his phonenumber.*"),
          new DialogueObject("*journal has been updated*"),
          new DialogueObject("", new GenericEventMethod[]{
              new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,-69.61f, 430.87f) }, true)
        });
        AddDialogue("LEVEL1/28/NO", new DialogueObject[]
       {
           new DialogueObject("CHO leaves...", new GenericEventMethod[]
           {
               new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue, NPC_Dictionary.Instance.getDialogueNPC(),"LEVEL1/28/QUESTION", null)
           },true)
       });

        AddDialogue("LEVEL1/29", new DialogueObject[]
        {
        new DialogueObject("Whaaaaatttt."),
        new DialogueObject("You've never seen a cat with a corndog."),
        new DialogueObject("So typicalllll. ")
        });

        AddDialogue("LEVEL1/30", new DialogueObject[]
        {
         new DialogueObject("If you want a drink go  some place else."),
         new DialogueObject("Wait a minute. That you CHO?! Good to see you!"),
         new DialogueObject("Gee this bar is older than your old man. Can you believe that!"),
         new DialogueObject("My dad won it playing high stakes poker."),
         new DialogueObject("He'd lose his truck if he’d lost the bet."),
         new DialogueObject("Best part was he owned no car! Just pointed to the truck out the window an’ said it was his!"),
         new DialogueObject("..."),
         new DialogueObject("There was once a real community at this bar."),
         new DialogueObject("But that sun I tell ya."),
         new DialogueObject("That sun drains the heart and soul out of these people. Has me thinking too."),
         new DialogueObject("Now all this bar has is nothing but drunks, drinking like there’s no tomorrow. "),
         new DialogueObject("And a cat, eating a corndog. He seems a little out of place don't you think? ", new GenericEventMethod[]
         {
             new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,7)
         },true),
         new DialogueObject("I’m done poisoning these people. This bar is closing down tommorow. Permanetly. ", new GenericEventMethod[]
         {
             new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,7,0),
           //  new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomIn,0.4f,0.4f,5f)
         },true)
        }) ;
        AddDialogue("LEVEL1/31", new DialogueObject[]
       {
         new DialogueObject("You wanna fight? HUH?"),
         new DialogueObject("Wanna tussle with the muscle?"),
          new DialogueObject("COME AT ME!")
       });
        AddDialogue("LEVEL1/32", new DialogueObject[]
      {
        new DialogueObject("Gee boss I really think our journal looks a lot better!"),
        new DialogueObject("I like the stickers, the stickers added a nice touch."),
        new DialogueObject("Get out! All of you!"),
        new DialogueObject("", new GenericEventMethod[]{
            //
        new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,4),
            new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeTriggerEvent,NPC_Dictionary.Instance.getNPC("Brother_is_mad_talk"),""),
            new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Brother_is_mad_talk"),"",null),
        new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("clan_g_1"), 247.0f, 940f, 70,null),
        new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("clan_g_2"), 247.0f, 935f, 70,null),
        new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("clan_g_3"), 247.0f, 943.7f, 70,new GenericEventMethod[]{
        new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,4,0)
        }),
        }
        ,false)
      }) ;
        AddDialogue("LEVEL1/33", new DialogueObject[]
        {
             new DialogueObject("I am sorry you saw me lash out like that."),
             new DialogueObject("And I apologize if any ARISE member has ever caused you any stress."),
             new DialogueObject("ARISE members are often senseless."),
             new DialogueObject("... These times CHO... these times"),
             new DialogueObject("They make you do crazy things"),
             new DialogueObject("Please, follow me."),
             new DialogueObject("", new GenericEventMethod[]{
             new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,5),
             new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC,NPC_Dictionary.Instance.getNPC("Brother_look_at_Image"),347f,850f,70f,new GenericEventMethod[] {
                   new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,5,0)
             })


             }, false),
        }) ;
        AddDialogue("LEVEL1/34/PT1", new DialogueObject[]
        {
            new DialogueObject("Ah, you remembers your old paintings CHO?, you were quite the artist!"),
            new DialogueObject("You always had a knack at seeing things clearly", new GenericEventMethod[]{
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue, NPC_Dictionary.Instance.getNPC("Brother_prayer"), "LEVEL1/34/PT2", null)
            },false),
            new DialogueObject("CHO. Talk to me when your ready for prayer.")
        });

        AddDialogue("LEVEL1/34/PT2", new DialogueObject[]
       {
            new DialogueObject("..."),
            new DialogueObject("Have you ever prayed to the sun?"),
            new DialogueObject("Its a simple endevor."),
            new DialogueObject("All that it requires..."),
            new DialogueObject("Is faith.", new GenericEventMethod[]{

               //garage object falling...
                //#1


                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj1"),1f, new GenericEventMethod[]
                {
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj2"),1f, null),
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj3"),0.1f,null),
                }),
                //#2
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj4"),1f, new GenericEventMethod[]
                {
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj5"),1f, null),
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj6"),0.5f,null),
                }),
                //#3
                 new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj7"),1f, new GenericEventMethod[]
                {
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj8"),1f, null),
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj9"),0.1f,null),
                }),
                //#4
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj10"),1f, new GenericEventMethod[]
                {
                new ThreeParameterMethod<NPC,float,GenericEventMethod[]>(NPC_Manager.Instance.ChangeGravityScale,NPC_Dictionary.Instance.getNPC("Gravity_Scale_Obj11"),0.05f, null),
               
                }),
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue, NPC_Dictionary.Instance.getNPC("Brother_prayer"),"LEVEL1/34/PT3", null)
                },false),

    

       }) ;
        AddDialogue("LEVEL1/34/PT3", new DialogueObject[]
        {
            new DialogueObject("Do you know the essence of ARISE?"),
            new DialogueObject("It is a religion that spread throughout our planet Xeno"),
            new DialogueObject("It is prophicied that on the day our great sun emits its last flame..."),
            new DialogueObject("It will select a brave warrior..."),
            new DialogueObject("This warrior is said to sacrifice himself to the great sun."),
            new DialogueObject("And only then will the sun warm up once again\nXeno will be saved."),
            new DialogueObject("I believe the sun has chosen me."),
            new DialogueObject("I might not see you for a long time brother. But. It's my duty to save these people."),
            new DialogueObject("Do you feel its soothing warmth?"),
            new DialogueObject("Relax you mind brother."),
            new DialogueObject("Let it alleviate your stress.", 0.03f),
            new DialogueObject("Relax...",0.04f),
            new DialogueObject("Find that inner comfort Christopher", 0.01f, new GenericEventMethod[]{

                new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("sun"), 393f, 961.4f, 20,null),
                new SingleParameterMethod<CanvasComp>(UI_Manager.Instance.PlayParticleSystem,CanvasComp_Dictionary.Instance.getCanvasComp("TV_effect")),
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue, NPC_Dictionary.Instance.getNPC("Brother_prayer"), "LEVEL1/34/PT4", null),
               new ThreeParameterMethod<NPC,float,float>(NPC_Manager.Instance.ScaleNPC,NPC_Dictionary.Instance.getNPC("sun"), 5f, 4.5f),
               },false)
        }) ;
        AddDialogue("LEVEL1/34/PT4", new DialogueObject[]
       {
            new DialogueObject("Hm.", new GenericEventMethod[]
            {
                new NoParameterMethod(Journal_Manager.Instance.BrotherOpenJournal)
            },false),
            new DialogueObject("Trust the chil-", 0.01f, new GenericEventMethod[]{
            new ThreeParameterMethod<NPC,float,float>(NPC_Manager.Instance.ScaleNPC,NPC_Dictionary.Instance.getNPC("sun"), 5f, 3f),
            },false),
            new DialogueObject("", new GenericEventMethod[]
            {
               
               new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeTriggerEvent, NPC_Dictionary.Instance.getNPC("ChangePos5"),"LEVEL1/CHANGEPOS8"),
              new TwoParameterMethod<float,float>(Change_Position_Manager.Instance.MovePlayer,810.3f, 934.7f),
              new SingleParameterMethod<CanvasComp>(UI_Manager.Instance.StopParticleSystem,CanvasComp_Dictionary.Instance.getCanvasComp("TV_effect")),
               new NoParameterMethod(Journal_Manager.Instance.ExitJournal),
            },true)
       });
        AddDialogue("LEVEL1/34/QUESTION", new DialogueObject[]
        {
            new DialogueObject("Speak to the tree?"),
        });
        AddDialogue("LEVEL1/34/YES", new DialogueObject[]
        {
             new DialogueObject("...Nothing happens."),
        });
        AddDialogue("LEVEL1/35", new DialogueObject[]
        {
            new DialogueObject("CHO!"),
            new DialogueObject("You finally made it out of bed.",
            new GenericEventMethod[]
            {
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,11),
                new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Mother_scene3"), -868.9f, 775.6f, 45f,null)
            },
            false
            ),
            new DialogueObject("?\n What is this place?"),
            new DialogueObject("", new GenericEventMethod[]
            {
            new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,11,0),
            new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopDialogue, NPC_Dictionary.Instance.getNPC("MotherTalk_scene3")),
            new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Mother_scene3"),false,"Mother_Taken",
            new GenericEventMethod[]
            {
                  new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Mother_scene3"), -745f, 782.2f, 300f,null),
                  new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc,NPC_Dictionary.Instance.getNPC("MotherTalk_scene3"))
            }
            )
            },
            false
            ),

        });
        AddDialogue("LEVEL1/36", new DialogueObject[]
        {
         new DialogueObject("*CHO hears to kid in the distance...*"),
         new DialogueObject("I didn't know! Hey that hurts!"),
         new DialogueObject("", new GenericEventMethod[]{

            new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getNPC("Kid_Help_1"))

         },false)

        });
        AddDialogue("LEVEL1/37", new DialogueObject[]
     {
         new DialogueObject("Let go of me!"),
         new DialogueObject("CHO help!"),
         new DialogueObject("", new GenericEventMethod[]{

            new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getNPC("Kid_Help_2"))

         },false)
     });






    }

    private void AddLevel2(string level)
    {
        AddDialogue("LEVEL2/1", new DialogueObject[] {
            new DialogueObject("CHO! thank goodness your here!"),
            new DialogueObject("Its Darnel! He's gone!"),
            new DialogueObject("We searched about everywhere."),
            new DialogueObject("And nobody will tell us!..."),
            new DialogueObject("Not even the old guy with memory problems!")

        });

        AddDialogue("LEVEL2/2", new DialogueObject[] //mom talk
        {
           new DialogueObject("...",new GenericEventMethod[]{

                 new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopDialogue, NPC_Dictionary.Instance.getNPC("Brother_1")),
                 new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopTrigger, NPC_Dictionary.Instance.getNPC("Brother_1")),
               new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,2),

           },false),
           new DialogueObject("Hey brother would you like eggs with-", new GenericEventMethod[]
           {
              new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Brother_1"), -471.2f, 763.1f, 45f,null),
              new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,2,5),
           },false),
           new DialogueObject("?"),
           new DialogueObject("Have you seen mother?"),
           new DialogueObject("Hm. Knowing her she's probably out drinking."),
           new DialogueObject("I will go looking for her before communion.", new GenericEventMethod[]
           {
               new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,5,0),
               new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopDialogue, NPC_Dictionary.Instance.getNPC("Mother_Trigger")),
              new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopTrigger, NPC_Dictionary.Instance.getNPC("Mother_Trigger")),

           },false)
        });

        AddDialogue("LEVEL2/3", new DialogueObject[]
         {
            new DialogueObject("A page."),
            new DialogueObject("Cho adds it to his Journal."),
            new DialogueObject("",  new GenericEventMethod[]
            {
                new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getDialogueNPC())
            }, false)
            //add page
            
         });

        AddDialogue("LEVEL2/4", new DialogueObject[] 
         {
            new DialogueObject("Wow."),
            new DialogueObject("I don't remember this last time I saw my room.")
         });

        AddDialogue("LEVEL2/5", new DialogueObject[] 
         {
            new DialogueObject("20 hours, 31 minutes, 14 seconds..."),
            new DialogueObject("20 hours, 31 minutes, 13 seconds..."),
            new DialogueObject("20 hours, 31 minutes, 12 seconds... Until ARISE saves us all!")
         });

        AddDialogue("LEVEL2/6", new DialogueObject[] 
        {
            new DialogueObject("A shame the bartender is letting go of the place."),
            new DialogueObject("He seems like a real character."),
            new DialogueObject("Maybe it's a little off topic but you should really join us at the temple today!"),
        });

        AddDialogue("LEVEL2/7", new DialogueObject[]
       {
           new DialogueObject("f^dkf#dl*as3x1as&l0s"),
           new DialogueObject("dkfhsic54*&3$@d?????"),
           new DialogueObject("..."),
           new DialogueObject("????"),
           new DialogueObject("?",level + "beer."),
       });
        AddDialogue("LEVEL2/7/PT1", new DialogueObject[]
        {
           new DialogueObject("???"),
           new DialogueObject("dshsdflhsdf?"),
           new DialogueObject(":)"),
           new DialogueObject(";)"),
           new DialogueObject(":)"),
           new DialogueObject("!"),
           new DialogueObject("Excuse me sir"),
           new DialogueObject("Your going to have to come with me."),
           new DialogueObject("??"),
           new DialogueObject("Come on buddy let's go."),

        });
        AddDialogue("LEVEL2/8", new DialogueObject[]
        {
            new DialogueObject("The tree whispers things to me..."),
            new DialogueObject("IT'S A CONSPIRATOR!!!")
        });
        AddDialogue("LEVEL2/9", new DialogueObject[]
       {
           new DialogueObject("Would you like a serving?"),
           new DialogueObject("Wow I feel really good."),
           new DialogueObject("You won't believe where we found these bottles"),
           new DialogueObject("These kids hid them in the secret"),
           new DialogueObject("AHEM!", new GenericEventMethod[]
           {
               new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,12)
           },false),
           new DialogueObject("Hm?"),
           new DialogueObject("Come here.", new GenericEventMethod[]
           {
               new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Clansmen_officer"), 32.2f, 219.27f, 25f,null)

           },false),
           new DialogueObject("*the officer's voice is faint*"),
           new DialogueObject("Wha... ...anchor... you cannot!.."),
           new DialogueObject("Ah that's right!", new GenericEventMethod[]
           {
               new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,12,0)
           },false),
           new DialogueObject("Well so long Christopher!")

            
       });
        AddDialogue("LEVEL2/10", new DialogueObject[]
        {
            new DialogueObject("The tree whispers things to me..."),
           new DialogueObject("IT'S A CONSPIRATOR")
        });

        AddDialogue("LEVEL2/11", new DialogueObject[]
       {
           new DialogueObject("Hey man! You got a uh..."),
           new DialogueObject("What's that called with the wire and prongs?"),
           new DialogueObject("My truck needs a hit of something"),
           new DialogueObject("Sorry for the inconvience sir. For blocking the road and everything.")
       });

        AddDialogue("LEVEL2/12", new DialogueObject[]
        {
            new DialogueObject("Whaaaaatttt."),
            new DialogueObject("You've never seen a cat with a corndog."),
            new DialogueObject("So typicalllll. "),
            new DialogueObject("Why is this counch so uncomfortable."),
            new DialogueObject("Excuse me sir. I'm with the ARISE crime department.", new GenericEventMethod[]
            {
            new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Clansmen_officer_2"), -166.5f, 778.9f, 10,null)
            },false),
            new DialogueObject("We got a call about a cat slamming corndogs."),
            new DialogueObject("Well...I can't have that happen. Are you the cat people are talking about?", 0.05f),
            new DialogueObject("Well I identify as Felis Catus with a bad eating habit."),
            new DialogueObject("Oh."),
            new DialogueObject("Well, your coming with me."),
            new DialogueObject("Youuuuu gotta be KITTEN men"),
            new DialogueObject("oh. cat pun.")
        });


        AddDialogue("LEVEL2/13", new DialogueObject[]
        {
            new DialogueObject("Mom isn't here either?"),
            new DialogueObject("This... is not right.")
           
        });

        AddDialogue("LEVEL2/14", new DialogueObject[]
        {
            new DialogueObject("Geez, heh. Y'know what kid, I should be more devistated with that sun dying and my father's bar closin'. I really should."),
            new DialogueObject("Listen, maybe keep this between me and you."),
            new DialogueObject("But I've been having these dreams where I see myself opening these doors again."),
            new DialogueObject("Maybe it's my age, but I see things... fitting into place. I hope you can get that feeling too."),
            new DialogueObject("Hey, I forgot to mention someone walked into this bar today an' to me to give you their number. "),
            new DialogueObject("Didn't get their name... but she was wearin' a distinctly green jacket and black top hat. That's all I can picture in my small head."),
            new DialogueObject("Heh, drinks can do that to ya."),
            new DialogueObject("How about giving her a call. Maybe she'll make things fit into place for you on this lonely day."),
            new DialogueObject("I remember being your age... feeling so small in this empty world. Would been nice to have someone by my side."),
            new DialogueObject("This town needs love.")
        });


        AddDialogue("LEVEL2/16", new DialogueObject[]
        {
             new DialogueObject("CHO! Oh thank the sun!"),
             new DialogueObject("...Please listen to me! I searched everywhere for your mother. "),
             new DialogueObject("She was with us this morning. She must have gone looking for you and- oh by the sun..."),
             new DialogueObject("Dammit this was never apart of the deal!", new GenericEventMethod[]{

                 new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Brother_Scene8"),false,"Worried_Brother",null)

             },false),
             new DialogueObject("MOM I'M COMING!"),
             new DialogueObject("", new GenericEventMethod[]
             {
                   new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Brother_Scene8"),false,"Worried_Brother",null),
                 new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Brother_Scene8"), 810.1f, 924.6f, 45f, new GenericEventMethod[]
                 {
                     new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Brother_Scene8"), 810f, 940f, 30f,new GenericEventMethod[]
                     {
                         new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getNPC("Brother_Scene8"))
                     })
                 })
             },false),
        }) ;
        //trigger event brother walking into the TV screen



        AddDialogue("LEVEL2/18", new DialogueObject[]
        {
            new DialogueObject("...This was never apart of the deal!.."),
            new DialogueObject("...What do you mean he knows? A Secret fortress?"),
            new DialogueObject("Oh by the sun! CHO must've thought it was a dream."),
            new DialogueObject("Of course I believe in my religion! Don't you?!\nHow dare you try to consault me!"),
            new DialogueObject("... Well yes! Of course he knows what is at stake."),
            new DialogueObject("I mean, how could one refuse...A whole population depends on him..."),
            new DialogueObject("Sh- I hear something."),
            new DialogueObject("!", new GenericEventMethod[]
            {
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("TV_Scene_8"),"",null),
                new TwoParameterMethod<NPC,float>(NPC_Manager.Instance.lowerLighting,NPC_Dictionary.Instance.getNPC("storage_light_1"),0.5f),
                 new TwoParameterMethod<NPC,float>(NPC_Manager.Instance.lowerLighting,NPC_Dictionary.Instance.getNPC("Area_Light_1"),0.5f)
            },false)
        });
        AddDialogue("LEVEL2/19", new DialogueObject[]
            {
                 new DialogueObject("It's dangerous brother you shouldn't be here! Just... JUST GET OUT WHILE YOU STILL HAVE THE CHANCE!"),
                 new DialogueObject("I- I can't find her. Please don't hurt her"),
                 new DialogueObject("Who is Ivan talking to?"),
                 new DialogueObject("Mother... MOTHER!\nOh god I can't feel my legs.")
            });




   }

    private void AddLevel3(string level)
    {
        AddDialogue("LEVEL3/TELEPHONE", new DialogueObject[]
      {
               new DialogueObject("Dial a phone number?", new GenericEventMethod[]{  new TwoParameterMethod<string[], GenericEventMethod[][]>(OptionalDialogue.instance.PlayOptionDialogue, new string[] { "Dial", "Don't Dial"}, new GenericEventMethod[][] {
                   new GenericEventMethod[] { new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue,null, "LEVEL3/TELEPHONE/YES", null) } ,
                new GenericEventMethod[] { new ThreeParameterMethod<NPC, string, GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogueAndTriggerDialogue, null, "TELEPHONE/NO", null) } }) },false)
      });
        AddDialogue("LEVEL3/TELEPHONE/YES", new DialogueObject[]
        {
            new DialogueObject("I don't think you should do that.\nWe are on cusp of saving Xeno!") //brother sprite
        });


        AddDialogue("LEVEL3/PT1", new DialogueObject[]
        {
            new DialogueObject("I have made your breakfast CHO!"),
            new DialogueObject("An omlette and raseberries.The raseberries are from the backyard!", new GenericEventMethod[]
            {
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getDialogueNPC(),"LEVEL3/PT2",null),
            },true)

        });
        AddDialogue("LEVEL3/PT2", new DialogueObject[]
        {
                new DialogueObject("mmmm!"),
         });

        AddDialogue("LEVEL3/2", new DialogueObject[]
        {
            new DialogueObject("Brother, get away from the TELEPHONE!", 0.04f),
            new DialogueObject("It- it's our great big day!", 0.015f),
            new DialogueObject("It's time CHO! The sun is out!"),
            new DialogueObject("That means its time for mass!"),
            new DialogueObject("The whole clan is expecting us!"),
            new DialogueObject("", new GenericEventMethod[]
            {

                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,2,0),
                  new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC,NPC_Dictionary.Instance.getNPC("Brother_1"),-521.6f,762.6f,80f,
            //change dialogue after movment...
            new GenericEventMethod[] {new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Brother_1"),"LEVEL1/4",null),
                new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopTrigger,NPC_Dictionary.Instance.getNPC("Brother_1")),
                new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC,NPC_Dictionary.Instance.getNPC("Brother_1"),-501.1f,770.35f,80f,null)
         }),
                  new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeSprite,NPC_Dictionary.Instance.getNPC("Brother_1"),"Resources_Scene1_BrotherCatering"),
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue,NPC_Dictionary.Instance.getNPC("Brother_1"),"",null),
                
            },false)
        });
        AddDialogue("LEVEL3/3/PT1", new DialogueObject[]
        {
            new DialogueObject("Ouuuuhhh look who it is CHO!", new GenericEventMethod[]
            {
                new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeTriggerEvent,NPC_Dictionary.Instance.getNPC("Brother_1"),""),
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,2,0),
                new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC,NPC_Dictionary.Instance.getNPC("Brother_1"),-440.3f,768f,50f, null),
                new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Mother_Robot"), -450.3f, 760.4f, 10f,null),
            },false),
            new DialogueObject("It's your mom!"),
            new DialogueObject("ALL HAIL ARISE."),
            new DialogueObject("Mom how are you doing this morning."),
            new DialogueObject("..."),
            new DialogueObject("YES."),
            new DialogueObject("Uh... well there's no pressure CHO."),
            new DialogueObject("JOIN OF US OR SUFFER."),
            new DialogueObject("Oh mother! \nYou and your witty jokes!"),
            new DialogueObject("..."),
            new DialogueObject("YES.", new GenericEventMethod[]
            {
                new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopDialogue,NPC_Dictionary.Instance.getNPC("Brother_1")),
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,2,0),
                new ThreeParameterMethod<NPC,string,GenericEventMethod[]>(NPC_Manager.Instance.ChangeDialogue, NPC_Dictionary.Instance.getNPC("Mother_Trigger"),"",null),
                new TwoParameterMethod<NPC,string>(NPC_Manager.Instance.ChangeTriggerEvent, NPC_Dictionary.Instance.getNPC("Mother_Trigger"),"")

            },false)
        });

        AddDialogue("LEVEL3/3/PT2", new DialogueObject[]
      {
            new DialogueObject("..."),
            new DialogueObject("...."),
            new DialogueObject("I LIKE YOU SON"),
            new DialogueObject("Awww your so considerate mom!"),
            new DialogueObject("Look how considerate she is CHO!"),
            new DialogueObject("ALL HAIL ARISE.")

      });
        AddDialogue("LEVEL3/3.5", new DialogueObject[]
        {
            new DialogueObject("A journal.", new GenericEventMethod[]
            {
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,6)
            },true),
            new DialogueObject("CHO takes..."),
            new DialogueObject("GO AHEAD.\nTAKE IT.", new GenericEventMethod[]
            {
                  new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,6,0),
            },true),
            new DialogueObject("CHO feels its best not to take it.")
        });
        AddDialogue("LEVEL3/4", new DialogueObject[]
        {
            new DialogueObject("You snitched on us!"),
            new DialogueObject("That fortress was top secret!"),
            new DialogueObject("And where is Darnel?")
        });
        AddDialogue("LEVEL3/5", new DialogueObject[]
       {
           new DialogueObject("Wwwwhhhhaaattttt?"),
           new DialogueObject("#*$(@?"),
           new DialogueObject("Where does he keep getting those corndogs?")
       });
        AddDialogue("LEVEL3/6", new DialogueObject[]
        {
            new DialogueObject("Come dance!"),
            new DialogueObject("We're saved!")
        });
        AddDialogue("LEVEL3/7", new DialogueObject[]
        {
            new DialogueObject("YOUR BROTHER IS JUST AHEAD.", new GenericEventMethod[]{

                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,7)

            },true),
            new DialogueObject("HEAR THIS CHILD- MY CHILD. REMEMBER WHO ARE YOUR FRIENDS AND WHO WANTS TO SEE YOU SUFFER.", new GenericEventMethod[]{

                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,7,0)

            },true),
            new DialogueObject("YOUR BROTHER IS A WONDERFUL LEADER BUT HAS A FLAWED SENSE OF ESTEEM. YOU ARE OUR SAVIOUR CHO. ", new GenericEventMethod[]{
                new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomInPriority,25,5,10) },true),
            new DialogueObject("YOU ARE MORE IMPORTANT TO ARISE, TO YOUR FRIENDS, TO YOUR MOTHER THAN YOU CAN IMAGINE."),
            new DialogueObject("SO JUST HOLD ON, AND FOLLOW ORDERS.", new GenericEventMethod[]{
                new ThreeParameterMethod<float,float,float>(CameraManager.Instance.ZoomInPriority,25,0,10) },true),
        });
        AddDialogue("LEVEL3/8", new DialogueObject[]
        {

            new DialogueObject("CHO hears brother Ivan in the distance."),
            new DialogueObject("Dammit what do you mean things have changed!..."),
            new DialogueObject("", new GenericEventMethod[]{

            new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getDialogueNPC())

         },false)
        }) ;
        AddDialogue("LEVEL3/8.5", new DialogueObject[]
        {
            new DialogueObject("We had one single deal and you think you can just walk all over it!"),
            new DialogueObject("This is not your land this is not your religion!"),
            new DialogueObject("I DESERVE THIS SACRIFICE I DESERVE THE PRAISE OF THESE PEOPLE!", new GenericEventMethod[]{

            new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("hand_movement"),false, "Brother_Is_Taken",null),

            },false), //throws brother off the balcony
            new DialogueObject("", new GenericEventMethod[]{

            new SingleParameterMethod<NPC>(NPC_Manager.Instance.DestroyNpc, NPC_Dictionary.Instance.getDialogueNPC())

         },false)
        });
        AddDialogue("LEVEL3/8.9", new DialogueObject[]
        {
            new DialogueObject("The sun is just ahead."),
            new DialogueObject("This is your path."),
        });
        AddDialogue("LEVEL3/9", new DialogueObject[]
        {
            new DialogueObject("Do you ever hear voices CHO?", level + "/sun"),
            new DialogueObject("I hear the voices too...", level + "/sun"),
            new DialogueObject("Doctors panicking.", level + "/sun"),
            new DialogueObject("A test.", level + "/sun"),
            new DialogueObject("Then silence.", level + "/sun"),
            new DialogueObject("Sometimes I think I'm alone in my thoughts", level + "/sun"),
            new DialogueObject("And I was for too long.", level + "/sun"),
            new DialogueObject("Then you came.", level + "/sun"),
            new DialogueObject("The hero, the saviour of something greater than Xeno.", level + "/sun"),
            new DialogueObject("Willing to save his people no matter the cost.", level + "/sun"),
            new DialogueObject("This reality here, this is the reality I have created for you.", level + "/sun"),
            new DialogueObject("Save this reality by grabbing this axe in front of you.", level + "/sun"),
            new DialogueObject("AND DESTROY THE TELEPHONE THIS IS YOUR DESTINY.", level + "/sun"),
            new DialogueObject("-", level + "/sun"),
            new DialogueObject("...Since when did you speak your mind?", level + "/sun"),
            new DialogueObject("I'm just confused. What does destroying a phone have to do with saving the plant?", level + "/sun"),
            new DialogueObject("Why is he questioning me?", level + "/sun"),
            new DialogueObject("Uhhh- CHO? Why are you questioning our Sun?", level + "/sun"),
            new DialogueObject("Because its silly! Look, its just... oh gosh.", level + "/sun"),
            new DialogueObject("Why do I have to destroy the telephone? How about you guys do it?", level + "/sun"),
            new DialogueObject("But CHO... The citizens of Xeno... you want them to die? This is your moment!", level + "/sun"),
            new DialogueObject("I... I just don't get it!", level + "/sun"),
            new DialogueObject("He's actually not gonna do it.", level + "/sun"),
            new DialogueObject("Why are you guys looking at me like I'm stupid?", level + "/sun"),
            new DialogueObject("...really?", level + "/sun"),
            new DialogueObject("Oh come on! You don't see the Anvils dropping!.. Even the secret fortress! Or... why was Darnel being served for breakfast!", level + "/sun"),
            new DialogueObject("Whenever I dial up that telephone I see something and its hidden away from me...", level + "/sun"),
            new DialogueObject("And as much as I love these people, \nmother...", level + "/sun"),
            new DialogueObject("...You brother...", level + "/sun"),
            new DialogueObject("I'm just so confused!", level + "/sun"),
            new DialogueObject("Christ brother! Have you seen our own mother? What happened to her?", level + "/sun"),
            new DialogueObject("CHO... You've been dreaming-", level + "/sun"),
            new DialogueObject("STOP that! Stop hiding these things!", level + "/sun"),
            new DialogueObject("What's going on here! Tell me what happened to mom! Why is she so robotic?", level + "/sun"),
            new DialogueObject("You don't think I don't see these things! I remember it all! It's all stored in my head because I have one get it! Go ahead me back to my room! Make me breakfast! Is this your idea of freedom!?", level + "/sun"),
            new DialogueObject("Are you sure you want to know?", level + "/sun"),
            new DialogueObject("Yes!", level + "/sun"),
            new DialogueObject("hm ok.", level + "/sun"),
        });
   }

    private void AddLevel4(string level) //The_Fortress
    {
        AddDialogue("LEVEL4/0.5", new DialogueObject[]
        {
            new DialogueObject("Hey Darnel!", new GenericEventMethod[]
            {
                new FiveParameterMethod<NPC,float,float,float,GenericEventMethod[]>(NPC_Manager.Instance.MoveNPC, NPC_Dictionary.Instance.getNPC("Kid_Fortress"), -2.3f, 77.1f, 10f, new GenericEventMethod[]
                {
                new TwoParameterMethod<NPC, GenericEventMethod[]>(NPC_Manager.Instance.StopAnimation,NPC_Dictionary.Instance.getNPC("Kid_Fortress"),null)
                }),
                new FourParameterMethod<NPC,bool,string,GenericEventMethod[]>(NPC_Manager.Instance.PlayAnimation,NPC_Dictionary.Instance.getNPC("Kid_Fortress"),false,"Kid_Move_Up",null),
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,2),
            },false),
            new DialogueObject("Hey!"),
            new DialogueObject("Wait up!", new GenericEventMethod[]{ new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,2,0), new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopDialogue,NPC_Dictionary.Instance.getNPC("Kid_Fortress"))  }, true ),
          //  new DialogueObject("", new GenericEventMethod[]{ new SingleParameterMethod<NPC>(NPC_Manager.Instance.StopDialogue,NPC_Dictionary.Instance.getNPC("Kid_Fortress")) },false)
        }) ;
        AddDialogue("LEVEL4/1", new DialogueObject[]
         {
            new DialogueObject("How was your rest Christopher?"),
            new DialogueObject("...?"),
            new DialogueObject("You can talk."),
            new DialogueObject("Oh right. That's a relief-"),
            new DialogueObject("...You got my name wrong."),
            new DialogueObject("My name is CHO and my home remains in Xeno."),
            new DialogueObject("What is he on about?"),
            new DialogueObject("It's a miracle I could contact you through that journal."),
            new DialogueObject("Calling the phone was my idea.\nMaybe that is obvious-"),
            new DialogueObject("Is it done?", new GenericEventMethod[]
            {
              new ThreeParameterMethod<float, float,float> (CameraManager.Instance.ZoomInPriority, 20, 5, 3)
            },false),
            new DialogueObject("Is Xeno saved?"),
            new DialogueObject("Are you kidding? forget Xeno! \nXeno is bogus! You really think that place had good intentions? \n" +
            "I mean seriously, that whole 'save our planet' story they were painfully directing was a - ", new GenericEventMethod[]
            {
                     new ThreeParameterMethod<float, float,float> (CameraManager.Instance.ZoomInPriority, 20, 0, 10)
            },false),
            new DialogueObject("Am I still dreaming?"),
            new DialogueObject("A dream?"),
            new DialogueObject("Because if this is a dream I'll just wait for Ivan to wake me up."),
            new DialogueObject("Well... I did create this place myself so I won't take that statement too negatively."),
            new DialogueObject("But can guartee that you won't be waking up from this reality!-",new GenericEventMethod[]
            {
              new ThreeParameterMethod<float, float,float> (CameraManager.Instance.ZoomInPriority, 20, 5, 3)
            },false),
            new DialogueObject("Where is my family?"),
            new DialogueObject("?"),
            new DialogueObject("Family? Seriously? I hope your using the word pretty loosely."),
            new DialogueObject("I'm sure it is no suprise but your whole 'reality' was produced within the confines of a TV screen."),
            new DialogueObject("That one.", new GenericEventMethod[]
            {
               new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,0,4),
            },false),
            new DialogueObject("To be exact."),
            new DialogueObject("But your lucky you had me saved. I would hate to see your corpse freeze to death!", new GenericEventMethod[]
            {
                new TwoParameterMethod<int,int>(CameraManager.Instance.CameraChanger,4,0),
            },false),
            new DialogueObject("My... people are dead?", new GenericEventMethod[]
            {
                     new ThreeParameterMethod<float, float,float> (CameraManager.Instance.ZoomInPriority, 20, 0, 10)
            },false),
            new DialogueObject("I guartentee it!"),
            new DialogueObject("My brother is dead."),
            new DialogueObject("We're so proud you took things head on!"),
            new DialogueObject("And now your rewarded in knowing your brother was fictional, he was manipulated, he wanted all of us mudered! "),
            new DialogueObject("I killed my brother."),
            new DialogueObject("...yes... But Ivan is purely ones and zero. He's a lifeless figment. You should know this more than anyone!"),
            new DialogueObject("It's best not to give him a name."),
            new DialogueObject("The kid doesn't need to hear this right now.", 0.01f),
            new DialogueObject("Or maybe treat him as fruit fly. Something insignificant."),
            new DialogueObject("..."),
            new DialogueObject("I think I'm gonna go for a walk."),
            new DialogueObject("A walk?"),
            new DialogueObject("No. There's is no time Christopher! There's so much more we must discuss! Where would your destination be anyways there not musch to-?"),
            new DialogueObject("Why are you breathing fast?"),
            new DialogueObject("This place has plenty good air."),
            new DialogueObject("SAVE IT! ") //panick attack
         }) ;
        AddDialogue("LEVEL4/1.5", new DialogueObject[]
        {
            new DialogueObject("Ivan! You can wake me up now!"),
            new DialogueObject("Ivan are you with me?"),
            new DialogueObject("I can say the line with you brother..."),
            new DialogueObject("'A little bit of salt...'"),
            new DialogueObject("'Bit of pepper.'"),
            new DialogueObject("'Always- season your..'"),
            new DialogueObject("'eggs.'"),
            new DialogueObject("Snap out of it."),
        });
        AddDialogue("LEVEL4/3", new DialogueObject[]
        {
             new DialogueObject("Snap out of it CHO."),
        });
        AddDialogue("LEVEL4/4", new DialogueObject[]
        {
            new DialogueObject("Snap out of it..."),
            new DialogueObject("Hey Christopher! You found the TV Screen!"),
            new DialogueObject("But I warn you. I would not-"),
            new DialogueObject("SNAP OUT OF IT!!!"),
            new DialogueObject("?"),
            new DialogueObject("SNNNAP OUT OF IT! \n SNAP OUT OF IT YOU PIECE OF SHIT AHHH", 0.03f),
            new DialogueObject("BROTHER I'M COMING!"),
            new DialogueObject("Woah woah woah."),
            new DialogueObject("hey hey hey!"),
            new DialogueObject("STOP that!, stop him!")
        });
        AddDialogue("LEVEL4/5", new DialogueObject[]
        {
            new DialogueObject("Thank goodness you made it out!"),
            new DialogueObject("You really shouldn't enter Xeno's reality again!"),
            new DialogueObject("There's a great adventure awating!"),
            new DialogueObject("Just kill me."),
            new DialogueObject("What are you talking about?"),
            new DialogueObject("This isn't fun anymore."),
            new DialogueObject("I discovered the underbelly. Your right, my world is fictional."),
            new DialogueObject("I get it. I'm not CHO."),
            new DialogueObject("Just kill me."),
            new DialogueObject("I would never in my wildest dreams think of-"),
            new DialogueObject("Ok.")
        });
        //bar scene. 
       // AddDialogue("")

    }

    private void AddLevel5(string level) 
    {
 

    }
    private void AddTelephone(string level)
    {
        AddDialogue("TELEPHONEBANTER/1", new DialogueObject[]
        {
          new DialogueObject("ha-haha-ahahaha!"),
          new DialogueObject("Yeah burn baby!"),
          new DialogueObject("Hello? Who's this?"),
          new DialogueObject("Yeah! Burn it all down!"),
          new DialogueObject("Sorry I was talking my friend.\nHe's more of a co-worker."),
          new DialogueObject("Hello?"),
          new DialogueObject("The stranger hung up.")
        });
    }
       
    private void AddLevelGameplay(string level)
    {
        AddDialogue("GAMEPLAY1/1", new DialogueObject[] {
        new DialogueObject("hi")
        }); 
    }

    private void AddTikTok(string level)
    {
        AddDialogue("TIKTOK1/1", new DialogueObject[]
        {
            new DialogueObject("How does one conceptually categorize coffee when one sees a coffee cup?"),
            new DialogueObject("Science tells us its the color, shape, and senses which help us recognise it."),
            new DialogueObject("How do we bind our ideas and thoughts to identify something."),
            new DialogueObject("Yet we interpret the coffee cup as something whole. The coffee cup is a coffee cup."),
            new DialogueObject("Ha! that's just me tho."),
        });
        AddDialogue("TIKTOK1/2", new DialogueObject[]
       {
            new DialogueObject("Is It true?"),
            new DialogueObject("All men are mortal."),
            new DialogueObject("Socretes is a man."),
            new DialogueObject("Therefore, Socretes is mortal?"),
            new DialogueObject("These impalances of truth haunt the anime girl."),
            new DialogueObject("Thaaat's just me tho."),
            new DialogueObject("heh heh.")
       });
        AddDialogue("TIKTOK1/3", new DialogueObject[]
        {
            new DialogueObject("#$()$@$"),
            new DialogueObject(":)"),
            new DialogueObject("*Don't try to translate the words all you'll get is a smiley face.*")
        });
        AddDialogue("TIKTOK1/4", new DialogueObject[]
        {
            new DialogueObject("Just a cat with his corndog"),
            new DialogueObject("Got a problem with that?"),
            new DialogueObject("Thennn suuuueee mmeee.")
        });
        AddDialogue("TIKTOK1/5/QUESTION", new DialogueObject[]
        {
            new DialogueObject("Hummus, everyone likes hummus.")
        });
        AddDialogue("TIKTOK1/5/YES", new DialogueObject[]
        {
            new DialogueObject("Christopher tries some hummus."),
            new DialogueObject("Something tastes off."),
            new DialogueObject("Hey what are you doing with my wife.\n No manners!"),
            new DialogueObject("Don't worry baybeh, I got you.")
        });
        AddDialogue("TIKTOK1/5/NO", new DialogueObject[]
        {
            new DialogueObject("Not everyone likes hummus.")
        });
        AddDialogue("TIKTOK1/6", new DialogueObject[]
        {
            new DialogueObject("<- ?")
        });
    }


    //getters and setters
    public Dictionary<string, DialogueObject[]> getDialogueData()
    {
        return dialogueData;
    }


}
