using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    public static ChildManager Instance;
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<ChildManager>();
        }
    }
    public GameObject MakeChildObject(string str, Transform parent)
    {
        GameObject g = Resources.Load<GameObject>(str);
        GameObject temp = Instantiate(g, parent);
        return temp;
    }
    

}
