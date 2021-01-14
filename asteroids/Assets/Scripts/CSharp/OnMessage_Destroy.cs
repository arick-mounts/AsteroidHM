using UnityEngine;
using System.Collections;

public class OnMessage_Destroy : MonoBehaviour {

	public string comment = "(Usage comment)";
	public string messageName = "";
	public bool showLogMessages = false;
	
	void Message(string Name) {
		if(showLogMessages) Debug.Log("Got Message: Name='" + Name + "' on object '" + name + "'\n");
		if(string.IsNullOrEmpty(messageName)==false && messageName != Name) return;
		if(showLogMessages) Debug.Log("Creating Object On Message: Name='" + Name + "' on object '" + name + "'\n");
				
		Destroy(this.gameObject);
	}
}
