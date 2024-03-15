using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueBox : MonoBehaviour
{
    private Image dialogueBox;
    public void DisableDialogueBox()
    {
        dialogueBox.enabled = false;
    }
    public void EnableDialogueBox()
    {
        dialogueBox.enabled = true;
    }
    private void OnEnable()
    {
        dialogueBox = GetComponent<Image>();
    }
}
