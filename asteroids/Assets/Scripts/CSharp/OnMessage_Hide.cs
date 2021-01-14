using UnityEngine;
using System.Collections;

public class OnMessage_Hide : MonoBehaviour {

	public string comment = "(Usage comment)";
	
	public string messageName = "";

	public bool showLogMessages = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Message(string Name) {
		if(showLogMessages) Debug.Log("Got Message: Name='" + Name + "' on object '" + name + "'\n");
		if(string.IsNullOrEmpty(messageName)==false && messageName != Name) return;

		if(gameObject.GetComponent<Renderer>()) {
			gameObject.transform.GetComponent<Renderer>().enabled = false;
			if(showLogMessages) Debug.Log( "On Message Hide: Name='" + Name + "' on object '" + name + "'\n");
		}
		else {
			if(showLogMessages) Debug.LogWarning("Ignoring On Message Hide: Name='" + Name + "' on object '" + name + "' due to no render component.\n");
		}
	}
}
