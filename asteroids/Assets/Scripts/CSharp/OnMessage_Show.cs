using UnityEngine;
using System.Collections;

public class OnMessage_Show : MonoBehaviour {

	public string comment = "(Usage comment)";
	public string messageName = "";
	public bool showLogMessages = false;

	void Message(string Name) {
		if(showLogMessages) Debug.Log("Got Message: Name='" + Name + "' on object '" + name + "'\n");
		if(string.IsNullOrEmpty(messageName)==false && messageName != Name) return;

		if(gameObject.GetComponent<Renderer>()) {
			gameObject.transform.GetComponent<Renderer>().enabled = true;
			if(showLogMessages) Debug.Log( "On Message Show: Name='" + Name + "' on object '" + name + "'\n");
		}
		else {
			if(showLogMessages) Debug.LogWarning("Ignoring On Message Show: Name='" + Name + "' on object '" + name + "' due to no render component.\n");
		}
	}
}
