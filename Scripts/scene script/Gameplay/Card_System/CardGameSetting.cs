using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardGameSetting : Card
{
    //card have two main methods -> play interface, spawn character.
    //if you play a card and the card 'adds something to the game' spawn character allows this.
    //card UI, collects the interfaces and plays them, altering all gameobject in some 'unique' way.
    //however this is not always the case so its a good gesture to keep it virtual and open to be overriden. 



    protected List<IGameSetting> gameSettings = new List<IGameSetting>();
    protected Gameplay_Stage stage;
    protected string spawnName = "";
  
    public void SetUpCardGameSetting(string characterName,string filename, string name, string slogan, string cardSelectionStage)
    {
      stage = Gameplay_Stage_Manager.Instance.getGameplay_Stage();
      spawnName = characterName; 
      SetUpCard(filename, name, slogan, cardSelectionStage);
    }
    public void AddInterfaces()
    {
        List<IGameSetting> gameplay_Object_GameSetting = new List<IGameSetting>();
        gameplay_Object_GameSetting = FindObjectsOfType<MonoBehaviour>(true).OfType<IGameSetting>().ToList();
        gameSettings = gameplay_Object_GameSetting;
    }
    public void setStage(Gameplay_Stage stage)
    {
        this.stage = stage;
    }
    public virtual IEnumerator SpawnCharacter()
    {
        stage.AddItem(spawnName);
        yield return null;

    }
    public virtual IEnumerator PlayAfterSpawn()
    {
        //maybe add some animation that plays to indicate the card that are recieving change.
        Debug.LogError("unoverriden game setting attack");
        
        yield return null;
    }



}

