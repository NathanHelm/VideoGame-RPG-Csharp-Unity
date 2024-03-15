using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManger: MonoBehaviour
{

    private void Start()
    {
        
    }
    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.Z))
        {
            DataPersistenceManager.Instance.SaveDataToJSON();
            Level_Manager.Instance.ChangeScene(0, null);
        }
      if(Input.GetKeyDown(KeyCode.X))
        {
            DataPersistenceManager.Instance.LoadDataFromJSON();
        }
    }


}
