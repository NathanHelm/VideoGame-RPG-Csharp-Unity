using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class AIManager : MonoBehaviour
{
    List<Collider2D> col = new List<Collider2D>();
    public static AIManager Instance;
    IEnumerator coroutine; 
    private void OnEnable()
    {
        Instance = FindObjectOfType<AIManager>().GetComponent<AIManager>();
    }
    public void StopAIPathFinding()
    {
        StopCoroutine(coroutine);
        col = new List<Collider2D>();
    }
    private IEnumerator PathFinding()
    {
        while (true)
        {
          
            // Expand the bounds along the Z axis
            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] != null)
                {
                    Debug.Log("col length" + col.Count + " col" + col[i].gameObject.name);
                    Bounds b = col[i].bounds;
                    // change some settings on the object
                    var guo = new GraphUpdateObject(b);
                    //guo.updatePhysics = true;
                    AstarPath.active.UpdateGraphs(guo);
                }
            }
            var current = AstarPath.active.data.gridGraph;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    //come before enemies are decided.
    public void PlayPathFinding(Item[] items, Card[] cards)
    {
        int n = Mathf.Max(items.Length, cards.Length);
        for (int i = 0; i < n; i++)
        {
            if (i < items.Length - 1)
            {
                Debug.Log("col length ppf" + n);
                try
                {
                    Collider2D c = items[i].gameObject.GetComponent<Collider2D>();
                    col.Add(c);
                }

                catch (NullReferenceException e)
                {
                    Debug.LogError(e);
                }
            }
            if (i < cards.Length - 1)
            {
                try
                {
                    Collider2D c = cards[i].gameObject.GetComponent<Collider2D>();
                    col.Add(c);
                }
                catch (NullReferenceException e)
                {
                    Debug.LogError(e);
                }
            }
        }
        StartCoroutine(coroutine = PathFinding());
    }
}
