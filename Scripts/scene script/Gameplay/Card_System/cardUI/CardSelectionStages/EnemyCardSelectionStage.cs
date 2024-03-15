using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardSelectionStage : CardSelectionStage
{
    List<Enemy> enemy = new List<Enemy>();
    List<Enemy> boxEnemyFromStage = new List<Enemy>();
    ParticleSystem particleSystem = null;

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }
    public IEnumerator SpawnCoroutine()
    {
        particleSystem = GetComponent<ParticleSystem>();
        enemy = CardManager.Instance.getEnemies();
        if (boxEnemyFromStage.Count - 1 <= enemy.Count -1)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                SpawnBox(enemy[i].avatar, particleSystem);
                boxEnemyFromStage.Add(enemy[i]);
                yield return new WaitForSeconds(1f); //there for a reason, rigidbody tends to clip.
            }
        }
    }
    public override void LookAtStage()
    {
        Debug.Log("looking at stage card tv camera" + CameraManager.Instance.getIndex("card_selection_tv_camera") + " " + CameraManager.Instance.priCamIndx);
        Spawn(); 
        CameraManager.Instance.CameraChanger(CameraManager.Instance.priCamIndx, CameraManager.Instance.getIndex("card_selection_tv_camera"));
    }


    public override void Interact(Sprite icon)
    {
        particleSystem = GetComponent<ParticleSystem>();
        Debug.Log("doing the interact");
        SpawnBox(icon, particleSystem);

    }
}