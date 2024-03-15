using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCard : CardGameSetting
{
    private List<Enemy> enemies;
    public List<Enemy> GetEnemies()
    {
        enemies = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies();
        return enemies;
    }
    public override IEnumerator SpawnCharacter()
    {
       
        yield return null;
    }

    public void BossCardFollowPos(Vector3 position)
    {
        Vector2.MoveTowards(transform.position, position, 5f);
    }
}
