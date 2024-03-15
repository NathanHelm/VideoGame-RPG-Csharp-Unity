using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardSelectionStage: CardSelectionStage
{
    List<Item> items;

    public override void Interact(Sprite icon)
    {
        items =  CardManager.Instance.getItems();
        for (int i = 0; i < items.Count; i++)
        {
            SpawnBox(items[i].avatar,GetComponent<ParticleSystem>());
        }
    }
    public override void LookAtStage()
    {
          CameraManager.Instance.CameraChanger(CameraManager.Instance.priCamIndx, CameraManager.Instance.getIndex("card_selection_tv_camera"));
    }


}