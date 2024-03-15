using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //how the player moves.
    public static PlayerMovement instance;
    private Rigidbody2D playerRigidBody;
    [SerializeField]
    private float moveSpeed = 15;
    private float MoveSpeed;
    private Vector2 movement;
    private Animator animator;
    private bool isWalking = true;
    //geters and setters
    public Rigidbody2D PlayerRigidBody;
    public Animator Animator { get; set; }

    // Update is called once per frame
    public void SetPlayer(Rigidbody2D playerRigidBody, Animator animator)
    {
        this.PlayerRigidBody = playerRigidBody;
        this.Animator = animator;
        MoveSpeed = moveSpeed;
        playerRigidBody.drag = 10;
        playerRigidBody.mass = 10;
    }
    public void setPlayerPos(Vector3 pos)
    {
        transform.position = pos;
    }


    public PlayerMovement()
    {

    }
    protected void playerMovementFunction()
    {
        //'if is walking' is true, then perform code.
        if (isWalking)
        {
            PlayerRigidBody.AddForce(movement * moveSpeed * 125, ForceMode2D.Force);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            Animator.SetFloat("Horizontal", movement.x);
            Animator.SetFloat("Vertical", movement.y);
            Animator.SetFloat("Speed", movement.sqrMagnitude);
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                Animator.SetFloat("Last Horizontal", Input.GetAxisRaw("Horizontal"));
                Animator.SetFloat("Last Vertical", Input.GetAxisRaw("Vertical"));
            }
        }

    }
   
    private void FixedUpdate()
    {
        playerMovementFunction();
    }
  
    private void Awake()
    {
        instance = FindObjectOfType<PlayerMovement>();
        SetPlayer(GetComponent<Rigidbody2D>(), GetComponent<Animator>());
    }
    public void setMoveSpeed(float MoveSpeed1)
    {
        this.moveSpeed = MoveSpeed1;
        Debug.Log("move speed setter" + moveSpeed);
    }
    public float getMoveSpeed()
    {
        return MoveSpeed;
    }
    //getters and setters

    public void setIsWalking(bool temp)
    {
        isWalking = temp;
    }

    public bool getIsWalking()
    {
        return isWalking;
    }

    public Vector3 getPostion()
    {
        return transform.position;
    }




}
