using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Manager : MonoBehaviour
{
    //bruh what
    public void ChangeTriggerEvent(TriggerEvent trigger, string s)
    {
        trigger.setKey(s);
    }
}
