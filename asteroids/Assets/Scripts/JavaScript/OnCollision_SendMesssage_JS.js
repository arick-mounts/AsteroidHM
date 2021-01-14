#pragma strict

enum OnCollision_MesssageDirection_JS { SelfAndAncestor, SelfOnly, SelfAndChildren }

var comment : String = "(Usage comment)";

var messageOnCollisionEnter : String = "";
var messageOnCollisionExit : String = "";
var messageOnTriggerEnter : String = "";
var messageOnTriggerExit : String = "";

var sendMessageTo : OnCollision_MesssageDirection_JS = OnCollision_MesssageDirection_JS.SelfOnly;
	
var errorIfNoReceiver : boolean = true;

var doCollideWith : String[] ;

var showLogMessages : boolean = false;

// Use this for initialization
function Start () {

}

// Update is called once per frame
function Update () {

}
	
function OnCollisionEnter(collision : Collision) {
	if(String.IsNullOrEmpty(messageOnCollisionEnter)) return;

	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && collision.gameObject.name==objName) {
			SendCollisionMessage(messageOnCollisionEnter, "Object '"+ this.gameObject.name + "' collided with '" + collision.gameObject.name);
		}
	}
}

function OnCollisionExit(collision : Collision) {
	if(String.IsNullOrEmpty(messageOnCollisionExit)) return;

	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && collision.gameObject.name==objName) {
			SendCollisionMessage(messageOnCollisionExit, "Object '"+ this.gameObject.name + "' collided with '" + collision.gameObject.name);
		}
	}
}

function OnTriggerEnter(other : Collider) {
	if(String.IsNullOrEmpty(messageOnTriggerEnter)) return;

	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && other.name==objName) {
			SendCollisionMessage(messageOnTriggerEnter, "Object '"+ this.gameObject.name + "' collided with '" + other.name);
		}
	}
}

function OnTriggerExit(other : Collider) {
	if(String.IsNullOrEmpty(messageOnTriggerExit)) return;

	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && other.name==objName) {
			SendCollisionMessage(messageOnTriggerExit, "Object '"+ this.gameObject.name + "' collided with '" + other.name);
		}
	}
}

function SendCollisionMessage(message : String, log : String) {

	if(String.IsNullOrEmpty(message)) return;

	if(sendMessageTo == OnCollision_MesssageDirection_JS.SelfAndAncestor) {
		if(errorIfNoReceiver) SendMessageUpwards("Message", message, SendMessageOptions.RequireReceiver);
		else SendMessageUpwards("Message", message, SendMessageOptions.DontRequireReceiver);
		if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnCollision_MesssageDirection_JS.SelfAndAncestor + "''\n");
	}
	
	if(sendMessageTo == OnCollision_MesssageDirection_JS.SelfOnly) {
		if(errorIfNoReceiver) SendMessage("Message", message, SendMessageOptions.RequireReceiver);
		else SendMessage("Message", message, SendMessageOptions.DontRequireReceiver);
		if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnCollision_MesssageDirection_JS.SelfOnly + "''\n");
	}
	
	if(sendMessageTo == OnCollision_MesssageDirection_JS.SelfAndChildren) {
		if(errorIfNoReceiver) BroadcastMessage("Message", message, SendMessageOptions.RequireReceiver);
		else BroadcastMessage("Message", message, SendMessageOptions.DontRequireReceiver);
		if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnCollision_MesssageDirection_JS.SelfAndChildren + "''\n");
	}

}