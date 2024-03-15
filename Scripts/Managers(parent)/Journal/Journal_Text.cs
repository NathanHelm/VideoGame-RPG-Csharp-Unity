using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Journal_Text : MonoBehaviour
{
    TextMeshProUGUI textUI;
    public void Translation(string line)
    {
        textUI.text = line;
        
    }
    public void EndTranslation()
    {
        textUI.text = "";
    }
    private void OnEnable()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }
}
