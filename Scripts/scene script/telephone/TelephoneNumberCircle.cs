using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneNumberCircle : MonoBehaviour
{

    float amount;
    public int Number;
    Color c = Color.clear;
    bool _returnClock = true;
    [SerializeField]
    bool runOnce = false;

    SpriteRenderer spriteRenderer;
    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //angle = (Mathf.PI / 6);


    }
    public void SelectNumber(Transform transform, float z)
    {
        if (runOnce == false)
        {
            StartCoroutine(RotateNumber(transform, z));
        }
    }
    public void HoverNumber()
    {
        spriteRenderer.color = Color.red;
    }
    public void Default()
    {
        spriteRenderer.color = Color.clear;
    }
    //donut rotation
    public IEnumerator RotateNumber(Transform t, float z)
    {
        runOnce = true;
        _returnClock = true;
        while (true)
        {

            //angle is descreasing
            /*
            transform.rotation = new Vector3(x, y, 0, 0);
            x = Mathf.Cos(angle);
            y = Mathf.Sin(angle);
            angle -= (Mathf.PI/20);
            if(x >= Mathf.Cos((7*Mathf.PI)/4)&& y >= Mathf.Sin((7 * Mathf.PI) / 4))
            {
                break;
            }
            /*/

           // Debug.Log(amount);
            t.rotation = Quaternion.Euler(0, 0, amount);
            if (_returnClock == false && amount >= 0)
            {
                break;
            }
            if (amount < z)
            {
                _returnClock = false;
            }
            if (_returnClock)
            {
                amount -= 4;
            }
            if (!_returnClock)
            {
                amount += 4;
            }



            yield return new WaitForFixedUpdate();
        }
        runOnce = false;
        yield return null;

    }
}
