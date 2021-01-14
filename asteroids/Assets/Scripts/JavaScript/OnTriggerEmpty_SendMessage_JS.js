#pragma strict

enum OnTriggerEmpty_MesssageDirection_JS { SelfAndAncestor, SelfOnly, SelfAndChildren }

var comment : String = "(Usage comment)";

var messageOnTriggerEmpty : String = "";

var sendMessageTo : OnTriggerEmpty_MesssageDirection_JS = OnTriggerEmpty_MesssageDirection_JS.SelfOnly;

var errorIfNoReceiver : boolean = true;

var doCollideWith : String[];

var showLogMessages : boolean = false;


private var InTriggerCount : int;
private var SentIsEmptyMessage : boolean;

// seems we must wait a few moments for collision to be detected
private var delayUntilFrame : int;


function OnEnable() {
	InTriggerCount = 0;
	SentIsEmptyMessage = false;
	delayUntilFrame = Time.frameCount+3;
}

function LateUpdate() {
	if(String.IsNullOrEmpty(messageOnTriggerEmpty)) return;

	if(SentIsEmptyMessage == false) {
		if(InTriggerCount == 0 && Time.frameCount > delayUntilFrame) {
			SentIsEmptyMessage = true;
			SendCollisionMessage(messageOnTriggerEmpty, "Object '" + this.gameObject.name + "'\n");
		}
	}
	else {
		if(InTriggerCount > 0) {
			// if there are objects in the trigger, clear sent message flag 
			//  so we can send the message again when it's empty
			SentIsEmptyMessage = false;
			delayUntilFrame = Time.frameCount+3;
		}
	}

}

function OnTriggerEnter(other : Collider) {
	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && other.name==objName) {
			InTriggerCount++;
		}
	}
}

function OnTriggerExit(other : Collider) {
	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && other.name==objName) {
			InTriggerCount--;
			if(InTriggerCount < 0)
				Debug.LogError("OnTriggerEmpty object count is negative (InTriggerCount=" + InTriggerCount + ")\n");
		}
	}
}

function SendCollisionMessage(message : String, log : String) {
	
	if(String.IsNullOrEmpty(message)) return;
	
	if(sendMessageTo == OnTriggerEmpty_MesssageDirection_JS.SelfAndAncestor) {
		if(errorIfNoReceiver) SendMessageUpwards("Message", message, SendMessageOptions.RequireReceiver);
		else SendMessageUpwards("Message", message, SendMessageOptions.DontRequireReceiver);
		if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnTriggerEmpty_MesssageDirection_JS.SelfAndAncestor + "''\n");
	}
	
	if(sendMessageTo == OnTriggerEmpty_MesssageDirection_JS.SelfOnly) {
		if(errorIfNoReceiver) SendMessage("Message", message, SendMessageOptions.RequireReceiver);
		else SendMessage("Message", message, SendMessageOptions.DontRequireReceiver);
		if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnTriggerEmpty_MesssageDirection_JS.SelfOnly + "''\n");
	}
	
	if(sendMessageTo == OnTriggerEmpty_MesssageDirection_JS.SelfAndChildren) {
		if(errorIfNoReceiver) BroadcastMessage("Message", message, SendMessageOptions.RequireReceiver);
		else BroadcastMessage("Message", message, SendMessageOptions.DontRequireReceiver);
		if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnTriggerEmpty_MesssageDirection_JS.SelfAndChildren + "''\n");
	}
	
}