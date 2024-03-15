using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextUI : MonoBehaviour
{
    TextMeshProUGUI textUI;
    private void OnEnable()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }
    public void DisableText()
    {
        textUI.text = "";
    }
}
