using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_Object : MonoBehaviour
{
    public Sprite avatar;
    public int hp;
    public string path;
    public string GamePlay_Obj_name;
    public Vector2[] spawnPositions;

    public void SetUp(int hp)
    {
       this.hp = hp;
    }

}
