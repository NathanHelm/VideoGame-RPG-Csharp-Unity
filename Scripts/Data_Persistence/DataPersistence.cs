using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour,IDataPersistence
{
    public virtual void Add(Dictionary<string, object> keyValuePairs)
    {
       // throw new System.NotImplementedException();
    }

  
    public virtual void Load(Dictionary<string, object> keyValuePairs)
    {
      //  throw new System.NotImplementedException();
    }
   
}
