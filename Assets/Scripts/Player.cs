using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {
    // to call the npc and let them know which player is possesing them
    public bool isPlayer1;

    //Movement Variables
    public float movespeed = 5f;// velocity of the player

    Vector3 moveVelocity; // direction in wich the player moves

    PlayerController controller;// movement controller 

    bool canMove;// it enables the ability of the player to move

    //state Variables
    bool possessing;// if the player is possesing a npc

    bool canPossess;

    //Target to start possession
    GameObject npcTarget;

    //Minigame elements
    public Image shadow;
    public Image buttonHint;

    public float timer;

    public float tapAmount;

    float buttonHintAlpha;
    int buttonHintAlphaDirection;

    void Start()
    {
        //get the controller componet
        controller = GetComponent<PlayerController>();

        //initil state variables
        canMove = true; // at the bigining of the game the player can move around

        canPossess = false;// is near to a npc, but is not possesing it

        

        possessing = false;// the player is not possesing anything
        //Disable the images because the these only display when the player is posesing something
        //disable it by using the alpha

        buttonHintAlpha = 0f;//because it begins hidden
        buttonHint.color = new Color(buttonHint.color.r, buttonHint.color.g, buttonHint.color.b, buttonHintAlpha);
        shadow.fillAmount = 0;
    }


    void Update()
    {
        //first see if the player can move
        if (canMove) {
            //now check which player this code is controlling
            //same code different input
            if (isPlayer1)
            {
                moveVelocity = new Vector3(Input.GetAxisRaw("HorizontalPlayer1"), 0f, Input.GetAxisRaw("VerticalPlayer1")).normalized;
            }
            else
            {
                moveVelocity = new Vector3(Input.GetAxisRaw("HorizontalPlayer2"), 0f, Input.GetAxisRaw("VerticalPlayer2")).normalized;
            }

            //move the player using the methods at the controller
            controller.Move(moveVelocity * movespeed);

        }
        else
        {
            controller.Move(Vector3.zero);
        }

        // to begin the process of possession
        if ( isPlayer1 && Input.GetButtonDown("Possess1") && canPossess)
        {
            StartPossession();
        }

        // to begin the process of possession for player1
        if (isPlayer1 && possessing && timer >0f && tapAmount >0)
        {
                timer -= Time.deltaTime;
                if (Input.GetButtonDown("Possess1"))
                {
                    tapAmount--;
                    shadow.fillAmount = tapAmount / 10;
                }
            
        }else if (isPlayer1 && possessing && (timer <= 0f || tapAmount<=0))
        {
            if (tapAmount<=0)
            {
                //npc is possess
            }
            EndPossession();
        }

        //hit if you can possess
        //This can be used no matter which player is
        if ((buttonHintAlpha>0 && buttonHintAlphaDirection==-1) || (buttonHintAlpha <1 && buttonHintAlphaDirection == 1))
        {
            buttonHintAlpha += Mathf.Clamp01(Time.deltaTime * (1f / 0.25f))*buttonHintAlphaDirection;
            buttonHint.color = new Color(buttonHint.color.r, buttonHint.color.g, buttonHint.color.b, buttonHintAlpha);
        }
            
        

    }

    void StartPossession()
    {
        canMove = false;
        canPossess = false;
        possessing = true;
        shadow.fillAmount = 1f;
        timer = 1.5f;
        tapAmount = 10f;
    }

    void EndPossession()
    {
        canMove = true;
        canPossess = false;
        possessing = false;
        shadow.fillAmount = 0f;
        buttonHintAlphaDirection = -1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && !canPossess)
        {
            //Hint htat you can posses the player
            buttonHintAlphaDirection = 1;

            canPossess = true;
            npcTarget = other.gameObject;
            print("Lol");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC") && canPossess)
        {
            npcTarget = null;

            // hide the possession hint
            buttonHintAlphaDirection = -1;
            canPossess = false;
        }
    }

    void Poosses()
    {
        //npcTarget.SendMessage("Possess", isPlayer1);
    }


}
