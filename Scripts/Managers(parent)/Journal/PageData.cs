 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PageData : Data
{
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private string translationText;

    public PageData(string translationText, Sprite sprite)
    {
        this.sprite = sprite;
        this.translationText = translationText;
        Debug.Log("instantiated page data amount" + Journal_Manager.Instance.getPageAmount());
        this.hashcode = "paper" + Journal_Manager.Instance.getPageAmount();
        this.type = this.GetType().ToString();
    }

    public Sprite getSprite()
    {
        return sprite;
    }
    public string getTranslationText()
    {
        return translationText;
    }
   


}
