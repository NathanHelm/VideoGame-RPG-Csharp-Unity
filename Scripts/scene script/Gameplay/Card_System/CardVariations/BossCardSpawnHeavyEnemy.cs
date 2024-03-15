using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCardSpawnHeavyEnemy : BossCard
{
    List<Enemy> strongEnemy = new List<Enemy>();
    private void OnEnable()
    {
        SetUpCardGameSetting("heavyclansmen", "Prefab/Cards/heavyenemy", "spawns heavy enemy", "they hurt ya!", "enemy");
    }
    public Enemy getStrongEnemy()
    {
        List<Enemy> enemies = GetEnemies();
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] is IHeavyEnemy)
            {
                strongEnemy.Add(enemies[i]);
            }
            Debug.LogError("IHeavyEnemy has not been implemented.");
        }
        return null;
    }
    public override IEnumerator SpawnCharacter()
    {
       // getStrongEnemy();
        if (strongEnemy.Count > 0)
        {
            int n = Random.Range(0, strongEnemy.Count - 1);
          //  spawnName = strongEnemy[n].GamePlay_Obj_name;
        }
        stage = Gameplay_Stage_Manager.Instance.getGameplay_Stage();
        stage.AddEnemy("heavyclansmen");
        //base.SpawnCharacter();
        yield return null;
    }
    public override IEnumerator PlayAfterSpawn()
    {
        yield return null;
    }
    public override IEnumerator CardUIAttack()
    {
        //todo make a system that will either other string enemeies.
        yield return null;
    }
    
}
