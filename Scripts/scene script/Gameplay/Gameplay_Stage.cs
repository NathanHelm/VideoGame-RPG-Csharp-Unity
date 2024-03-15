using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gameplay_Stage : MonoBehaviour
{
    //items and enemies
    private List<GameObject> curGamplayObjectsObjects = new List<GameObject>();
    private Dictionary<string, Gameplay_Object> nameToGameplayObject = new Dictionary<string, Gameplay_Object>(); //contains all the enemy/item data.  
    private List<Item> items = new List<Item>(); 
    private List<Enemy> enemies = new List<Enemy>();
    private List<Enemy> curEnemies = new List<Enemy>();
    private List<Item> curItems = new List<Item>();
    private List<Gameplay_Object> curGameplayObjects = new List<Gameplay_Object>();
    private GameStage_Environment curGameStageEnv;
    private GameStage_Environment gameStage_Environment;
    private Vector2[] enemyCords;
    private Vector2[] ItemCords;
    private Vector2[] stageBoundsCords;
    private Vector2[] cardCords;
    private Vector2[] areaCords;
    private List<Gameplay_Object_Data> gameplay_Objects;
    private void Start()
    {
        //gathers all the data from the files.
    }
    public void SetStage(Gameplay_Stage_Data gameplay_Stage_Data)
    {
        curEnemies = new List<Enemy>();
        curGameplayObjects = new List<Gameplay_Object>();
        curItems = new List<Item>();
        curGamplayObjectsObjects = new List<GameObject>();
        ItemCords = gameplay_Stage_Data.barrierSpawnPos;
        enemyCords = gameplay_Stage_Data.enemySpawnPos;
        stageBoundsCords = gameplay_Stage_Data.stageBoundsCords;
        cardCords = gameplay_Stage_Data.cardSpawnCords;
         //adds the current enemies...
        for (int i = 0; i < gameplay_Stage_Data.enemies.Length; i++)
        {
            Enemy e = (Enemy)getGameplay(gameplay_Stage_Data.enemies[i]);
            curGameplayObjects.Add(e);
            curEnemies.Add(e);
        }
        //adds the current items...
        for (int i = 0; i < gameplay_Stage_Data.items.Length; i++)
        {
            Item item = (Item)getGameplay(gameplay_Stage_Data.items[i]);
            curGameplayObjects.Add(item);
            curItems.Add(item);
        }
      
        //adds the allgameplayObjects
       
     }

    private Gameplay_Object getGameplay(string name)
    {
        if(nameToGameplayObject[name] == null)
        {
            Debug.LogError("Enemies not found in the hashtable");
        }
        return nameToGameplayObject[name];
    }

    public void StoreAllDataFromResourcesFolderAndAddKeyPairVs(string currentScene)
    {
        //this code:
        //gets all scriptable object for our the current scene.
        //these scriptable objects "direct" our code and points to specific items/enemies to use for a the current 'battle'.
        //the scriptable object also contains a path which is eventually converted to a gameobject.
        //now we can directly add gameobject to the hashmap with its associated name.
        //note: we can't just throw a bunch of prefab enemies due to way prefabs work.
        gameplay_Objects = Resources.LoadAll<Gameplay_Object_Data>("Gameplay/Gameplay_Objects/" + currentScene).ToList();
        //also gets all gameplay object that are 'card game objects'
        Gameplay_Object_Data[] gameplay_Object_Data_Persistence = Resources.LoadAll<Gameplay_Object_Data>("Gameplay/Gameplay_Objects/Persistence");
        for(int i = 0; i < gameplay_Object_Data_Persistence.Length; i++)
        {
            gameplay_Objects.Add(gameplay_Object_Data_Persistence[i]);
        }

        foreach(Gameplay_Object_Data single in gameplay_Objects)
        {

               // Debug.Log("path " + single.Gameplay_Object_Path + " " + single.GameObject_fileName);
                GameObject gameObj = Resources.Load<GameObject>(single.Gameplay_Object_Path);
                Gameplay_Object gameplay_Object = gameObj.GetComponent<Gameplay_Object>();
                gameplay_Object.path = single.Gameplay_Object_Path;
                gameplay_Object.avatar = single.EnemySprite;
                nameToGameplayObject.TryAdd(single.GameObject_fileName, gameplay_Object);
                Gameplay_Stage_Manager.Instance.AddGameplay_Object(single.GameObject_fileName, gameplay_Object);
                if(gameplay_Object is Item)
                {
                    items.Add((Item)gameplay_Object);
                }
                else if(gameplay_Object is Enemy) //using a scriptable object you can introduce unique data.
                {
                    enemies.Add((Enemy)gameplay_Object);
                   
                }
                else if(gameplay_Object is GameStage_Environment)
                {
                     gameStage_Environment = (GameStage_Environment)gameplay_Object;
                }
                else
                {
                    Debug.LogError("its something else");
                    throw new NullReferenceException();
                }

        }
        
    }
    public void AddEnemy(string name)
    {
        Enemy gameplay_Object = nameToGameplayObject[name] as Enemy;
        curEnemies.Add(gameplay_Object);
       
    }
    public void AddEnemy(Enemy enemy)
    {
        curEnemies.Add(enemy);
    
    }
    public void AddItem(string name)
    {
        Item gameplay_Item = nameToGameplayObject[name] as Item;
        curItems.Add(gameplay_Item);
    }
    public void AddItem(Item item)
    {
        curItems.Add(item);
    }
    public void SpawnGamePlayObjects()
    {
        SpawnItems();
        StartCoroutine(SpawnEnemies()); //async await coroutine...
        SpawnStage();
        //spawns both items and enemies...
    }
    public void SpawnStage()
    {
        GameObject prefab = Resources.Load<GameObject>(gameStage_Environment.path);
        GameObject g = Instantiate(prefab);
        Gameplay_Object gameplay_Object = g.GetComponent<GameStage_Environment>();
        curGameplayObjects.Add(gameplay_Object);
        curGamplayObjectsObjects.Add(g);

    }

    public void SpawnItems()
    {
        for(int i = 0; i < curItems.Count; i ++)
        {
            SpawnItem(curItems[i]);
        }
    }
    public IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < curEnemies.Count; i ++)
        {
            SpawnEnemy(curEnemies[i]);
        }
        yield return null;
    }
    public void SpawnEnemy(Enemy e)
    {
        GameObject enemyObject = Resources.Load<GameObject>(e.path);
        GameObject g = Instantiate(enemyObject);
        Debug.Log(getRandomVec(enemyCords));
        g.transform.position = getRandomVec(enemyCords);
        curGamplayObjectsObjects.Add(g);
    }
    public void SpawnItem(Item item)
    {
        GameObject itemObject = Resources.Load<GameObject>(item.path);
        GameObject g = Instantiate(itemObject);
        g.transform.position = getRandomVec(ItemCords);
        curGamplayObjectsObjects.Add(g);
    }

    public Vector2 getRandomVec(Vector2[] vectors)
    {
        int n = vectors.Length - 1;
        int randomCord = UnityEngine.Random.Range(0,n);
        return vectors[randomCord];
    }
    public List<Enemy> getCurEnemies()
    {
        return curEnemies;
    }
    public List<Item> getCurItems()
    {
        return curItems;
    }
    public Vector2[] getEnemyCords()
    {
        return enemyCords;
    }
    public Vector2[] getItemCords()
    {
        return ItemCords;
    }
    public Vector2[] getStageBoundsCords()
    {
        return stageBoundsCords;
    }
    public GameStage_Environment GetGameStage_Environment()
    {
        return gameStage_Environment;
    }
    public GameStage_Environment GetCurGameStage_Environment()
    {
        return curGameStageEnv;
    }
    public List<GameObject> GetCurGameObjects()
    {
        return curGamplayObjectsObjects;
    }
    public List<Gameplay_Object> GetCurGameplay_Objects()
    {
        return curGameplayObjects;
    }
    public Vector2[] getCardCords()
    {
        return cardCords;
    }
}
