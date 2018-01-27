using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	private int estado;	//0-Normal, 1-Poseido
	private int trait;

	// Use this for initialization
	void Start () {
		estado = 0;
		trait = Random.Range (0,1);
		transform.Rotate (Vector3.up, Random.Range (0f ,360f));
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * 0.3f);
	}

	//Para chocar con otros NPCS
	private void OnTriggerEnter(Collider hit){
		if(hit.tag.Equals("NPC")){
			//cambiar direccion
		} else if(hit.tag.Equals("Player")) {
			//Hacer cosas de player
		}
	}

	//Para detectar si choco con el player
	private void OnCollisionEnter(Collision hit){

	}
}
