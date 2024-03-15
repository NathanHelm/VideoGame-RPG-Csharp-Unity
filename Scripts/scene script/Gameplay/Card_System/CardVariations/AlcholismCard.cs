using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcholismCard : CardGameSetting
{
    Vector3 targetPosition;
    
    private void SetUpAlcholismCard()
    {
        SetUpCardGameSetting("couch", "Prefab/Cards/drunkcard", "Big Beer", "Enemies get a little tipsy", "enemy");

    }
   
  
    public IEnumerator CardMovementCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            int i = Random.Range(0, stage.getCardCords().Length - 1);
            targetPosition = stage.getCardCords()[i];
            rb.AddForce(rb.drag * rb.mass * targetPosition, ForceMode2D.Impulse);
            transform.Rotate(new Vector3(0, Time.deltaTime, 0));
        }
    }
    private void OnEnable()
    {
        SetUpAlcholismCard();
        CardMovement();
        AddToCardUI();
    }
    //all overriden members.
    public override void CardUIMovement(RectTransform rect)
    {
        //custom ui movement.
        base.CardUIMovement(rect);
    }
    public override IEnumerator PlayAfterSpawn()
    {
       
        gameSettings = new List<IGameSetting>();
        AddInterfaces();
        for (int i = 0; i < gameSettings.Count; i++)
        {
            gameSettings[i].AlcholismCard();
        }
        int total = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies().Count;
        while (true)
        {
            yield return new WaitUntil(() => Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies().Count > total);
           Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies()[Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies().Count - 1]
           .AlcholismCard();
           total = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies().Count;
        }

    }

    public override void CardMovement()
    {
        //custom movement
        StartCoroutine(CardMovementCoroutine());
    }
}
