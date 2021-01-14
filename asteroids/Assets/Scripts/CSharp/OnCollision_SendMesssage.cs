using UnityEngine;
using System.Collections;

public class OnCollision_SendMesssage : MonoBehaviour {
	
	public enum MesssageDirection { SelfAndAncestor, SelfOnly, SelfAndChildren };
	
	public string comment = "(Usage comment)";

	public string messageOnCollisionEnter = "";
	public string messageOnCollisionExit = "";
	public string messageOnTriggerEnter = "";
	public string messageOnTriggerExit = "";

	public MesssageDirection sendMessageTo = MesssageDirection.SelfOnly;
		
	public bool errorIfNoReceiver = true;
	
	public string[] doCollideWith;
	
	public bool showLogMessages = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnCollisionEnter(Collision collision) {
		if(string.IsNullOrEmpty(messageOnCollisionEnter)) return;

		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && collision.gameObject.name==objName) {
				SendCollisionMessage(messageOnCollisionEnter, "Object '"+ this.gameObject.name + "' collided with '" + collision.gameObject.name);
			}
		}
    }

	void OnCollisionExit(Collision collision) {
		if(string.IsNullOrEmpty(messageOnCollisionExit)) return;

		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && collision.gameObject.name==objName) {
				SendCollisionMessage(messageOnCollisionExit, "Object '"+ this.gameObject.name + "' collided with '" + collision.gameObject.name);
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(string.IsNullOrEmpty(messageOnTriggerEnter)) return;

		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && other.name==objName) {
				SendCollisionMessage(messageOnTriggerEnter, "Object '"+ this.gameObject.name + "' collided with '" + other.name);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(string.IsNullOrEmpty(messageOnTriggerExit)) return;

		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && other.name==objName) {
				SendCollisionMessage(messageOnTriggerExit, "Object '"+ this.gameObject.name + "' collided with '" + other.name);
			}
		}
	}

	void SendCollisionMessage(string message, string log) {

		if(string.IsNullOrEmpty(message)) return;

		if(sendMessageTo == MesssageDirection.SelfAndAncestor) {
			if(errorIfNoReceiver) SendMessageUpwards("Message", message, SendMessageOptions.RequireReceiver);
			else SendMessageUpwards("Message", message, SendMessageOptions.DontRequireReceiver);
			if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + MesssageDirection.SelfAndAncestor + "''\n");
		}
		
		if(sendMessageTo == MesssageDirection.SelfOnly) {
			if(errorIfNoReceiver) SendMessage("Message", message, SendMessageOptions.RequireReceiver);
			else SendMessage("Message", message, SendMessageOptions.DontRequireReceiver);
			if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + MesssageDirection.SelfOnly + "''\n");
		}
		
		if(sendMessageTo == MesssageDirection.SelfAndChildren) {
			if(errorIfNoReceiver) BroadcastMessage("Message", message, SendMessageOptions.RequireReceiver);
			else BroadcastMessage("Message", message, SendMessageOptions.DontRequireReceiver);
			if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + MesssageDirection.SelfAndChildren + "''\n");
		}

	}
}
