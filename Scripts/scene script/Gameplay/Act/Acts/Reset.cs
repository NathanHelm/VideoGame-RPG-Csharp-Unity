using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour, IAct
{
    public IEnumerator PlayAct(bool stopOrPlayAct)
    {
        AIManager.Instance.StopAIPathFinding();
        //destroy all 'current' gameplay objects and all gameplay objects in the scene. 
        List<GameObject> gameplay_Object = Gameplay_Stage_Manager.Instance.getGameplay_Stage().GetCurGameObjects();
        for(int i = 0; i < gameplay_Object.Count; i++)
        {
            Debug.Log("gameplay_Object.Count" + gameplay_Object.Count);
            Destroy(gameplay_Object[i]);
        }
        Gameplay_Object[] gs = FindObjectsOfType<Gameplay_Object>();
        for(int i = 0; i < gs.Length; i++)
        {
            Destroy(gs[i].gameObject);
        }
        Gameplay_Stage gameplay_Stage = Gameplay_Stage_Manager.Instance.getGameplay_Stage();
        Boss b = Gameplay_Stage_Manager.Instance.getBoss();
        b.StopAllBodyMovement();
        b.StopAllCoroutines();
        b.LookAtCards();
        //remember this for reversing gameplay...
        // Destroy(gameplay_Stage.GetCurGameStage_Environment().gameObject);
        //gameplay_Stage.SpawnStage();
        CameraManager.Instance.RemoveCam("CinemachineCamera(Clone)");
        Act_Manager.Instance.setEnable(true);
        yield return null;
    }
}
