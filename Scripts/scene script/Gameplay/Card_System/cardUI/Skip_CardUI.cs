using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skip_CardUI : CardUI
{
    private bool skipTheRound = true;
   
    public override void PlayCardUI()
    {
        //skips this
        setskipTheRound(false); 
    }

    public void setskipTheRound(bool b)
    {
        skipTheRound = b;
    }
    public bool getSkipTheRound()
    {
        return skipTheRound;
    }
    public void SetUpSkipCardUI()
    {
        cardSelectedStage = "boss";
        TextMeshProUGUI[] textArray = GetComponentsInChildren<TextMeshProUGUI>();
        text = textArray[0];
        textInfo = textArray[1];
        rectTransform = GetComponent<RectTransform>();
        text.text = "Skip";
        textInfo.text = "You skip this round.";
        
    }

         
}
