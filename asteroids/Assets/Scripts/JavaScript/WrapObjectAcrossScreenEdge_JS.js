#pragma strict

var comment : String = "(Usage comment)";

var showLogMessages : boolean = false;

private var gameCamera : Camera;

private var rectCamLocal : Rect;
private var camRecBottomLeft : Vector3;
private var camRecTopRight : Vector3;
	
function Start () {
	gameCamera = Camera.main;
}


// When object move off edge of screen set to enter opposite side of sreen
// Do this on the LateUpdate to make sure other objects positions have been calculated

function LateUpdate () {
	
	var pos : Vector3 = transform.position;
	
	//
	rectCamLocal = gameCamera.pixelRect;
	camRecBottomLeft = gameCamera.ScreenToWorldPoint(Vector3(rectCamLocal.x,rectCamLocal.y, gameCamera.nearClipPlane));
	camRecTopRight = gameCamera.ScreenToWorldPoint(Vector3(rectCamLocal.x+rectCamLocal.width,rectCamLocal.y+rectCamLocal.height, gameCamera.nearClipPlane));
			
	// Test right wall
	if(transform.position.x > camRecTopRight.x) {
		pos.x = camRecBottomLeft.x + 0.1f;
		if(showLogMessages) Debug.Log("Outside Camera Right");
	}

	// Test left wall
	if(transform.position.x < camRecBottomLeft.x) {
		pos.x = camRecTopRight.x - 0.1f;
		if(showLogMessages) Debug.Log("Outside Camera Left");
	}

	// Test top wall
	if(transform.position.z > camRecTopRight.z) {
		pos.z = camRecBottomLeft.z + 0.1f;
		if(showLogMessages) Debug.Log("Outside Camera Top");
	}

	// Test bottom wall
	if(transform.position.z < camRecBottomLeft.z) {
		pos.z = camRecTopRight.z - 0.1f;
		if(showLogMessages) Debug.Log("Outside Camera Bottom");
	}
	
	transform.position = pos;
}