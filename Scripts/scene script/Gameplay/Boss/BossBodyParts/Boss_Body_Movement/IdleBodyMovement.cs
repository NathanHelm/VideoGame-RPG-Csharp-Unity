using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBodyMovement : BodyMovement
{
    float amplitude;
    float angle;
    float torqueForce;
    public void SetUpBodyMovementIdle(float amplitude, float angle, float torqueForce)
    {
        
        this.amplitude = amplitude;
        this.angle = angle;
        this.torqueForce = torqueForce;
    }

    public override void Transformation()
    {
        coroutines = new IEnumerator[2];
        Wobble();
        SlightRotation();
    }
    public override void StopTransformation()
    {
        base.StopTransformation();
    }
    private void Wobble()
    {
        rb.angularDrag = 3;
        rb.drag = 25;
        StartCoroutine(coroutines[0] = IdleCoroutine(bodyPartTransform, amplitude, angle));
    }
    public void SlightRotation()
    {
        StartCoroutine(coroutines[1] = SlightRotationCoroutine(10));
    }
    public IEnumerator SlightRotationCoroutine(float torqueF)
    {
        bool runnin = false;
        bodyPartTransform.eulerAngles = new Vector3(0, 0, 0);
        while (true)
        {
            
            yield return new WaitForSeconds(0.5f);
           //rb.AddTorque(torqueF);
            yield return new WaitForSeconds(0.5f);
           // rb.AddTorque(-torqueF);
            
           //Debug.Log(bodyPartTransform.eulerAngles.z -90 + "body part rot" + bodyPartTransform.rotation.z);//bodyPartTransform.rotation.ToEuler() + " ");

            if (bodyPartTransform.eulerAngles.z - 90 > 45)
            {
                bodyPartTransform.Rotate(new Vector3(0,0,1));
            }
            if (bodyPartTransform.eulerAngles.z - 90 < 315)
            {
                bodyPartTransform.Rotate(new Vector3(0,0,-1));
            }
           
        }
        yield return null;
    }
    public IEnumerator IdleCoroutine(Transform trans, float amplitude, float angle)
    {
        
        Transform t = trans;
        float frequency = 0;
        float constant = t.position.x;
        float y = t.position.y;
        PlayAnimation("idle");
        while (true)
        {
            
            frequency += angle;
            float wobblingY = Mathf.Sin(frequency) * amplitude;
            float y1 = wobblingY + y;
            Vector3 destination = new Vector3(constant, y1, 0);
            rb.MovePosition(destination);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
