using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_Stage_Data
{
    public Vector2[] areaCords;
    public Vector2[] enemySpawnPos;
    public Vector2[] barrierSpawnPos;
    public Vector2[] stageBoundsCords;
    public Vector2[] cardSpawnCords;
    public string[] enemies;
    public string[] items;
    private string gameplayStagePath = "Prefab/Stage/";
    public string path;
    public string gameStageName;
    public Gameplay_Stage_Data(string gameStagefileName, Vector2[] enemySpawnPos, Vector2[] barrierSpawnPos, string[] enemies, string[] items, Vector2[] stageBoundsCords, Vector2[] cardSpawnCords, Vector2[] areaCords)
    {
        this.path = gameplayStagePath + gameStagefileName;
        this.enemySpawnPos = enemySpawnPos;
        this.barrierSpawnPos = barrierSpawnPos;
        this.enemies = enemies;
        this.items = items;
        this.stageBoundsCords = stageBoundsCords;
        this.gameStageName = gameStagefileName;
        this.cardSpawnCords = cardSpawnCords;
        this.areaCords = areaCords;
    }
}
