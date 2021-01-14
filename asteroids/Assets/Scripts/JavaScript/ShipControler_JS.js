#pragma strict

var comment : String = "(Usage comment)";

var movementMaxSpeed : float = 1;
var movementAcceleration : float = .2;

var turnMaxSpeed : float = 1;
var turnAcceleration : float = .5;

var spawnPoint : Transform;

var showLogMessages : boolean = false;

private var currenMovementVelocity : Vector3;
private var currentTurnVelocity : float = 0f;


// Use this for initialization
function Start () {

}

function OnEnable() {
	transform.position = spawnPoint.position;
	transform.rotation = spawnPoint.rotation;

	currenMovementVelocity = Vector3.zero;
	currentTurnVelocity = 0f;
}

// Update is called once per frame
function Update () {
	
	// calc acceleration
	var vertInput : float = Input.GetAxis("Vertical");
	if(vertInput > 0f) {
		currenMovementVelocity += transform.forward * vertInput * Time.deltaTime * movementAcceleration;
	}

	var hozInput : float = Input.GetAxis("Horizontal") * Time.deltaTime * turnAcceleration;
	if(Mathf.Abs(hozInput) > 0.01f ) {
		currentTurnVelocity += hozInput;
	}
	else {
		currentTurnVelocity = 0f;
	}
	
	// cap acceleration
	if(currenMovementVelocity.magnitude > movementMaxSpeed) {
		currenMovementVelocity = currenMovementVelocity.normalized * movementMaxSpeed;
	}
	
	currentTurnVelocity = Mathf.Min(currentTurnVelocity, turnMaxSpeed);
	currentTurnVelocity = Mathf.Max(currentTurnVelocity, -turnMaxSpeed);
	
	// move
	transform.position += currenMovementVelocity * Time.deltaTime;
	transform.Rotate(0, Time.deltaTime * currentTurnVelocity, 0);

}