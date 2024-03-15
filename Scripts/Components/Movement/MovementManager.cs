using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;
    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<MovementManager>().GetComponent<MovementManager>();
        }
    }
    public void Wobble(Transform trans, bool selected, float angle,float amplitude)
    {
        StartCoroutine(WobbleCoroutine(trans, selected, angle, amplitude));

    }
    public void Follow(Transform trans, bool selected, Vector3 target, float moveSpeed)
    {
     //  StartCoroutine(FollowPlayerCoroutine(trans, selected, target, moveSpeed));
    }
    public void MotherCaught(Transform trans, Transform target)
    {
        StartCoroutine(MotherCaughtMovementCoroutine(trans, target));
    }
    public IEnumerator WobbleCoroutine(Transform trans, bool selected, float angle, float amplitude)
    {
        Transform t = trans;
        // float amplitude = 0.5f;
        float frequency = 0;
        //float angle = 
        float y = t.position.y;
        
        float constant = t.position.x;
        Debug.Log(constant + "constant");
        while (selected)
        {
           // Mathf.PI / 20
            frequency += angle;
            float wobblingY = Mathf.Sin(frequency) * amplitude;
            //Debug.Log(y + wobblingY + "y pos wobbling");
            float y1 = y + wobblingY;
            trans.position = new Vector3(constant, y1, 0);
            yield return new WaitForFixedUpdate();
        }
       
    }
   public IEnumerator FollowPlayerCoroutine(Transform trans, bool selected, Vector3 target, float moveSpeed)
    {
        
        while(selected)
        {
            Vector3 direction = Vector3.MoveTowards(trans.localPosition, target, moveSpeed);

         //   Debug.Log("direction " + direction);
            // Move towards the player using the calculated direction and speed
            trans.localPosition = direction;
//            Debug.Log(trans.position + "transform.position" + target + "target.position");
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
    IEnumerator MotherCaughtMovementCoroutine(Transform trans,Transform playerT)
    {
        float x = 0;
        float y = 0;
        float degree = 0;
        while (true)
        {
        //    Debug.Log("Distance: " + Vector3.Distance(trans.position, playerT.position));
            if (Vector3.Distance(trans.position, playerT.position) < 30)
            {
                degree += Mathf.PI / 50;
                y = Mathf.Sin(degree);

                x += Time.deltaTime / 2;

                Vector3 vec = new Vector3(x, y, 0);

                trans.position = new Vector3(x + trans.position.x, y + trans.position.y, 0);
            }
            else
            {
                degree += Mathf.PI / 50;
                y = Mathf.Sin(degree);
                x += 0.5f * (Time.deltaTime / 25);
                trans.position = new Vector3(trans.position.x + x, trans.position.y + y, 0);
            }
            yield return new WaitForFixedUpdate();
        }
        yield return null;

    }
    public IEnumerator Scale(Transform scaler, Vector3 yield, float spd)
    {
        while (scaler.localScale.x >= yield.x)
        {
           scaler.localScale -= new Vector3(spd, 0, 0);
        }
        while(scaler.localScale.y >= yield.y)
        {
            scaler.localScale -= new Vector3(0, spd, 0);
            yield return new WaitForFixedUpdate();
        }    
        yield return null;
    }

    public bool ispositionFound(Transform t,Vector3 target)
        {
            float dist = Vector2.Distance(t.localPosition, target);
            if (dist < 1f)
            {
              
                return false;
            }
            else
            {
                return true;
            }
        }
}
