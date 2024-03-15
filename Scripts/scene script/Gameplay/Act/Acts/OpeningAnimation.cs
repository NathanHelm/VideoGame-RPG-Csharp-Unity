using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningAnimation : MonoBehaviour, IAct
{
    private List<Enemy> enemies;
    private List<Item> items;
    private List<Box_OpeningAnimation> box_OpeningAnimations = new List<Box_OpeningAnimation>();
    private Black_Image black_Image;
    private List<Box_OpeningAnimation> largestBoxAmount = new List<Box_OpeningAnimation>() , temp = new List<Box_OpeningAnimation>();
    UIScript UIScript;
    private void OnEnable()
    {
        UIScript = FindObjectOfType<UIScript>().GetComponent<UIScript>();
        black_Image = FindObjectOfType<Black_Image>().GetComponent<Black_Image>();
    }
    public void DisplayBackground()
    {
        black_Image.setOpague();
    }
    public void DissolveBackground()
    {
       StartCoroutine(black_Image.OpagueToTransparent());
    }
 
    public void OpeningAnim()
    {
   
        DisplayBackground();
        enemies = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurEnemies();
        items = Gameplay_Stage_Manager.Instance.getGameplay_Stage().getCurItems();
        box_OpeningAnimations = new List<Box_OpeningAnimation>();
        int number = 0;
 

        DialogueManager.Instance.spawnText("These Enemies Want You Dead.", new Vector3(-250, 75f));
        for(int i = 0; i < enemies.Count; i++)
        {
            Vector2 pos = new Vector2((-250 + number), 0);
            MakeBox(enemies[i].avatar, enemies[i].GamePlay_Obj_name, pos);
            number += 150; 
        }
        largestBoxAmount = FindLargestGroup(temp);
        number = 0;
        DialogueManager.Instance.spawnText("These Items Will Help You Recover.", new Vector3(-250, 250.5f * 1.1f));
        for (int i = 0; i < items.Count; i++)
        {
            Vector2 pos = new Vector2((-250 + number), 180);
            MakeBox(items[i].avatar, items[i].GamePlay_Obj_name, pos);
            number += 150;
        }
        largestBoxAmount = FindLargestGroup(temp);
        DialogueManager.Instance.spawnText("These Are The Ablilites.", new Vector3(-250, -180));
        largestBoxAmount = FindLargestGroup(temp);
        
    }
    public void MakeBox(Sprite sprite, string name, Vector3 pos)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Opening_Animation/Opening_animation_avatar");
        Debug.Log("prefab! ->" + prefab.name + "position ->" + pos);
        Box_OpeningAnimation box_OpeningAnimation = Instantiate(prefab, UIScript.transform).GetComponent<Box_OpeningAnimation>();
        box_OpeningAnimation.SetUp(sprite, name, pos);
        box_OpeningAnimations.Add(box_OpeningAnimation);
        temp.Add(box_OpeningAnimation);
        box_OpeningAnimation.FindPosition();
    }
    public List<Box_OpeningAnimation> FindLargestGroup(List<Box_OpeningAnimation> temp1)
    {
        //gathers the individual coordinates of the 'opening animation' rows
        //In the last row, it performs a 
        if(largestBoxAmount.Count < temp1.Count)
        {
            return temp1;
        }
        if(largestBoxAmount.Count == temp1.Count)
        {
            // Vector2.Distance(largestBoxAmount[largestBoxAmount.Count - 1], )
            //   if()
            //  return
            return temp1; 
        }
            temp1 = new List<Box_OpeningAnimation>();  
        return largestBoxAmount;
        
    }
    public void DestoyBoxs()
    {
        for(int i = 0; i < box_OpeningAnimations.Count; i++)
        {
            box_OpeningAnimations[i].Destroy();
        }
    }

    public IEnumerator PlayAct(bool enable)
    {
        OpeningAnim();
        yield return new WaitUntil(() => !largestBoxAmount[largestBoxAmount.Count - 1].ispositionFound());
        Debug.Log("continuing pt 2");
        for(int i = 0; i < box_OpeningAnimations.Count; i ++)
        {
            box_OpeningAnimations[i].Display(); 
        }
        yield return new WaitForSeconds(2.5f);
        DissolveBackground();
        DestoyBoxs();
        DialogueManager.Instance.DestroyText();
        Act_Manager.Instance.setEnable(true);
        yield return null;
    }


}
