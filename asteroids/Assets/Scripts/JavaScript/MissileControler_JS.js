#pragma strict

var comment : String = "(Usage comment)";

var moveSpeed : float = 50f;
var lifeSpan : float = 2f;

var showLogMessages : boolean = false;

// Use this for initialization
function Start () {
	if(lifeSpan > .001f) {
		Destroy(gameObject, lifeSpan);
	}
}

// Update is called once per frame
function Update () {
	transform.Translate(0, 0, Time.deltaTime * moveSpeed);
}
