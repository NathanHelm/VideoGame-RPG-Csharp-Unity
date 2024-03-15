using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal_Manager : MonoBehaviour
{
    private GameObject prefabPage;
    [SerializeField]
    List<PageData> pages; //stores the data
    [SerializeField]
    Stack<Page> rightPage = new Stack<Page>();
    [SerializeField]
    Stack<Page> leftPage = new Stack<Page>();
    [SerializeField]
    List<Page> pagesPrefabObject; //stores that gameobject
    PlayerMovement p;
    int Index = 0;
    public static Journal_Manager Instance;
    private Journal_Text journal_Text;
    bool isControlsEnabled = false;
    private bool stopper = false;
    private int pageAmount;

    private void Awake()
    {
        Debug.Log("AWAKE-> " + getPageAmount());
    }
    private void OnEnable()
    {
        try
        {
            Instance = FindObjectOfType<Journal_Manager>().GetComponent<Journal_Manager>();
            journal_Text = FindObjectOfType<Journal_Text>().GetComponent<Journal_Text>();
            p = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
            prefabPage = Resources.Load<GameObject>("Prefab/UI/p_parent");
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e);
        }
        Debug.Log("ONENABLE-> " + getPageAmount());
    }
    private void Start()
    {
        Debug.Log("START-> " + getPageAmount());
    }
    private void Update()
    {
        PlayJournal();
    }
    private void PlayJournal()
    {
        if (pages.Count > 0 && Input.GetKeyDown(KeyCode.Tab) && getControlsEnable() == false)
        {
          setJournal();
        }
    }
    public void BrotherOpenJournal()
    {
        setControlsEnable(false);
        stopper = true;
        StartCoroutine(PageAnimation());
        Debug.Log(stopper + "stopper");
        StartCoroutine(BrotherOpenJournalEnumerator());
    }
    private void setJournal()
    {
        //stop talking coroutine
        StopAllCoroutines();
        DialogueManager.Instance.StopAllCoroutines();
        p.setIsWalking(false);
        StartCoroutine(PageAnimation());
        StartCoroutine(Controls());
        //show front cover
        //press 'return' to open book   
    }
    public IEnumerator BrotherOpenJournalEnumerator()
    {
    
       
        yield return new WaitUntil(() => stopper == false);
        while(leftPage.Count > 1)
        {
            Debug.Log("flip!");
            journal_Text.EndTranslation();
            Page p = leftPage.Pop();
            p.FlipRight();
            rightPage.Push(p);
            yield return new WaitForSeconds(0.8f);
        }
        yield return null;
       // ExitJournal();
      //  p.setIsWalking(true); //player is still undergoing dialog
    }
    public IEnumerator Controls()
    {
        setControlsEnable(true);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Tab));
        }
      
        while (true)
        {


//            Debug.Log("page debbuger -> check index" + Index);
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (rightPage.Count > 0)
                {
                    Debug.Log("flip left");
                    journal_Text.EndTranslation();
                    Page p = rightPage.Pop();
                    p.FlipLeft();
                    leftPage.Push(p);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (leftPage.Count > 0)
                {
                    journal_Text.EndTranslation();
                    Page p = leftPage.Pop();
                    p.FlipRight();
                    rightPage.Push(p);
                }

            }
            if(Input.GetKeyDown(KeyCode.Return))
            {
                journal_Text.Translation(pagesPrefabObject[Index].getTranslation());
                //translation
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ExitJournal();
                break;
            }
            yield return new WaitForSeconds(0);
        }
        yield return null;
    }
    public void AddPage(PageData p)
    {
        pages.Add(p);
        DataPersistenceManager.Instance.SaveDataToPersistenceToJson(p);
        //no need to create a new filehandler for the journal
        //if scene has no pages, in runtime the paper will be added.
    }
 
    public void Load(Dictionary<string, object> keyValuePairs)
    {
        //TODO -> LOAD MUST RUN BEFORE ADD.
        //plays on start, 
        //adds all page data on load.

        Dictionary<string, object> dict = DataPersistenceManager.Instance.getPersistentData();
        Debug.Log("Load pages in game");
        pageAmount = 0;
        while (dict.ContainsKey("paper" + pageAmount))
        {
            //good.
            PageData p = (PageData)dict["paper" + pageAmount];
            pages.Add(p);
            ++pageAmount;
        }
    }
    public void Add(Dictionary<string, object> keyValuePairs)
    {
        //unforunately this method doesn't really work for persistent data.
        return;
    }

    public void getPagaData()//if there is any...
    {

    }
    public IEnumerator PageAnimation()
    {
        DestroyPages(); //resets the counter.
        if (pages.Count <= 0)
        {
            yield return null;
        }
        for(int i = 0; i < pages.Count; i++)
        {
            SpawnPage(i);
            pagesPrefabObject[i].FlipLeft();
            leftPage.Push(pagesPrefabObject[i]);
            yield return new WaitForSeconds(0.1f);
        }
        stopper = false; 
        yield return null;
       
    }
    public void SpawnPage(int i)
    {
       pagesPrefabObject.Add(Instantiate(prefabPage,FindObjectOfType<InsideUIScript>().transform).GetComponent<Page>());
       pagesPrefabObject[i].SetUp(pages[i]);
    }
    public void DestroyPages()
    {
        foreach(Page p in pagesPrefabObject)
        {
            Destroy(p.gameObject);
        }
        pagesPrefabObject = new List<Page>();
    }
    public void ExitJournal()
    {
        //exit journal ->
        DestroyPages();
        p.setIsWalking(true);
        setControlsEnable(false);
    }
    public bool setControlsEnable(bool temp)
    {
        isControlsEnabled = temp;
        return isControlsEnabled; 
    }
    public bool getControlsEnable()
    {
        return isControlsEnabled; 
    }
    public int getPageAmount()
    {
        return pageAmount;
    }

}
