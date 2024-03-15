using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : MonoBehaviour,IAct
{
    public static Begin Instance;
    private string sceneName;
    Gameplay_Stage gameplay_Stage;
    private Dictionary<string, Gameplay_Stage_Data> stageData = new Dictionary<string, Gameplay_Stage_Data>();
    Boss b;



    private void SetUp()
    {

        gameplay_Stage = Gameplay_Stage_Manager.Instance.getGameplay_Stage();
        stageData = Gameplay_Stage_Manager.Instance.stageData;
        sceneName = Gameplay_Stage_Manager.Instance.currentlevelName;
    }


    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Begin>().GetComponent<Begin>();
        }
       
    }

    private void ResetStage()
    {
        gameplay_Stage.SetStage(stageData[sceneName]);
        List<CardGameSetting> packets = Gameplay_Stage_Manager.Instance.getCardsPackets();
        for (int i = 0; i < packets.Count; i++)
        {
                Debug.Log("packet is here");
                StartCoroutine(packets[i].SpawnCharacter());
        }
    }

    private void ResetBoss()
    {
        b = Gameplay_Stage_Manager.Instance.getBoss();
        Gameplay_Stage_Manager.Instance.ResetBoss();
    }

    public string setSceneName(string str)
    {
        return sceneName = str;
    }





    public IEnumerator PlayAct(bool stopOrPlayAct)
    {
        SetUp();
        ResetStage();
        ResetBoss();
      
        Act_Manager.Instance.setEnable(true);
        yield return null;
    }
}
