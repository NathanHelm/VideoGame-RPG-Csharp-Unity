using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telphone_Texture : MonoBehaviour
{
    [SerializeField]
    Material material;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        material = GetComponent<SpriteRenderer>().material;
        StartCoroutine(DifferentPoints());
    }

    // Update is called once per frame
    IEnumerator DifferentPoints()
    {
        material.EnableKeyword("Float");
        material.EnableKeyword("range");
        for (float i = 0.01f; i <= 1; i+=0.01f)
        {
//            Debug.Log(i);
            yield return new WaitForFixedUpdate();
           
            material.SetFloat("_float", i);
        }
        for (int i = 0; i <= 1000; i++)
        {
         //   Debug.Log(i);
            yield return new WaitForFixedUpdate();
            material.SetVector("_range", new Vector4(0, i, 0, 0));
        }
        StartCoroutine(DifferentPoints());
        yield return null;
    }
}
