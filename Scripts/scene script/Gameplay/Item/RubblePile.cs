using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubblePile : MonoBehaviour
{
    List<Sprite> sprites = new List<Sprite>();
    public void AddSprite(Sprite s)
    {
        sprites.Add(s);
    }
    private void SetUp()
    {
        Sprite[] rubbleSprites = Resources.LoadAll<Sprite>("Gameplay/Gameplay_Sprites/Rubble_Sprites");
        foreach (Sprite rub in rubbleSprites)
        {
            Debug.Log("adding zi sprites");
            sprites.Add(rub);
        }
    }
    public void SpawnRubble(Color c, Vector3 spawnPos)
    {
        if(sprites.Count <= 0)
        {
            SetUp();
        }
        for (int i = 0; i < sprites.Count / 2; i++)
        {
            GameObject g = Resources.Load<GameObject>("Prefab/Item/Rubble/rubble_obj");
            Rubble r = Instantiate(g).GetComponent<Rubble>();
            r.transform.position = spawnPos;
            r.setSprite(sprites[i], c);
            r.Dissapear();
        }
    }

    
}
