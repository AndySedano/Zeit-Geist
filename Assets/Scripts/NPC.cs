using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	private int estado;	//0-Normal, 1-En Posesion, 2-PoseidoP1, 3-PoseidoP2, 4-faint
    int prevState;
	private int trait; //0-No Trait, 1- NonBeliver, 2-frightened, 
    private int nonbeliverTries;
    Collider coll;

    // Use this for initialization
    void Start () {

		estado = 0;// current state

        prevState = 0;//memory to get back to the initial state if the possession fails

		trait = Random.Range (0,3);//Traits are generated ramdomly

        nonbeliverTries=(trait==1)?3:0;//Tries are equal to 3 only if the npc has nonbeliver as a trait

		transform.Rotate (Vector3.up, Random.Range (0f ,360f));//Npc starts with a random direction 

        //coll= GetComponent<>
	}
	
	// Update is called once per frame
	void Update () {
		if(estado != 1 && estado!=4){
			transform.Translate (Vector3.forward * 0.05f);
		}

//		else if (estado == 2 || estado == 3) {
//			transform.Translate (Vector3.forward * 0.5f);
//		}
	}

    //Para detectar si choco con algo que no sea player
    void OnCollisionEnter(Collision hit)
    {
        //print(hit.transform.tag);
		transform.Rotate (Vector3.up, transform.rotation.y + 180);
	}

	//Mensaje que manda el player cuando entra a su trigger
	public void inPosession(bool isPlayer1){

        //estado = 1;//En Posesion
        //the case cero is the normal amount of taps needed to 
        //possess
        int taps2Possess=5;
        float time2Possess = 1.25f;

		if((isPlayer1 && estado==3)||(!isPlayer1 && estado == 2))
        {
            taps2Possess = (int)(taps2Possess * 1.25f);
            time2Possess = time2Possess * 0.75f;
        }

        prevState = estado;// save the prev state in case the player fails the minigame
        estado = 1;//curren state is being possess

        switch (trait)
        {
            case (1):   //nonBeliver
                    time2Possess = time2Possess * 0.75f;
                break;
            case (2)://frightened
                if ((!isPlayer1 && estado==2) || (isPlayer1 && estado == 3))
                {
                    estado = 4;
                }
                break;
        }
        //Because a ghost can scared a npc only if the state remains as one
        //the method is called
        if (estado==1)
        {
            if (isPlayer1)
            {
                GameManager.instance.player1.StartPossession(taps2Possess,time2Possess);
            }else
            {
                GameManager.instance.player2.StartPossession(taps2Possess, time2Possess);
            }
        }
	}

	public void possessionSuccesful(bool isPlayer1){
        if (nonbeliverTries == 0 && isPlayer1)
        {
            GameManager.instance.player1Score += 1;
            gameObject.layer = 13;//layer 13 is the layer of the player 1
            estado = 2;
        }
        else if(nonbeliverTries == 0 && !isPlayer1)
        {
            estado = 3;
            GameManager.instance.player2Score += 1;
            gameObject.layer = 12;//layer 12 is the layer of the player 2
        }
        else
        {
            estado = prevState;
            nonbeliverTries--;
        }

       
	}

	public void possessionFailed(){
        estado = prevState;
	}
}
