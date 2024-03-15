using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OptionalDialogue : Controls<OptionBox>
{
    [SerializeField]
    GameObject optionBox;
    RectTransform rectTransform;
    List<OptionBox> optionBoxs = new List<OptionBox>();
    //int index = 0;
    public static OptionalDialogue instance;
    public void PlayOptionDialogue(string[] amountOfBoxes, GenericEventMethod[][] genericEventMethod)
    {
        CameraManager.Instance.ZoomInPriority(27, 2, 2.5f);
        setTransform(); //gets the transform to perform sin and cosine functions...
        int length = amountOfBoxes.Length - 1;
        PriorityQueue<int> priorityQueue = new PriorityQueue<int>();
        float angle = 0;
        float angleIncrease = Mathf.PI/(length + 1 / 2);
        float radius = 105f;
        for (int i = 0; i <= length; i++)
        {
            OptionBox option = Instantiate(optionBox,FindObjectOfType<UIScript>().transform).GetComponent<OptionBox>();
            angle += angleIncrease;
            option.setUp(0 + (Mathf.Cos(angle) * radius) , 0 + Mathf.Sin(angle)* radius, amountOfBoxes[i],new Color(256, 256, 256, 235), genericEventMethod[i]);
            optionBoxs.Add(option);
        }
        StartCoroutine(selectOptionsCoroutine(optionBoxs, length));
        

    }
    public override bool condition()
    {
        Debug.Log("checking");
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //
           // beforeInput();
            optionBoxs[index].Select();
            Disable();
            Debug.Log("Disabled");
            return false;
        }
        else
        {
            return true;
        }
    }
    private void Disable()
    {
        for(int i = 0; i < optionBoxs.Count; i++)
        {
            Destroy(optionBoxs[i].gameObject); 
        }
        optionBoxs = new List<OptionBox>();
    }
    private void Start()
    {
        //test

       //PlayOptionDialogue(new string[] { "yes", "no"}, new GenericEventMethod[][] { new GenericEventMethod[] { } , new GenericEventMethod[] { } });
    }
    private void Awake()
    {
        instance = FindObjectOfType<OptionalDialogue>();
    }
    public override void doSomething(int index)
    {
        optionBoxs[index].setIsWobbling(true);
        optionBoxs[index].Run();
    }
    public override void beforeInput()
    {
        Debug.Log("index" + index);
        optionBoxs[index].setIsWobbling(false);
    }

    public void setTransform()
    {
        rectTransform = FindObjectOfType<DialogueBox>().GetComponent<RectTransform>();
    }
}
