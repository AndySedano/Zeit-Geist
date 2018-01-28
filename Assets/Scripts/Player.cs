using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {
    public CanvasManger canvasManager;

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
    

    float timer;

    float tapAmount;

    float totalTaps; 

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
        canvasManager.HideImages(0f);
        canvasManager.Fill.fillAmount = 0;
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


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////Pllayer1 Possession's Controls//////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // to begin the process of possession
        if ( isPlayer1 && Input.GetButtonDown("Possess1") && canPossess)
        {
            npcTarget.SendMessage("inPosession",isPlayer1);
        }

        // to begin the process of possession for player1
        if (isPlayer1 && possessing && timer >0f && tapAmount <totalTaps)
        {
                // controll the timer
                timer -= Time.deltaTime;
            // only in this window of time the input of the possession button is read
                if (Input.GetButtonDown("Possess1"))
                {
                    tapAmount++;
                    canvasManager.Fill.fillAmount = tapAmount / totalTaps;
                }
            
        }else if (isPlayer1 && possessing && (timer <= 0f || tapAmount>=totalTaps))
        {// when time is over or when the tap acount is reached the process finish
            if (tapAmount >= totalTaps)
            {
                // only if the player reach the taps before the time runs out 
                //the npc is possessed
                npcTarget.SendMessage("possessionSuccesful", isPlayer1);
            }
            else
            {
                npcTarget.SendMessage("possessionFailed");
            }
            //end the possession process
            EndPossession();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////Pllayer2 Possession's Controls//////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // to begin the process of possession
        if (!isPlayer1 && Input.GetButtonDown("Possess2") && canPossess)
        {
            npcTarget.SendMessage("inPosession", isPlayer1);
        }

        // to begin the process of possession for player1
        if (!isPlayer1 && possessing && timer > 0f && tapAmount <totalTaps)
        {
            // controll the timer
            timer -= Time.deltaTime;
            // only in this window of time the input of the possession button is read
            if (Input.GetButtonDown("Possess2"))
            {
                tapAmount++;
                canvasManager.Fill.fillAmount = tapAmount / totalTaps;
            }

        }
        else if (!isPlayer1 && possessing && (timer <= 0f || tapAmount >=totalTaps))
        {// when time ran out or the player reched the tap acount the process finish
            if (tapAmount >=totalTaps)
            {
                // only if the player reach the taps before the time runs out 
                //the npc is possessed
                npcTarget.SendMessage("possessionSuccesful", isPlayer1);
            }else
            {
                npcTarget.SendMessage("possessionFailed");
            }
            //end the possession process
            EndPossession();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //hint if you can possess
        //This can be used no matter which player is
        if ((buttonHintAlpha>0 && buttonHintAlphaDirection==-1) || (buttonHintAlpha <1 && buttonHintAlphaDirection == 1))
        {
            buttonHintAlpha += Mathf.Clamp01(Time.deltaTime * (1f / 0.25f))*buttonHintAlphaDirection;
            canvasManager.HideImages(buttonHintAlpha);
        }
            
        

    }
    //called by the exterior to know how many time the player need to tap
    //& the time 
    public void StartPossession(int taps, float time)
    {
        canMove = false;
        canPossess = false;
        possessing = true;
        canvasManager.Fill.fillAmount = 0f;
        timer = time;
        tapAmount = 0;
        totalTaps = taps;
    }

    void EndPossession()
    {
        canMove = true;
        canPossess = false;
        possessing = false;
        canvasManager.Fill.fillAmount = 0f;
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
            //print("Enter");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC") && canPossess)
        {
            //npcTarget = null;

            // hide the possession hint
            buttonHintAlphaDirection = -1;
            canPossess = false;
            //print("out");
        }
    }

    void Poosses()
    {
        //npcTarget.SendMessage("Possess", isPlayer1);
    }


}
