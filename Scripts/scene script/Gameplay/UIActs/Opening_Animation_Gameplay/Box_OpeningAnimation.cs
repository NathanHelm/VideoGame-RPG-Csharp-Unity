using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
public class Box_OpeningAnimation : MonoBehaviour
{
    float halfwidth = 0;
    TextMeshProUGUI textUI;
    Image image;
    string enemyName;
    Sprite sprite = null;
    RectTransform rectTransform;
    Vector3 target;
    bool isWobbling = true;
    public void SetUp(Sprite sprite, string enemyName, Vector3 targetpos)
    {
        
        this.enemyName = enemyName;
        this.sprite = sprite;
        halfwidth = transform.localScale.x / 2;
        textUI = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        target = targetpos;
    }
    public void Display()
    {
        // textUI.
        Wobble();
        textUI.text = enemyName;
        image.sprite = sprite;

    }
    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
    public void FindPosition()
    {
        StartCoroutine(MovementManager.Instance.FollowPlayerCoroutine(rectTransform,ispositionFound(),target,7f));
    }
    public bool ispositionFound()
    {
        float dist = Vector2.Distance(rectTransform.localPosition, target);
      
        if(dist < .1f)
        {
         
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Wobble()
    {
       isWobbling = true;
       StartCoroutine(MovementManager.Instance.WobbleCoroutine(rectTransform, isWobbling, Mathf.PI/20, 1.2f));
    }
    public RectTransform getRectTransform()
    {
        return rectTransform;
    }

        





}