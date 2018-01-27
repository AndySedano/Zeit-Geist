using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	private int estado;	//0-Normal, 1-En Posesion, 2-PoseidoP1, 3-PoseidoP2
	private int trait;

	// Use this for initialization
	void Start () {
		estado = 0;
		trait = Random.Range (0,1);
		transform.Rotate (Vector3.up, Random.Range (0f ,360f));
	}
	
	// Update is called once per frame
	void Update () {
		if(estado == 0){
			transform.Translate (Vector3.forward * 0.3f);
		}

//		else if (estado == 2 || estado == 3) {
//			transform.Translate (Vector3.forward * 0.5f);
//		}
	}

	//Para detectar si choco con algo que no sea player
	private void OnCollisionEnter(Collision hit){
		transform.Rotate (Vector3.up, transform.rotation.y + 180);
	}

	//Mensaje que manda el player cuando entra a su trigger
	private void inPosession(bool isPlayer1){
		
		estado = 1;//En Posesion

		switch (estado) {

		case(0):	//Normal
			if (isPlayer1) {
				//Minigame facil al player1
			} else {
				//Minigame facil al player2
			}
			break;

		case(2):	//Poseido por player 1
			if (isPlayer1) {
				//No hacer nada
				estado = 2;
			} else {
				//Minigame dificil al player2
			}
			break;

		case(3):	//Poseido por player 2
			if (isPlayer1) {
				//Minigame dificil al player 1
			} else {
				//N
				estado = 2;
			}
			break;

		default:
			//Crashear
			break;
		}
	}

	private void possessionSuccesful(bool isPlayer1){
		if (isPlayer1) {
			GameManager.instance.player1Score += 1;
		} else {
			GameManager.instance.player1Score += 2;
		}
	}

	private void possessionFailed(bool isPlayer1){
		//Checar traits
	}
}
