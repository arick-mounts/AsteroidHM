#pragma strict

var  comment : String = "(Usage comment)";

var messageName : String = "";

var showLogMessages : boolean = false;

function Message(Name : String) {
	if(showLogMessages) Debug.Log("Got Message: Name='" + Name + "' on object '" + name + "'\n");
	if(String.IsNullOrEmpty(messageName)==false && messageName != Name) return;
	if(showLogMessages) Debug.Log("Creating Object On Message: Name='" + Name + "' on object '" + name + "'\n");
			
	Destroy(this.gameObject);
}
