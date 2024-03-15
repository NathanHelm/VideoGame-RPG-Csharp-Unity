using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : TriggerEvent 
{
    [SerializeField]
    private string dialogueKey;
    private DialogueObject[] dialogueObjects;
    DialogueManager dialogueManager;
    DialogueDictionary dialogueDictionary;
    DialogueBox diaBox;
    TextUI text;
    Avatar avatar;
    PlayerMovement playerMovement;
    //IEnumerator awaitrunDialog;
    public Sprite npcSprite { get; set; }
    
    public override void Load(Dictionary<string, object> keyValuePairs)
    {
        
            //Debug.Log("name"+ gameObject.name + "data NPC" + DataPersistenceManager.Instance.JsonObjectToString(keyValuePairs[transform.GetHashCode().ToString()]));
            NpcData npcData = (NpcData)keyValuePairs[gameObject.name];
            Debug.Log(gameObject.name +" Load NPC name " + gameObject.name);
            if (npcData != null)
            {
                this.dialogueKey = npcData.dialogueKey;
                transform.position = npcData.position;
                setKey(npcData.triggerKey);
            }
            else
            {
            Debug.LogError("npcData is null for" + gameObject.name);
            }
   
    }

    public override void Add(Dictionary<string, object> keyValuePairs)
    {
        Debug.Log(gameObject.name + " Add hashcode NPC " + gameObject.name);
        NpcData npcData = new NpcData(dialogueKey, transform.position, getKey(), gameObject.name);
        if (!keyValuePairs.ContainsKey(gameObject.name))
        {
            keyValuePairs.Add(gameObject.name, npcData);
        }
    }

    private void EnableUI()
    {
        //enable objects
        diaBox.EnableDialogueBox();
        playerMovement.setIsWalking(false);
        
    }
    private void DisableUI()
    {
        Avatar.Instance.DisableAvatar();
        playerMovement.setIsWalking(true);
        diaBox.DisableDialogueBox();
        text.DisableText();
        avatar.DisableAvatar();
        
    }

    private void Start()
    {
        SetPlayerTransform();
        avatar = FindObjectOfType<Avatar>();
        text = FindObjectOfType<TextUI>();
        diaBox = FindObjectOfType<DialogueBox>();
        dialogueManager = DialogueManager.Instance;
        dialogueDictionary = DialogueDictionary.Instance;
        playerMovement = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
       // playerMovement.
    }
    public void RunDialog()
    {
        //Debug.Log("running dialogue");
        dialogueManager.SetIndex(0);
        StartCoroutine(AwaitRunDialogue());
    }
    public void RunTriggerDialogue()
    {
       
        if (!dialogueKey.Equals(""))
        {
            //Debug.Log("dialogue is running");
            dialogueManager.SetIndex(0);
            EnableUI();
            dialogueObjects = dialogueDictionary.GetDialogueObjectData(dialogueKey);
            DialogueManager.Instance.RunDialogueCoroutine(dialogueObjects);
            StartCoroutine(AwaitRunDialogue());
        }
        
    }

    public IEnumerator AwaitRunDialogue()
    {

        if (!dialogueKey.Equals(""))
        {

            dialogueObjects = dialogueDictionary.GetDialogueObjectData(dialogueKey);

            while (dialogueManager.Index <= dialogueObjects.Length)
            {
               
                if (dialogueManager.Index >= dialogueObjects.Length)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        break;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Return) && !DialogueManager.Instance.isDialogueRuning())
                {
                    EnableUI();
                    DialogueManager.Instance.RunDialogueCoroutine(dialogueObjects);
                }
                
                yield return new WaitForSeconds(0);
            }
           
            DisableUI();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            while (true)
            {

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    RunDialog();
    
                break;
                }
                yield return new WaitForSeconds(0);
            }

        }

    
        yield return null;
       
    }

    public void StopTalking()
    {
        StopAllCoroutines();
        DisableUI();
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player_Moves_out");
            DisableUI();
            StopAllCoroutines();
            dialogueObjects = null;
        }
    }

    //override 
    public override void Action()
    {
        RunDialog();
    }
    public Sprite setNPCSprite(Sprite s)
    {
        SpriteRenderer sR  = gameObject.GetComponent<SpriteRenderer>();
        npcSprite = sR.sprite;
        try
        {
            if (npcSprite == null)
            {
                throw new NullReferenceException();
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e + "can't find your sprite :(");
            return null;
        }
        return sR.sprite = s;
    }
    public void setDialogueKey(string newKey)
    {
        dialogueKey = newKey;
        //RunDialog();
    }
    public IEnumerator awaitSetDialogueKey()
    {
        while (true)
        {
            
            if (Input.GetKeyDown(KeyCode.Return))
            {  
                break;
            }
            yield return new WaitForSeconds(0);
        }
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

    }
}
