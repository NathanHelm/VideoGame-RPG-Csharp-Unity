using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class FileHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public string getDataFileName()
    {
        return dataFileName;
    }

    public Queue<object> Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Queue<object> queue = new Queue<object>();
        if (File.Exists(fullPath))
        {
            try
            {
                //make a data field within the json file which contains the type we want to access
                //
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                //   Debug.Log("Data to load->" + dataToLoad);
                string[] alljsonLines = dataToLoad.Split('\n');
                for (int i = 1; i < alljsonLines.Length; i++)
                {
                    Debug.Log("all json lines hoe " + alljsonLines[i] + " " + dataFileName);
                    Data temp = JsonUtility.FromJson<Data>(alljsonLines[i]);
                    Type t = Type.GetType(temp.type);
                    object o = JsonUtility.FromJson(alljsonLines[i], t);
                    queue.Enqueue(o);
                    Debug.Log("the type of these hoes: " + o.GetType());
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e + "error!");
            }
        }
        else
        {
            Debug.LogError("file does not exist");
        }
        
        return queue;
    }
    public void ResetFile()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        File.WriteAllLines(fullPath, new string[] { });
    }
    public void MakeFile()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if(!File.Exists(fullPath))
        {
            File.Create(fullPath);
        }
        else
        {
            Debug.Log("File_already_exists");
        }
        
    }
    public void MakeDictionary()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        }
        else
        {
            Debug.Log("made the dictionary->" + fullPath);
        }
        
    }
    public void Save(string json, Type data)
    {
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            MakeDictionary();
            try
            {
                //create directory path
                //MakeDictionary();
       
                // Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = json;
                using (FileStream stream = new FileStream(fullPath, FileMode.Append))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                    writer.WriteLine();
                    writer.Write(dataToStore);
                    }
                }

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to sace data to file:" + fullPath + "\n" + e);
            }
        
    }

}
