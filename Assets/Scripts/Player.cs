using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {

    public bool isPlayer1;

    //speed variable
    public float movespeed = 5f;

    //
    Vector3 moveVelocity;

    PlayerController controller;

    //movement variables
    float player1x;
    float player1y;
    float player2x;
    float player2y;

    void Start()
    {
        controller = GetComponent<PlayerController>();
    }


    void Update()
    {

        if (isPlayer1)
        {
            moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
        else
        {
            moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }

        //move the player
        
        controller.Move(moveVelocity * movespeed);
        

        
        
    }
}
