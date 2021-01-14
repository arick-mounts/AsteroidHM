using UnityEngine;
using System.Collections;

public class GamePlayLogic : MonoBehaviour {
	
	public GameObject shipObject;
	public GameObject  shipSafeTriggerObject;
	
	public GUIText livesText = null;
	public GUIText scoreText = null;
	public GUIText highScoreText = null;
	public GUIText gameOverText = null;
	public GUIText pressStartText = null;
	
	public int gameStage;
	
	public bool showLogMessages = false;
	
	private int score;
	private int lives;
	private int highScore;
	private int level;
	
	private float waitTime;
	
	// Use this for initialization
	void Start () {				
		Initialize();
		shipSafeTriggerObject.SetActive(false);
	}
	
	void Initialize() {
		gameStage = 1;
		
		score = 0;
		lives = 0;
		highScore = 0;
		level = 0;
		
		// create asteroids on start screen
		SpawnAsteroids(level+3,.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
		// setup wait screen
		if(gameStage==1) { 
			if(showLogMessages) Debug.Log("Entering Setup Wait State 0");
			// show GUI
			gameOverText.gameObject.SetActive(true);
			pressStartText.gameObject.SetActive(true);
			highScoreText.gameObject.SetActive(true);
			
			// disable ship
			shipObject.SetActive(false);
			
			// next state
			gameStage = 2;
		}
		
		// wait for game to start
		if(gameStage==2) { 
			if(showLogMessages) Debug.Log("Entering WaitStart State 1");
			if (Input.GetKeyDown(KeyCode.Space)) {
				if(showLogMessages) Debug.Log("Start Key Was Pressed.");
				gameStage = 3;
			}
		}
		
		// setup game 
		if(gameStage==3) { 
			if(showLogMessages) Debug.Log("Entering PlayGame State 0");
			SetupGame();
			// next state
			gameStage = 4;
		}
		
		// play game loop
		if(gameStage==4) { 
			// nothing happens here becuase all the work is 
			//  done below where the messages are received
		}
		
		// show game over
		if(gameStage==5) { 
			if(showLogMessages) Debug.Log("Entering PlayGame State 2");
			
			// show GUI
			gameOverText.gameObject.SetActive(true);
			highScoreText.gameObject.SetActive(true);
			
			// start timer to game in a few seconds
			EndGame();
			
			// next state
			gameStage=6;
		}
		
		// wait while game is ending
		if(gameStage==6) { 
			// do nothing while giving player time to realize game is over
		}
	}
	
	void SetupGame() {
		// reset lives and score
		SetLives(3);
		SetScore(0);
		level = 0;
		
		// hide GUI
		gameOverText.gameObject.SetActive(false);
		pressStartText.gameObject.SetActive(false);
		highScoreText.gameObject.SetActive(false);
		
		// deystroy any existing asteroids
		BroadcastMessage("Message", "DestroyAllAsteroids", SendMessageOptions.RequireReceiver);
		
		// create starting asteroids
		SpawnAsteroids(level+3,.5f);
		// spawn ship in 3 seconds or when safe
		ResetShip(3);
	}
	
	IEnumerator EndGame() {
		yield return new WaitForSeconds(3);
		
		// set game stage to wait for start button
		gameStage=1;
	}
	
	void ShipHit() {
		SetLives(lives-1);
		
		if(lives == 0) {
			if(gameStage==4){
				gameStage=5;
			}
		}
	}
	
	void AsteroidHit() {
		
		int asteroids = CountObjectsWithTag("Asteroid");
		asteroids -= 1; // subtract one becuase the atsteroid just hit still exists during this frame
		
		if(asteroids==0){
			// only if game is running
			if(gameStage==4){
				level += 1;
				SpawnAsteroids(level+3,3);
			}
			else {
				Debug.LogWarning("Asteroid Hit during NONE gameplay.\n");
			}
		}
	}
	
	
	int CountObjectsWithTag(string tagName) {
		GameObject[] objectsWithTag  = GameObject.FindGameObjectsWithTag(tagName);
		//		Debug.LogWarning("Objects with tag '" + tagName + "' = " + objectsWithTag.Length + "\n");
		return objectsWithTag.Length;
	}
	
	void SetLives(int value) {
		lives = value;
		if(lives < 0 ) lives = 0;
		livesText.gameObject.SetActive(true);
		livesText.text = "Lives: " + lives;
	}
	
	void SetScore(int value) {
		score = value;
		scoreText.gameObject.SetActive(true);
		scoreText.text = "Score: " + score;
		if(score > highScore) {
			SetHighScore(score);
		}
	}
	
	void SetHighScore(int value) {
		if(value > highScore) highScore = value;
		highScoreText.gameObject.SetActive(true);
		highScoreText.text = "High Score: " + highScore;
	}
	
	IEnumerator ResetShip(float delay) {
		if(showLogMessages) Debug.Log("Entering ResetShip Method");
		
		if(shipObject) {
			if(showLogMessages) Debug.Log("Starting Reset Ship...");
			shipObject.SetActive(false);
			
			// only if game is running or is starting up
			if(gameStage==3 || gameStage==4) {
				shipSafeTriggerObject.SetActive(false);
				yield return new WaitForSeconds(delay);
				shipSafeTriggerObject.SetActive(true);
				//	shipObject.SetActive(true);
			}
		}
		else{
			Debug.LogError("Aborting Reset Ship");
		}
	}
	
	IEnumerator SpawnAsteroids(int count, float delay) {
		if(showLogMessages) Debug.Log("Entering Spawn Asteroids Method");
		
		yield return new WaitForSeconds(delay);
		
		for(int i=0; i<count; i++) {
			SendMessage("Message", "CreateLargeRock", SendMessageOptions.RequireReceiver);
		}
	}
	
	void Message(string Name) {
		if(showLogMessages) Debug.Log("GamePlayLogic Got Message: Name='" + Name + "' on object '" + name + "'\n");
		
		if(Name == "ShipHit") {
			if(showLogMessages) Debug.Log( "Recignized 'ShipHit' message.\n");
			ShipHit();
			ResetShip(3);
		}
		
		if(Name == "ShipSpawn") {
			if(showLogMessages) Debug.Log( "Recignized 'ShipSpawn' message.\n");
			
			if(shipObject.activeInHierarchy == false && gameStage==4) {
				shipObject.SetActive(true);
			}
			else {
				if(showLogMessages) Debug.LogWarning( "Ignoring 'ShipSpawn' message, ship is already active or game is over.\n");
			}
			
			shipSafeTriggerObject.SetActive(false);
		}
		
		
		if(Name == "HitLargeRock") {
			if(showLogMessages) Debug.Log( "Recignized 'HitLargeRock' message.\n");
			SetScore(score+10);
			AsteroidHit();
		}
		if(Name == "HitMediumlRock") {
			if(showLogMessages) Debug.Log( "Recignized 'HitMediumlRock' message.\n");
			SetScore(score+20);
			AsteroidHit();
		}
		if(Name == "HitSmallRock") {
			if(showLogMessages) Debug.Log( "Recignized 'HitSmallRock' message.\n");
			SetScore(score+30);
			AsteroidHit();
		}
		
	}
}
