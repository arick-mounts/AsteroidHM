#pragma strict

var shipObject : GameObject;
var shipSafeTriggerObject : GameObject;

var livesText : GUIText;
var scoreText : GUIText;
var highScoreText : GUIText;
var gameOverText : GUIText;
var pressStartText : GUIText;

var gameStage : int;

var showLogMessages : boolean = false;

private var score : int;
private var lives : int;
private var highScore : int;
private var level : int;

private var waitTime : float;

// Use this for initialization
function Start () {				
	Initialize();
	shipSafeTriggerObject.SetActive(false);
}

function Initialize() {
	gameStage = 1;
	
	score = 0;
	lives = 0;
	highScore = 0;
	level = 0;
	
	// create asteroids on start screen
	SpawnAsteroids(level+3,.5f);
}

// Update is called once per frame
function Update () {

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

function SetupGame() {
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

function EndGame() {
	yield WaitForSeconds(3);

	// set game stage to wait for start button
	gameStage=1;
}

function ShipHit() {
	SetLives(lives-1);

	if(lives == 0) {
		if(gameStage==4){
			gameStage=5;
		}
	}
}

function AsteroidHit() {

	var asteroids : int = CountObjectsWithTag("Asteroid");
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


function CountObjectsWithTag(tagName : String) : int {
	var objectsWithTag : GameObject[]  = GameObject.FindGameObjectsWithTag(tagName);
//		Debug.LogWarning("Objects with tag '" + tagName + "' = " + objectsWithTag.Length + "\n");
	return objectsWithTag.Length;
}

function SetLives(value : int) {
	lives = value;
	if(lives < 0 ) lives = 0;
	livesText.gameObject.SetActive(true);
	livesText.text = "Lives: " + lives;
}

function SetScore(value : int) {
	score = value;
	scoreText.gameObject.SetActive(true);
	scoreText.text = "Score: " + score;
	if(score > highScore) {
		SetHighScore(score);
	}
}

function SetHighScore(value : int) {
	if(value > highScore) highScore = value;
	highScoreText.gameObject.SetActive(true);
	highScoreText.text = "High Score: " + highScore;
}

function ResetShip(delay : float) {
	if(showLogMessages) Debug.Log("Entering ResetShip Method");

	if(shipObject) {
		if(showLogMessages) Debug.Log("Starting Reset Ship...");
		shipObject.SetActive(false);

		// only if game is running or is starting up
		if(gameStage==3 || gameStage==4) {
			shipSafeTriggerObject.SetActive(false);
			yield WaitForSeconds(delay);
			shipSafeTriggerObject.SetActive(true);
		//	shipObject.SetActive(true);
		}
	}
	else{
		Debug.LogError("Aborting Reset Ship");
	}
}

function SpawnAsteroids(count : int, delay : float) {
	if(showLogMessages) Debug.Log("Entering Spawn Asteroids Method");

	yield WaitForSeconds(delay);

	for(var i : int=0; i<count; i++) {
		SendMessage("Message", "CreateLargeRock", SendMessageOptions.RequireReceiver);
	}
}

function Message(Name : String) {
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