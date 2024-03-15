using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float alpha = 1;
    public IEnumerator DissapearCoroutine()
    {
        Debug.Log("start coroutine");
        yield return new WaitForSeconds(2f);

       
        while(alpha > 0)
        {
            Debug.Log("alphaing " + alpha);
            alpha -= 0.01f;
            spriteRenderer.color = new Color(1f,1f,1f,alpha);
            yield return new WaitForSeconds(0.01f);
        }
       
       
       
        Debug.Log("Destruction!");
        Destroy(gameObject);
       
        yield return null;
        
    }
    public void Dissapear()
    {
        StartCoroutine(DissapearCoroutine());
    }
    public void setSprite(Sprite s,Color color)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = s;
        spriteRenderer.color = color;
    }
}
