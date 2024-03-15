using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class NumberBox : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textmesh;
    string line,line1,line2,line3;
    [SerializeField]
    List<int> numbers;
    [SerializeField]
    DialogueManager dialogueManager;
    //data persistance
    Vector3 playerPos;


    // Update is called once per frame
    public void ClockNumber(int managerNumber)
    {
        //adds a 
        numbers.Add(managerNumber);
       
        //runs 4 times, displays the line in the text mesh.
        if (numbers.Count <= 3)
        {
            line1 = FindLine(line1, 3);
        }
        else if (numbers.Count > 3 && numbers.Count < 7)
        {
            line2 = FindLine(line2, 6);
        }
        else if(numbers.Count < 11)
        {
            line3 = FindLine(line3, 10);
        }

        line = line1 + " " + line2 + " " + line3;
        textmesh.text = line;
        Debug.Log(line);
        Check();

    }

    void Check()
    {
        if (line.Trim().Length > 18)
        {
            Debug.Log("line is longer than 18" + line.Trim().Length);
            if (line.Trim() == "6-0-4 9-4-3 1-0-4-1") //transition to scene 1
            {
                Level_Manager.Instance.ChangeScene(2, null);
            }
            else if (line.Trim() == "7-8-8 4-3-1 4-0-0-9") // transition to scene 3
            {
                Level_Manager.Instance.ChangeScene(3, null);
                Debug.Log("Change to scene 3");
            }
            else
            {
                NPC nPC = new NPC();
                //dialog key.
                nPC.setDialogueKey("TELEPHONEBANTER/1");
                Level_Manager.Instance.ChangeScene(Level_Manager.Instance.GetPreviousSceneNumber(), null);
                //    new GenericEventMethod[] { new SingleParameterMethod<NPC>(NPC_Manager.Instance.TriggerDialog, nPC) });
            }
        }



    }

    string FindLine(string line, int maxNumberInString)
    {
        
        for (int i = numbers.Count-1; i < numbers.Count; i++)
        {
            //made new line
           
            string newline;
            //new line = 0
            newline = numbers[i].ToString() + "-";
            if (numbers.Count >= maxNumberInString)
            {
                newline = numbers[i].ToString();
            }
            //line = 0
            
            line += newline;
        }
        return line;
    }
    
}
