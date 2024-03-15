using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance;
    PlayerMovement playerMovement;
    //every json file has a string,
    //recall that any Gameobjectcan be saved
    private FileHandler fileHandler;
    private FileHandler persistingFileHandler;
    IDataPersistence dataPersistence;
    private Dictionary<string, object> keyValuePairs;
    private Dictionary<string, object> persistentData = new Dictionary<string, object>();
    DataPersistence[] dialogueInterfaces = new DataPersistence[] { };

    public void addGameData(string s, object data)
    {
        keyValuePairs.Add(s, data);
    }
    public Dictionary<string, object> getPersistentData()
    {
        foreach (KeyValuePair<string, object> single in persistentData)
        {
            Debug.Log("persistent values hoe:" + single.Value);
        }
        return persistentData;
    }
    public void addGameDataPersistence(string s, object data)
    {
        if (!persistentData.ContainsKey(s))
        {
            persistentData.Add(s, data);
        }
    }
    public void ChangeData(string s, object data)
    {
       if(keyValuePairs.ContainsKey(s))
        {
            keyValuePairs[s] = data;
        }
       else
        {
            addGameData(s, data);
        }
    }
    
    public object GetData(string hashcode)
    {
        if (keyValuePairs.ContainsKey(hashcode))
        {
            return keyValuePairs[name];
        }
        else
        {
            throw new NullReferenceException();
        }
    }
    public string JsonObjectToString(object data)
    {
        if (data != null)
        {
            return JsonUtility.ToJson(data);
        }
        else
        {
            throw new NullReferenceException();
        }
    }

    private void OnEnable()
    {
        
        if (Instance == null)
        {
            Instance = FindObjectOfType<DataPersistenceManager>().GetComponent<DataPersistenceManager>();
        }
       
    }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public void Save()
    {
        AddKeyPairValues();
        SaveDataToJSON();
    }
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("save");
            AddKeyPairValues();
            SaveDataToJSON();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("load");
            LoadDataFromJSON();
       }
         
        
    }
    

    public void SetFileHandlers()
    {
   
       fileHandler = Level_Manager.Instance.GetFileNameOnScene();
       persistingFileHandler = Level_Manager.Instance.GetPersistentFileHandler();
    }
    public void Begin()
    {
        Debug.Log("starting");
        persistentData = new Dictionary<string, object>();
        AddKeyPairValues();
        LoadDataFromJSON();
        Journal_Manager.Instance.Load(null);
        Debug.Log("after the loading" + Journal_Manager.Instance.getPageAmount());
    }


    

    private void AddKeyPairValues()
    {
        keyValuePairs = new Dictionary<string, object>();
        dialogueInterfaces = FindObjectsOfType<DataPersistence>();
        foreach (DataPersistence single in dialogueInterfaces)
        {
        single.Add(keyValuePairs);
        }
    }
    public void SaveDataToJSON()
    {
        // extention to the save data -> if you CHANGE scenes then is important to save all previous code. but its also important to keep the saved data into another file.
        //make a new file specifically for each scene...
        fileHandler.ResetFile();
        //persistingFildHandler's file does not reset.
        foreach (KeyValuePair<string, object> single in keyValuePairs)
        {
            Debug.Log("save to json Value" + single.Key);
            fileHandler.Save(JsonObjectToString(single.Value), single.Value.GetType());
        }

    }
    public void SaveDataToPersistenceToJson(PageData pageData)
    {
        //directly saves data to the json file.
        persistingFileHandler.Save(JsonObjectToString(pageData), pageData.GetType());
    }
    public void LoadDataFromJSON()
    {
        try
        {

            if (fileHandler != null)
            {
                if (fileHandler.Load().Count > 1)
                {
                    Queue<object> q = fileHandler.Load();
                    List<string> keys = new List<string>();
                    foreach (KeyValuePair<string, object> single in keyValuePairs)
                    {
                        keys.Add(single.Key);
                       
                    }
                    for (int i = 0; i < keys.Count; i++)
                    {
                        object obj = q.Dequeue();
                        Data d = (Data)obj;
                        string key = d.hashcode;
                        Debug.Log("loadkey " + key);
                        keyValuePairs[key] = obj;
                    }
                }
            }
            else
            {
                throw new NullReferenceException();
            }

            LoadDataPersistence();

            foreach (DataPersistence single in dialogueInterfaces)
            {
                single.Load(keyValuePairs);
            }

        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e + "filehandler is null");
        }
    }
    public void LoadDataPersistence()
    {
        
        if (persistingFileHandler != null)
        {
            Debug.Log("loading the data persistence");
            Queue<object> q = persistingFileHandler.Load();
            Debug.Log("Queue count" + q.Count);
                while (q.Count > 0)
                {
                    object obj = q.Dequeue();
                    Data d = (Data)obj;
                    string key = d.hashcode;
                    Debug.Log("key for loading data persistence");
                    addGameDataPersistence(key, obj); //retrieves all pages.
                }
        }
        else
        {
            throw new NullReferenceException();
        }
        Debug.Log("persiting hashmap size" + persistentData.Count);

    }
    
}
