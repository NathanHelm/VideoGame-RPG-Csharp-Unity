using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
public class Gameplay_Stage_Manager : MonoBehaviour,IAct
{
    // Start is called before the first frame update
    private List<CardGameSetting> cardPackets = new List<CardGameSetting>();
    [SerializeField]
    private OpeningAnimation opening;
    private Gameplay_Stage currentStage;
    public string currentlevelName;
    public string currentbossName;
    public static Gameplay_Stage_Manager Instance;
    public Dictionary<string, Gameplay_Stage_Data> stageData = new Dictionary<string, Gameplay_Stage_Data>();
    private Dictionary<string, Boss> nameToBoss = new Dictionary<string, Boss>();
    private Dictionary<string, Gameplay_Object> nameToGameplay = new Dictionary<string, Gameplay_Object>();
    [SerializeField]
    private Boss currentBoss;
    private int rounds = 1;
    private void OnEnable()
    {
        Instance = FindObjectOfType<Gameplay_Stage_Manager>().GetComponent<Gameplay_Stage_Manager>();
    }
    
    private void setAllLevelsDict()
    {
        stageData.Add("Gameplay_1", new Gameplay_Stage_Data("Gameplay_1",
        new Vector2[] { new Vector3(-40.8f, -4.6f, 0), new Vector3(76.2f, -0.8f, 0) },
        new Vector2[] { new Vector2(-20.9f, -7.4f), new Vector2(82.9f, -53.1f),
        new Vector2(-79.1f, 42.5f) },
        //enter enemies and objects
        new string[]{ "clansmen", "healer", "clansmen"} ,
        new string[]{ "chair", "couch", "chair" },
        //LEFT TO RIGHT FOR THESE BOUNDS
        new Vector2[] { new Vector2(-94.9f, 2.2f), new Vector2(97, -9.2f) },
        //card positions
        new Vector2[] { new Vector2(67.2f, -56.4f), new Vector2(-82f, -15f), new Vector2(-80.9f,35.4f)},
        new Vector2[] { new Vector2 (10,10)}
        ));

    }
    private void setBossDict()
    {
        nameToBoss.TryAdd("Brother_Boss", new Brother_Boss());
    }
    public void AddGameplay_Object(string gameplay_object_name, Gameplay_Object g)
    {
        nameToGameplay.TryAdd(gameplay_object_name, g);
    }

    private void Start()
    {
        currentlevelName = Level_Manager.Instance.GetCurrentSceneName();
        setBossDict();
        setAllLevelsDict(); //sets data for all scenes...
        setEverything();
       
    }
    public void setEverything()
    {
        setStage(currentlevelName);
        setBoss();
        setCardsManager();
    }
    private void setStage(string levelName)
    {
        currentStage = gameObject.AddComponent<Gameplay_Stage>();
        currentStage.StoreAllDataFromResourcesFolderAndAddKeyPairVs(levelName); //does the same thing as a hashmap only gets the enemies via a scriptable object.
        currentStage.SetStage(stageData[levelName]);
    }
    private void setBoss()
    {
        currentBoss.SetCustomBoss();
        currentbossName = currentBoss.bossName;
    }
    private void setCardsManager()
    {
        CardManager.Instance.setManager(rounds, currentStage.getCardCords());
    }

    public void PlayStage()
    {
        currentStage.SpawnGamePlayObjects();
    }
    public void PlayBoss()
    {
        currentBoss.SpawnBodyParts();
        currentBoss.AttackState();
    }
    public void ResetBoss()
    {
            Debug.Log("resetting");
            currentBoss = nameToBoss[currentbossName];
            BossBodyPart[] bossBodyParts = GameObject.Find("Boss").GetComponents<BossBodyPart>();
            BodyMovement[] bodyMovements = GameObject.Find("Boss").GetComponents<BodyMovement>();
            Boss[] originalBoss = GameObject.Find("Boss").GetComponents<Boss>();
            for (int i = 0; i < bossBodyParts.Length; i++)
            {
                Destroy(bossBodyParts[i]); //destroy added boss components, pt1
            }
            for (int i = 0; i < bodyMovements.Length; i++)
            {
                Destroy(bodyMovements[i]); //destroy added boss components, pt1
            }
            for (int i = 0; i < originalBoss.Length; i++)
            {
                for(int j = 0; j < originalBoss[i].GetBossBodyPart().Count; j++)
                {
                    Destroy(originalBoss[i].GetBossBodyPart()[j].gameObject);
                }
                Destroy(originalBoss[0]);
            }
            Boss b = GameObject.Find("Boss").AddComponent(currentBoss.GetType()) as Boss;
            
            b.SetCustomBoss();
            currentBoss = b;
    }

    public void AddPacket(CardGameSetting c)
    {
        cardPackets.Add(c);
    }
    public void PlayAfterSpawn()
    {
        //applies all interfaces changes
        foreach (CardGameSetting single in cardPackets)
        {
          StartCoroutine(single.PlayAfterSpawn());
        }
        cardPackets = new List<CardGameSetting>();
    }

    public Gameplay_Stage getGameplay_Stage()
    {
        return currentStage;
    }
    public void SetCurrentStage(Gameplay_Stage s)
    {
        currentStage = s;
    }
    public void SetCurrentBoss(Boss b)
    {
        currentBoss = b;
    }

    public IEnumerator PlayAct(bool enable)
    {
        PlayStage();
        CardManager.Instance.spawnCards();
        //waits for all gameplay objects to spawn before playing the previous round's cards
        AIManager.Instance.PlayPathFinding(FindObjectsOfType<Item>(),FindObjectsOfType<Card>());
        PlayBoss();
        PlayAfterSpawn(); //does all attacks after all objects are instantiated. 
        


        //go to the gameplay stage 



        yield return new WaitForSeconds(25);
        setRound(getRounds() + 1);
        Act_Manager.Instance.setEnable(true);
        yield return null;
    }
    public Gameplay_Object getGameplay_Object(string str)
    {
        return nameToGameplay[str];
    }
    public Boss getBoss()
    {
        return currentBoss;
    }
    public int getRounds()
    {
        return rounds;
    }
    public int setRound(int amount)
    {
        return (rounds + amount);
    }
    public List<CardGameSetting> getCardsPackets()
    {
        return cardPackets;
    }

}
