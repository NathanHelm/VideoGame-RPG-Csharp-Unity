using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhoneManager : MonoBehaviour
{
    public NumberBox numberBox;
    
    List<int> numbers;
    [SerializeField]
    List<float> z;
    int amount;
    [SerializeField]
    List<TelephoneNumberCircle> telephoneNumberCircle;
    Transform clockTransform;
    
    private void OnEnable()
    {
        numberBox = FindObjectOfType<NumberBox>().GetComponent<NumberBox>();
        clockTransform = GameObject.Find("Rotary_dial").transform;
        z.Add(-45);
        for(int i = 1; i < telephoneNumberCircle.Count; i+=1)
        {
            z.Add(-(45+30*i));
           
        }
       
    }
    private void Update()
    {

        amount = CircleSelector(amount);
        //Debug.Log(amount);
        telephoneNumberCircle[amount].HoverNumber();
        MakeCirclesDefaultColor();

    }
    int CircleSelector(int amount)
    {
        //logic for moving the circle in all directions
        int max = telephoneNumberCircle.Count - 1;


        if (Input.GetKeyDown(KeyCode.A))
        {
            ++amount;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            --amount;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
           
            int num = telephoneNumberCircle[amount].Number;
            telephoneNumberCircle[amount].SelectNumber(clockTransform,z[amount]);
            numberBox.ClockNumber(num);
        }
        if (amount > max)
        {
            amount = 0;
        }
        else if (amount < 0)
        {
            amount = max;
        }



        return amount;
    }
    void MakeCirclesDefaultColor()
    {
        for (int i = 0; i < telephoneNumberCircle.Count; i++)
        {
            if (i != amount)
            {
                telephoneNumberCircle[i].Default();
            }
        }
    }
}
