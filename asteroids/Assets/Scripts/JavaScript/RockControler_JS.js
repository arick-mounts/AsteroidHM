#pragma strict

var comment : String = "(Usage comment)";

var driftSpeed : float = 10f;
var rollSpeed : float = 10f;

var showLogMessages : boolean = false;

private var acceleration : Vector3;

// Use this for initialization
function Start () {

	// Set random dirction at drift speed
	acceleration = Random.onUnitSphere;
	acceleration.y = 0;
	acceleration = acceleration.normalized * driftSpeed;
		
	// Set random roll direction at roll speed
	if(Random.value > .5) rollSpeed *= -1f;
}

// Update is called once per frame
function Update () {
	transform.position += acceleration * Time.deltaTime;	
	transform.Rotate(0,Time.deltaTime * rollSpeed, 0);
}