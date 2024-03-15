using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CanvasComp_Dictionary : MonoBehaviour
{
    public static CanvasComp_Dictionary Instance;
    private Dictionary<string, CanvasComp> canvasCompNameToCanvasComp = new Dictionary<string, CanvasComp>();

    public void AddAllCanvasCompInLevel(CanvasComp[] canvasComps)
    {
        foreach (CanvasComp single in canvasComps)
        {
            canvasCompNameToCanvasComp.Add(single.gameObject.name, single);
        }
    }
    public CanvasComp getCanvasComp(string key)
    {
        try
        {
            if (canvasCompNameToCanvasComp.ContainsKey(key))
            {
                return canvasCompNameToCanvasComp[key];
            }
            else
            {
                throw new NullReferenceException();
            }

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e + "key does not exist");
        }
        return null;

    }

    private void OnEnable()
    {
        Instance = FindObjectOfType<CanvasComp_Dictionary>();
        AddAllCanvasCompInLevel(FindObjectsOfType<CanvasComp>());
    }
}
