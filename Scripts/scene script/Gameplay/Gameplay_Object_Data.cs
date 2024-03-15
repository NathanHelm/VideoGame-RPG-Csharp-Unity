using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gameplay_Object_Data", order = 1)]
public class Gameplay_Object_Data : ScriptableObject
{
    [SerializeField]
    public string GameObject_fileName;
    [SerializeField]
    public string Gameplay_Object_Path;
    public Sprite EnemySprite;
 
}
