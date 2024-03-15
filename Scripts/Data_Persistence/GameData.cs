using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //every game data class has data.
    //the data can be manipulated.
    //to gather all saved data, its smart to store it in a hashmap for easy access.
    // to gather a key pair value, 
    public Data data;
    public GameData(Data data)
    {
        this.data = data;
    }
    public void setData(string hash, Data newData)
    {


    }
}
public class Data
{
    public string hashcode;
    public string type;
}
public class NpcData : TriggerEventData
{
    public string dialogueKey;
    public Vector3 position;
    public NpcData(string dialogueKey, Vector3 position, string triggerKey, string hashcode)
    {
        this.hashcode = hashcode;
        this.dialogueKey = dialogueKey;
        this.position = position;
        this.triggerKey = triggerKey;
        type = this.GetType().ToString();
    }
}
public class TriggerEventData : Data
{
    public string triggerKey;
}
//subclasses
public class PlayerData : Data
{
    public string name;
    public Vector3 playerPos;
    public PlayerData(Vector3 set, string name)
    {
        this.name = name;
        playerPos = set;
            
    }
}

public class ItemsData : Data
{
    //items that you have collected in the game
}
