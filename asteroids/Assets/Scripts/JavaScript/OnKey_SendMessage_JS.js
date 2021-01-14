#pragma strict

enum OnKey_MesssageDirection_JS { SelfAndAncestor, SelfOnly, SelfAndChildren }

var comment : String = "(Usage comment)";

var sendOnKey : KeyCode = KeyCode.Space;
var messageName : String = "";

var sendMessageTo : OnKey_MesssageDirection_JS = OnKey_MesssageDirection_JS.SelfOnly;
	
var errorIfNoReceiver : boolean  = true;

var showLogMessages : boolean  = false;


// Update is called once per frame
function Update () {
	if (Input.GetKeyDown(sendOnKey)) {
		
		if(sendMessageTo == OnKey_MesssageDirection_JS.SelfAndAncestor) {
			if(errorIfNoReceiver) SendMessageUpwards("Message", messageName, SendMessageOptions.RequireReceiver);
			else SendMessageUpwards("Message", messageName, SendMessageOptions.DontRequireReceiver);
		}
		
		if(sendMessageTo == OnKey_MesssageDirection_JS.SelfOnly) {
			if(errorIfNoReceiver) SendMessage("Message", messageName, SendMessageOptions.RequireReceiver);
			else SendMessage("Message", messageName, SendMessageOptions.DontRequireReceiver);
		}
		
		if(sendMessageTo == OnKey_MesssageDirection_JS.SelfAndChildren) {
			if(errorIfNoReceiver) BroadcastMessage("Message", messageName, SendMessageOptions.RequireReceiver);
			else BroadcastMessage("Message", messageName, SendMessageOptions.DontRequireReceiver);
		}
		
		
		if(showLogMessages) Debug.Log("Sent Message: Name='" + messageName + "' to '" + sendMessageTo + "' on object '" + name + "'\n");
	}	
}