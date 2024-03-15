using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    private Animator animator;
    private Image image;
    public static Avatar Instance;

    public void DisableAvatar()
    {
        image.sprite = null;
        image.color = new Color(256, 256, 256,0);
    }

    public IEnumerator PlayAvatarAnimation()
    {
        int alpha = 0;
        image.color = new Color(256, 256, 256, alpha);
        while(alpha <= 256)
        {

            alpha++;
            image.color = new Color(256, 256, 256, alpha);
            yield return new WaitForSeconds(0.005f);
        }
    }
    //getters plus setters
    public void SetSprite(Sprite s)
    {
        image.sprite = s;
    }
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
        
    }
    private void Awake()
    {
        Instance = this;
    }
    // public void Set
}
