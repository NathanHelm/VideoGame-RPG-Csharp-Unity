using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : Item
{
    //blockers are used to barriers in the game.
    protected Rigidbody2D rigidbody2D;
    public Transform coverTransform; //to direct all pathfinding AI
    public float offset;
    ParticleSystem par;
    SpriteRenderer spriteRenderer;
    public float maxVelocityX = 4;
    public float maxVelocityY = 2;
    RubblePile rubble;
    bool runOnce = false;
    public void SetUp(float maxvx, float maxvy, int health)
    {
           
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("Blocker");
        maxVelocityX = maxvx;
        maxVelocityY = maxvy;
        this.health = health;
        if(spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        if (rigidbody2D == null)
        {
            rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        }
        rigidbody2D.mass = 30;
        rigidbody2D.gravityScale = 0;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy_Pathfinding"))
        {
            Debug.Log(gameObject.name + " name, health ->" + health);
            HurtBlocker();
        }
    }
    public void DestroyBlocker()
    {
        StartCoroutine(DestroyBlockerCoroutine());
    }
    private IEnumerator DestroyBlockerCoroutine()
    {
        DontDisplayBlockerImage();
        ParticleSystem particleSystem = particleDestroy();
        rubble = gameObject.AddComponent<RubblePile>();
        rubble.SpawnRubble(spriteRenderer.color, transform.position);
        yield return new WaitForSeconds(particleSystem.main.duration);
        gameObject.SetActive(false);
    }
    private void DontDisplayBlockerImage()
    {
        spriteRenderer.sprite = null;
    }

    private void HurtBlocker()
    {
        float velocityX = Mathf.Abs(rigidbody2D.velocity.x);
        float velocityY = Mathf.Abs(rigidbody2D.velocity.y);
        Debug.Log("VECLOVITY -> " + velocityX + " " + velocityY);
        float offsetVelocity = 1;
        if (velocityX > maxVelocityX && velocityY > maxVelocityY)
        {
            Debug.Log("taken damage");
            int velocitynum = (int)((velocityX + velocityY) * offsetVelocity);
            health -= velocitynum;
        }
        if (health <= 0 && !runOnce)
        {
            DestroyBlocker();
            runOnce = true;
        }
    }
    private ParticleSystem particleDestroy()
    {
       
        GameObject g = Resources.Load<GameObject>("Prefab/Particle_Systems/particle_system_Death");
        par = Instantiate(g, transform).GetComponent<ParticleSystem>();
        par.Play();
        return par;
    }

    public Transform findclosestVector(Vector3 ObjPos)
    {
        offset = 0.5f;
        Vector3[] blockerPositions = new Vector3[4];
        Vector3 scale = GetComponent<Transform>().localScale;
        coverTransform = GetComponent<Transform>();
        blockerPositions[0] = new Vector3(coverTransform.position.x + (scale.x * offset), coverTransform.position.y, 0);
        blockerPositions[1] = new Vector3(coverTransform.position.x - (scale.x * offset), coverTransform.position.y, 0);
        blockerPositions[2] = new Vector3(coverTransform.position.x, coverTransform.position.y + (scale.y * offset), 0);
        blockerPositions[3] = new Vector3(coverTransform.position.x, coverTransform.position.y - (scale.y * offset), 0);
        Vector3 farthest = new Vector3(0,0,0);
        float distance = 0; 
        for (int i = 0; i < blockerPositions.Length; i++)
        {
           float dist = (int)Vector3.Distance(ObjPos, blockerPositions[i]);
           if(dist > distance)
           {
                distance = dist;
                farthest = blockerPositions[i];
           }
        }
        coverTransform.position = farthest;
        Debug.Log("vector 3 - > " + coverTransform.position);
       
        return coverTransform;
    }

}
