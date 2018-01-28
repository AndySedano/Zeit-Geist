using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int player1Score = 1;
	public int player2Score = 0;
	public float timeLeft;
	public Player player1;
	public Player player2;
	public CinemachineVirtualCamera menuCam;
	public CinemachineVirtualCamera gameCam;
	public CinemachineVirtualCamera victoryCam;
	public Canvas menu;
	public CanvasGroup pausaGroup;
	public CanvasGroup menuGroup;
	public Text timer;

	private bool inGame = false;
	private bool gameFinished = false;


	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (inGame) {
			timeLeft -= Time.deltaTime;
			string minSec = (int)timeLeft + "  segundos";
			timer.text = minSec;
			if (timeLeft < 0) {
				endGame ();
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				inGame = false;
				Time.timeScale = 0;

				//Activa el grupo de pausa
				pausaGroup.alpha = 1;
				pausaGroup.interactable = true;
				pausaGroup.blocksRaycasts = true;
				//Desactiva el grupo del menu
				menuGroup.alpha = 0;
				menuGroup.interactable = false;
				menuGroup.blocksRaycasts = false;
				//activa el canvas
				menu.enabled = true;
			}

		}
	}


	//Cargar los NPC's
	void loadNPCS() {

	}

	//Poner los players en su posicion incial
	void loadPlayers() {
		player1.transform.position = new Vector3 (-16,0.5f,-41);
		player2.transform.position = new Vector3 (8,0.5f,-41);
	}


	//--------------- Menu Inicial -----------------//

	//Empezar el juego
	public void beginGame(){
		menu.enabled = false;
		menuCam.enabled = false;
		timer.enabled = true;
		timeLeft = 50;
		loadNPCS ();
		loadPlayers ();
		StartCoroutine (beginTimer ());
	}

	//Para esperar los dos segundos en lo que se acomoda la camara
	public IEnumerator beginTimer(){
		yield return new WaitForSecondsRealtime (2);
		inGame = true;
	}

	//Salir del Juego
	public void exitGame() {
		Application.Quit();
	}

	//Aqui falta
	public void instrucciones() {

	}



	//--------------- Menu Pausa -----------------//


	//Continua el juego del menu de pausa
	public void continueGame() {
		menu.enabled = false;
		pausaGroup.alpha = 0;
		pausaGroup.interactable = false;
		Time.timeScale = 1;
		menu.enabled = false;
		inGame = true;
	}

	//Recarga el juego
	public void restartGame() {
		menuCam.enabled = true;
		gameFinished = false;
		Time.timeScale = 1;
		menu.enabled = false;
		StartCoroutine (restartTimer ());
		timer.text = "Tiempo Restante";
	}

	public IEnumerator restartTimer(){
		yield return new WaitForSecondsRealtime (2);
		beginGame ();
	}

	public void returnToMenu() {
		menuCam.enabled = true;
		pausaGroup.alpha = 0;
		pausaGroup.interactable = false;
		menu.enabled = true;
		inGame = false;
		gameFinished = false;
		timer.enabled = false;
	}









	//Se muere todo
	public void endGame(){
		inGame = false;
		gameFinished = true;
		gameCam.enabled = false;
		timer.text = "Victoria!";

		if (player1Score > player2Score) {
			victoryCam.LookAt = player1.transform;
		} else {
			victoryCam.LookAt = player2.transform;
		}

		StartCoroutine (resetGame ());
	}

	//Para despues de ganar, reiniciar el juego
	public IEnumerator resetGame(){
		yield return new WaitForSecondsRealtime (5);
		returnToMenu ();
	}
		





}
