using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int player1Score = 0;
	public int player2Score = 0;
	public float timeLeft;
	public GameObject player1;
	public GameObject player2;

	private bool inGame = false;

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
		if(inGame){
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0){
				endGame ();
			}
		}
	}

	//Empezar el juego
	void beginGame(){
		SceneManager.LoadScene ("Game");
		timeLeft = 30;
		loadNPCS ();
	}

	//Cargar el PreJuego
	void loadPreGame(){
		SceneManager.LoadScene ("PreGame");
	}
		
	//Se muere todo
	void endGame(){
		
	}

	void loadNPCS(){
		
	}
}
