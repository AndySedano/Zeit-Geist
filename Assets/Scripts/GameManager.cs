using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int player1Score = 0;
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
	public Spawner spawner;
	public Button btnIniciar;
	public Button btnContinuar;
	public bool inGame = false;

	private bool gameFinished = false;
	private AudioSource[] audios;


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
		audios = GetComponents<AudioSource> ();
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
				//Activa el boton principal
				menu.GetComponentInChildren<EventSystem> ().SetSelectedGameObject (btnContinuar.gameObject);
				//activa el canvas
				menu.enabled = true;
			}
		}
	}


	//Cargar los NPC's
	void loadNPCS() {
		spawner.SpawnNpc ();
	}

	//Poner los players en su posicion incial
	void loadPlayers() {
		player1.transform.position = new Vector3 (-16,0.5f,-41);
		player2.transform.position = new Vector3 (8,0.5f,-41);
	}


	//--------------- Menu Inicial -----------------//

	//Empezar el juego
	public void beginGame(){
		//Desactiva el menu y la camara del menu
		menu.enabled = false;
		menuCam.enabled = false;
		gameCam.enabled = true;

		//Activa el timer y pone el tiempo
		timer.text = "Tiempo Restante";
		timer.enabled = true;
		timeLeft = 10;

		//Carga los NPCS
		loadNPCS ();
		loadPlayers ();

		StartCoroutine (fadeOut (audios[0], 2f));

		//Espera dos segundos en lo que la camara se mueve y empieza
		StartCoroutine (beginTimer ());
	}

	//Para esperar los dos segundos en lo que se acomoda la camara
	public IEnumerator beginTimer(){
		yield return new WaitForSecondsRealtime (2);
		loadPlayers ();
		inGame = true;
		audios [1].Play ();
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
		//Desactiva el canvas
		menu.enabled = false;

		//Desactiva el menu pausa
		pausaGroup.alpha = 0;
		pausaGroup.interactable = false;
		pausaGroup.blocksRaycasts = false;

		Time.timeScale = 1;
		inGame = true;
	}

	//Recarga el juego
	public void restartGame() {
		menuCam.enabled = true;
		gameFinished = false;
		menu.enabled = false;
		timer.text = "Tiempo Restante";
		StartCoroutine (restartTimer ());

	}

	public IEnumerator restartTimer(){
		yield return new WaitForSecondsRealtime (2);
		beginGame ();
	}

	public void returnToMenu() {
		//Activa la camara del menu
		menuCam.enabled = true;

		//Desactiva el grupo de pausa
		pausaGroup.alpha = 0;
		pausaGroup.interactable = false;
		pausaGroup.blocksRaycasts = false;
		menu.GetComponentInChildren<EventSystem> ().SetSelectedGameObject (btnIniciar.gameObject);

		//Activa el grupo del menu
		menuGroup.alpha = 1;
		menuGroup.interactable = enabled;
		menuGroup.blocksRaycasts = enabled;

		//Activa el menu
		menu.enabled = true;

		inGame = false;
		gameFinished = false;
		timer.enabled = false;
		Time.timeScale = 1;
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
		timer.enabled = false;
		loadPlayers ();
		returnToMenu ();
	}




	public static IEnumerator fadeOut (AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

			yield return null;
		}

		audioSource.Stop ();
		audioSource.volume = startVolume;
	}


		





}
