using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : Gameplay_Object, IGameSetting
{

    public int attackDamage;
    public int maxhp; 
    public float speed;
    public Transform target;
    protected PolygonCollider2D pCol;
    protected Animator anim;
    protected Animator animChild;
    protected SpriteRenderer enemySpriteR;
    protected SpriteRenderer childSpriteR;
    protected string enemyPath = "Prefab/Enemy/";
    

    public void SetUpEnemy(int attackDamage, int hp, float speed, Transform target)
    {
        childSpriteR = GetComponentInChildren<SpriteRenderer>();
        enemySpriteR = GetComponent<SpriteRenderer>();
        animChild = GetComponentInChildren<Animator>();
        anim = GetComponent<Animator>();
        pCol = GetComponent<PolygonCollider2D>();
        if (pCol == null)
        {
            pCol = gameObject.AddComponent<PolygonCollider2D>();
        }
        this.target = target;
        this.attackDamage = attackDamage;
        SetUp(hp);
        maxhp = hp;
        this.speed = speed;
    }
    protected void ChangeLayering(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    public virtual void Attack()
    {
       
    }
    public virtual void Move()
    {

    }
    protected void PlayAnimation(string s)
    {
        anim.Play(s);
    }
    protected void PlayChildAnimation(string s)
    {
        if (animChild != null)
        {
            animChild.Play(s);
        }
    }
    protected void DisappearingSprite()
    {
        StartCoroutine(DissapearCoroutine(enemySpriteR));
    }
    protected void DisappearingSpriteChild()
    {
        StartCoroutine(DissapearCoroutine(childSpriteR));
    }
    public IEnumerator DissapearCoroutine(SpriteRenderer spriteR)
    {
        float alpha = 1;
        yield return new WaitForSeconds(0.5f);
        while (alpha > 0)
        {
            alpha -= 0.05f;
            spriteR.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(0.06f);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerManager.instance.setPlayerHealth(-(attackDamage));
            
        }
    }
    public Transform getplayerTransform()
    {
        Transform playerTransform = FindObjectOfType<PlayerMovement>().transform;
        return playerTransform;
    }

    public virtual void AlcholismCard()
    {
        speed /= 2;

    }



    /*

        public void SpawnChildGameObject(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            GameObject temp = Instantiate(prefab,transform);
            float x = transform.position.x;
            float y = transform.position.y;
            float scalex = transform.localScale.x;
            float scaley = transform.localScale.y;
            temp.transform.position = new Vector3(x, y + (scaley / 4));

        }
    */
}