using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Controls<T> : MonoBehaviour
{
    public int index = 0;
    protected int previndx = 0;
    public IEnumerator selectOptionsCoroutine(List<T> list, int maxIndex)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            yield return new WaitUntil(() => !Input.GetKeyDown(KeyCode.Return));
        }
        beforeInput();
        doSomething(index);

        while (condition())
        {

            Debug.Log("condition" + index +" "+ previndx);
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
                {
                    beforeInput();
                    previndx = index;
                    ++index;
                if (index > maxIndex)
                {
                   
                    index = maxIndex;
                    previndx = index - 1;

                }
                    doSomething(index); //page flip animation

   
                }

                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
                {
                    beforeInput();
                    previndx = index;
                    --index;
                    if (index < 0)
                    {
                        
                        index = 0;
                        previndx = index + 1;
                    }

                    doSomething(index);
                    
                
                }
            
          
            yield return new WaitForSeconds(0);
        }
        yield return null;
    }
    public virtual void doSomething(int index)
    {

    }
    public virtual bool condition()
    {
        return true;
    }
    public virtual void beforeInput()
    {

    }



}
