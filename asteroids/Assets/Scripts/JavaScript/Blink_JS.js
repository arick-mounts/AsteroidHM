#pragma strict

var blinkSpeed : float = 1;

function OnEnable () {
	if(gameObject.GetComponent.<Renderer>()!=null) 
		StartCoroutine(BlinkRenderer());

	if(gameObject.GetComponent.<GUIText>()!=null) 
		StartCoroutine(BlinkGuiText());

	if(gameObject.GetComponent.<Renderer>()==null && gameObject.GetComponent.<GUIText>()==null) {
		Debug.LogError("Could NOT Blink object '" + gameObject.name + "' (no Render or GuiText)\n");
	}

}


function Start () {

}

function Update () {

}

function BlinkRenderer() {
	while(true) {
		yield WaitForSeconds(blinkSpeed/2f);
		gameObject.GetComponent.<Renderer>().enabled = false;
		yield WaitForSeconds(blinkSpeed/2f);
		gameObject.GetComponent.<Renderer>().enabled = true;
	}
}

function BlinkGuiText() {
	while(true) {
		yield WaitForSeconds(blinkSpeed/2f);
		gameObject.GetComponent.<GUIText>().enabled = false;
		yield WaitForSeconds(blinkSpeed/2f);
		gameObject.GetComponent.<GUIText>().enabled = true;
	}
}
