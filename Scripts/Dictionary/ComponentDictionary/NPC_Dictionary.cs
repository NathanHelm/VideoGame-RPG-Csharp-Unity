using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NPC_Dictionary : MonoBehaviour
{
    public static NPC_Dictionary Instance;
    [SerializeField]
    private NPC dNPC;
    private Dictionary<string, NPC> npcNameToNPc = new Dictionary<string, NPC>();

    public void AddAllNPCsInLevel(NPC[] npc)
    {
        foreach(NPC single in npc)
        {
            npcNameToNPc.Add(single.gameObject.name, single);
        }
    }
    public NPC getNPC(string key)
    {
        try
        {
            if (npcNameToNPc.ContainsKey(key))
            {
                return npcNameToNPc[key];
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


    public void setDialogueNPC(NPC nPC)
    {
        this.dNPC = nPC;
    }
    public NPC getDialogueNPC()
    {
       // Debug.Log("dNPC" + dNPC.gameObject.name);
        return dNPC;
    }
    
    private void OnEnable()
    {
        Instance = FindObjectOfType<NPC_Dictionary>();
        AddAllNPCsInLevel(FindObjectsOfType<NPC>());
    }
}
