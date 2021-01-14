#pragma strict

var comment : String = "(Usage comment)";

var messageName : String = "";

var showLogMessages : boolean = false;

function Message(Name : String) {
	if(showLogMessages) Debug.Log("Got Message: Name='" + Name + "' on object '" + name + "'\n");
	if(String.IsNullOrEmpty(messageName)==false && messageName != Name) return;

	if(GetComponent(Renderer)) {
		gameObject.transform.GetComponent.<Renderer>().enabled = false;
		if(showLogMessages) Debug.Log( "On Message Hide: Name='" + Name + "' on object '" + name + "'\n");
	}
	else {
		if(showLogMessages) Debug.LogWarning("Ignoring On Message Hide: Name='" + Name + "' on object '" + name + "' due to no render component.\n");
	}
}