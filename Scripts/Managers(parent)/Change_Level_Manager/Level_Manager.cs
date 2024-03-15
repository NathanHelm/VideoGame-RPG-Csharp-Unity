using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Level_Manager : MonoBehaviour
{
    private GameObject g;
    public static Level_Manager Instance;
    private int previousSceneNumber = 0;
    public static Dictionary<string, FileHandler> sceneStringPairFileName;
    GenericEventMethod[] gs = new GenericEventMethod[] { };

    public FileHandler GetFileNameOnScene()
    {
        try
        {
            if (sceneStringPairFileName.Count < 1)
            {
                throw new NullReferenceException();
            }
        }
        catch(NullReferenceException e)
        {
            Debug.LogError(e + " Dictionary hasn't loaded yet. Chill out bruh.");
        }
        if(!sceneStringPairFileName.ContainsKey(GetCurrentSceneName()))
        {
            Debug.LogError("scene not found");
        }
        Debug.Log("cureent scene name" + GetCurrentSceneName());
        return sceneStringPairFileName[GetCurrentSceneName()];
        
        
    }
    public FileHandler GetPersistentFileHandler()
    {
        if (!sceneStringPairFileName.ContainsKey("ComeToLifeSaveFile_p_Data"))
        {
            Debug.LogError("persistentFileHandlerNotFound");
        }
        if(sceneStringPairFileName["ComeToLifeSaveFile_p_Data"] == null)
        {
            Debug.LogError("persistentFileHandlerisded");
        }
        Debug.Log("persistence " + sceneStringPairFileName["ComeToLifeSaveFile_p_Data"]);
        return sceneStringPairFileName["ComeToLifeSaveFile_p_Data"];
    }
   
    public string GetCurrentSceneName()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.name;
    }
    public int GetCurrentSceneNumber()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.buildIndex;
    }
    public int GetPreviousSceneNumber()
    {
        return previousSceneNumber;
    }
    public void SetPreviousSceneNumber(int previous)
    {
        previousSceneNumber = previous;
    }
    public FileHandler GetPreviousSceneFileHandler()
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(GetPreviousSceneNumber());
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneStringPairFileName[sceneName];
    }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
       
    }
    private void OnEnable()
    {
        Instance = FindObjectOfType<Level_Manager>().GetComponent<Level_Manager>();
        if (sceneStringPairFileName == null)
        {
            setAllScenes();
        }

    }
    private void setAllScenes()
    {
        sceneStringPairFileName = new Dictionary<string, FileHandler>();
        sceneStringPairFileName.Add("ComeToLifeSaveFile_p_Data", new FileHandler(Application.persistentDataPath, "ComeToLifeSaveFileData.json"));
        sceneStringPairFileName["ComeToLifeSaveFile_p_Data"].MakeDictionary();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            // Get the scene path from the build settings
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            // Extract the scene name from the path
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

//           Debug.Log("scene by build index" + sceneName);
           sceneStringPairFileName.Add(sceneName, new FileHandler(Application.persistentDataPath, "ComeToLifeSaveFile_Level" + i + ".json"));
        }
    //    Debug.Log("going hoe");
          string fullPath = Path.Combine(Application.persistentDataPath, "ComeToLifeSaveFile_Data.json");
        Debug.Log(fullPath);
    
    }

    private void FixedUpdate()
    {
       // Debug.Log("updating level manager");
    }
    public void ChangeScene(int level, GenericEventMethod[] genericEventMethods)
    {
        gs = genericEventMethods;
        Debug.Log("change scene");
        DataPersistenceManager.Instance.SetFileHandlers();
        DataPersistenceManager.Instance.Save();

        SetPreviousSceneNumber(GetCurrentSceneNumber());
        SceneManager.LoadScene(level);
    }
    public void Start()
    {
        if (DataPersistenceManager.Instance != null)
        {
            LevelIsLoaded();
        }
    }
    private void LevelIsLoaded()
    {
        DataPersistenceManager.Instance.SetFileHandlers();
    
        DataPersistenceManager.Instance.Begin();
        if (gs == null)
        {
            gs = gs = new GenericEventMethod[] { };
        }
        if (gs.Length > 0)
        {
            IEvent.e.pushEvents(gs);
            IEvent.e.PlayEvent();
        }
    }
}
