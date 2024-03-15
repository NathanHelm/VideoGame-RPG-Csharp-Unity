using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    void Load(Dictionary<string, object> keyValuePairs);
    //adds the data into the static data persistence manager.
    void Add(Dictionary<string, object> keyValuePairs);
}

