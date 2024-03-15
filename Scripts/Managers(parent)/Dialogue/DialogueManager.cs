using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DialogueManager : MonoBehaviour
{
    //public static DialogueManager _instance;
    public static DialogueManager Instance;
    private IEnumerator dialogueRunning;
    private DialogueDictionary dialogueDictionary;
    
    public float TextTime { get; set; }
    [SerializeField]
    public TextMeshProUGUI TextUI { get; set; }
    [SerializeField]
    public int Index;
    UIScript uIScript;
    public string Line { get; set; }

    Stack<GameObject> stack = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<DialogueManager>().GetComponent<DialogueManager>();
        }
      
    }
    private void OnEnable()
    {
        uIScript = FindObjectOfType<UIScript>().GetComponent<UIScript>();
        TextUI = FindObjectOfType<TextUI>().GetComponent<TextMeshProUGUI>();
    }


    public float setTextTime(float textTime)
    {
        return TextTime = textTime;
        
    }
    public TextMeshProUGUI setTextUI(TextMeshProUGUI t)
    {
        return TextUI = t;
    }

    IEnumerator RunDialogue(DialogueObject[] dialogueObjects)
    {
        TextUI.text = "";
        string line = dialogueObjects[Index].DialogueLine; //gets line before key is replaced

        this.Line = line; //set dialog object line to dialogue manager line so it can be easily accessible to awaiting dialogue object
        Debug.Log("dialog objects enqueu");
        dialogueObjects[Index].Enqueue();
        IEvent.e.PlayEvent();//this comes after line.
        
        try
        {
          Sprite s = dialogueObjects[Index].GetSprite(); 
          if(s != null)
            {
                //sets the sprite of the avatar class to the dialogue object's sprite
                Avatar.Instance.SetSprite(s);
                StartCoroutine(Avatar.Instance.PlayAvatarAnimation());
            }
        }
        catch(NullReferenceException )
        {
            Debug.Log("character not found you dounut");
        }
        
       
            for (int i = 0; i < line.Length; i++)
            {
                char singleText = line[i];
                TextUI.text += singleText;

                yield return new WaitForSeconds(TextTime);
            }
            IndexPlusOne(dialogueObjects);
            dialogueRunning = null;
            yield return null;
    }
    public void ScrollingText(TMP_Text t, string line, float T)
    {
        StartCoroutine(ScrollingTextCoroutine(t,line, T));
    }
    public IEnumerator ScrollingTextCoroutine(TMP_Text t, string line, float textT)
    {
        for (int i = 0; i < line.Length; i++)
        {
            char singleText = line[i];
            t.text += singleText;

            yield return new WaitForSeconds(textT);
        }
    }
    public void spawnText(string line, Vector3 pos)
    {
        GameObject textUIgO = Resources.Load<GameObject>("Prefab/Opening_Animation/UI_Text");
        TextMeshProUGUI textUI = Instantiate(textUIgO, uIScript.transform).GetComponent<TextMeshProUGUI>();
        RectTransform r = textUI.gameObject.GetComponent<RectTransform>();
        r.localPosition = pos;
        ScrollingText(textUI, line, 0.05f);
        stack.Push(textUI.gameObject);
    }
    public void DestroyText()
    {
        StartCoroutine(DestroyTextCoroutine());
    }
    public IEnumerator DestroyTextCoroutine()
    {
        while(stack.Count > 0)
        { 
            GameObject g = stack.Pop();
            Destroy(g);
           yield return new WaitForSeconds(1 - Time.deltaTime);
        } 
    }

    public void RunDialogueCoroutine(DialogueObject[] dialogueObjects)
    {
      StartCoroutine(dialogueRunning = RunDialogue(dialogueObjects));
    }

    public bool isDialogueRuning()
    {
        if (dialogueRunning == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int IndexPlusOne(DialogueObject[] ds)
    {
        ++Index;

        return Index;
    }
    public int SetIndex(int indx)
    {
        Index = indx;
        return Index;
    }

    public int SetTextTime(int speed)
    {
        TextTime = speed;
        return speed;
    }
    public bool IsLineDialogueObject(string line)
    {
        if(Line.Equals(line))
        {
            Debug.Log("line equals line !");
            return true;
        }
        else
        {
            return false;
        }
    }

}
