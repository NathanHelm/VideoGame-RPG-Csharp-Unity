using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Image_SetNativeSize : MonoBehaviour
{
    [SerializeField]
    Image s;
    float width,height;
    float scale = 1.5f;
    RectTransform rectTransform;
    Animator animator;
    // Start is called before the first frame update
    private void OnEnable()
    {
        s = GetComponent<Image>();
        animator = GetComponent<Animator>();
       
    }
    public void SetImageSize()
    {
        s.enabled = true;
        s.SetNativeSize();
        width = s.rectTransform.rect.width;
        height = s.rectTransform.rect.height;
        s.rectTransform.localScale = new Vector3(CalculateScale(scale,width), CalculateScale(scale,height));
       
    }
    float CalculateScale(float scale,float rectScale)
    {
        scale /= rectScale;
        scale *= 10;
       // Debug.Log(scale);
        return scale;
    }
    public void ChangeImage(Sprite sprite)
    {
        Debug.Log("change Image");
        EnableAvatar();
        if (sprite == null){
            DisableImage();
        }
        else
        {
            s.sprite = sprite;
            s.color = new Color(255, 255, 255, 255);
        }
    }
    public void DisableImage()
    {
        //s.sprite = null;
       // s.color = new Color(0, 0, 0, 0);
    }
    public void EnableAvatar()
    {
        animator.Play("Avatar_Appear");
        
    }
    




}
