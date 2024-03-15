using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionBox : MonoBehaviour
{
    //data representation of the option box
    float x = 0, y = 0;
    Vector2 optionBoxPosition;
    string dialogue;
    Image img;
    Color color;
    ParticleSystem particleSystem;
    TextMeshProUGUI textMesh;
    RectTransform rect;
    GenericEventMethod[] genericEventMethod;
    bool isWobbling;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        img = GetComponent<Image>();
        particleSystem.playOnAwake = false;
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
    }
    public void setUp(float x, float y, string line, Color color, GenericEventMethod[] g)
    {
        //setup
        genericEventMethod = g;
        this.color = color; //<- colors the image
        img.color = color;
        textMesh.text = line;
        this.dialogue = line;
        //play particle system
        particleSystem.Play();
        this.x = x;
        this.y = y;//sets up the dialog box in a way that when the movement stops...
            //the code reads its previous position. [animation looks more seemlesss.]


        rect.localPosition = new Vector3(x, y, 0);
    }
    public void Run()
    {
        setUp(x, y, dialogue,new Color(150,150,150,256),genericEventMethod);
        MovementManager.Instance.Wobble(transform, isWobbling, Mathf.PI/20, 0.5f);
    }
    public void Select()
    {
        for(int i = 0; i < genericEventMethod.Length; i++)
        {
            genericEventMethod[i].playGenericMethod();
        }
    }
    public bool setIsWobbling(bool isItWobbling)
    {
        isWobbling = isItWobbling;
        if(!isWobbling)
        {
            MovementManager.Instance.StopAllCoroutines();
        }
        return isWobbling;
    }

}