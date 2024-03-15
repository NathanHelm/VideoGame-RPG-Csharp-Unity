using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //all of the player's data
    public static PlayerManager instance;
    Rigidbody2D playerRigidbody2d;
    Animator playerAnimator;
    public int health = 10;

    public void Health()
    {

    }
    public void setPlayerManager()
    {
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        health = 3;
    }
    public void setPlayerHealth(int dmg)
    {
        Debug.Log("player health goes up/down by: " + dmg);
        health += dmg;
        return;
    }
    private void OnEnable()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        }
        setPlayerManager();
    }
}
