using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Act_Manager : MonoBehaviour
{
    private List<IAct> allActs = new List<IAct>();
    public static Act_Manager Instance;
    Stack<IAct> actsInGame = new Stack<IAct>();
    bool enable = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Instance = FindObjectOfType<Act_Manager>().GetComponent<Act_Manager>();
    }
    void Start()
    {
        AddActToGame();
        GameplayLoop();
    }
    public void AddActToGame()
    {
        IAct first = FindObjectOfType<Begin>().GetComponent<Begin>();
        IAct act = FindObjectOfType<OpeningAnimation>().GetComponent<OpeningAnimation>();
        IAct act1 = Gameplay_Stage_Manager.Instance;
        IAct act2 = FindObjectOfType<Reset>().GetComponent<Reset>();
        IAct act3 = FindObjectOfType<CardSelection>().GetComponent<CardSelection>();


        allActs.Add(first);
        allActs.Add(act3);
        allActs.Add(act2);
        allActs.Add(act1);
        allActs.Add(act);
        
       

    }
    public void GameplayLoop()
    {
        if(actsInGame.Count <= 0)
        {
            for (int i = 0; i < allActs.Count; i++)
            {
                actsInGame.Push(allActs[i]);
            }
        }
       
        setEnable(false);
        StartCoroutine(actsInGame.Pop().PlayAct(enable));
        StartCoroutine(WaitForBool());
    }
 
    public IEnumerator WaitForBool()
    { 
        while (true)
        {
            if (enable)
            {
                GameplayLoop();
                break;
            }
        
            yield return new WaitForFixedUpdate();
        }

       
    }
    public void setEnable(bool enable)
    {
        this.enable = enable;
    }

}
