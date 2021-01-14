using UnityEngine;
using System.Collections;

public class OnTriggerEmpty_SendMessage : MonoBehaviour {
	
	public enum OnTriggerEmptyMesssageDirection { SelfAndAncestor, SelfOnly, SelfAndChildren };
	public string comment = "(Usage comment)";
	public string messageOnTriggerEmpty = "";
	public OnTriggerEmptyMesssageDirection sendMessageTo = OnTriggerEmptyMesssageDirection.SelfOnly;
	public bool errorIfNoReceiver = true;
	public string[] doCollideWith;
	public bool showLogMessages = false;

	private int InTriggerCount = 0;
	private bool SentIsEmptyMessage = false;

	// seems we must wait a few moments for collision to be detected
	private int delayUntilFrame = 0;

	void OnEnable() {
		InTriggerCount = 0;
		SentIsEmptyMessage = false;
		delayUntilFrame = Time.frameCount + 3;
	}

	void LateUpdate() {
		if(string.IsNullOrEmpty(messageOnTriggerEmpty)) return;

		if(SentIsEmptyMessage == false) {
			if(InTriggerCount == 0 && Time.frameCount > delayUntilFrame) {
				SentIsEmptyMessage = true;
				SendCollisionMessage(messageOnTriggerEmpty, "Object '"+ this.gameObject.name + "'\n");
			}
		}
		else {
			if(InTriggerCount > 0) {
				// if there are objects in the trigger, clear sent message flag 
				//  so we can send the message again when it's empty
				SentIsEmptyMessage = false;
				delayUntilFrame = Time.frameCount + 3;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && other.name==objName) {
				InTriggerCount++;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && other.name==objName) {
				InTriggerCount--;
			}
		}
	}
	
	void SendCollisionMessage(string message, string log) {
		
		if(string.IsNullOrEmpty(message)) return;
		
		if(sendMessageTo == OnTriggerEmptyMesssageDirection.SelfAndAncestor) {
			if(errorIfNoReceiver) SendMessageUpwards("Message", message, SendMessageOptions.RequireReceiver);
			else SendMessageUpwards("Message", message, SendMessageOptions.DontRequireReceiver);
			if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnTriggerEmptyMesssageDirection.SelfAndAncestor + "''\n");
		}
		
		if(sendMessageTo == OnTriggerEmptyMesssageDirection.SelfOnly) {
			if(errorIfNoReceiver) SendMessage("Message", message, SendMessageOptions.RequireReceiver);
			else SendMessage("Message", message, SendMessageOptions.DontRequireReceiver);
			if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnTriggerEmptyMesssageDirection.SelfOnly + "''\n");
		}
		
		if(sendMessageTo == OnTriggerEmptyMesssageDirection.SelfAndChildren) {
			if(errorIfNoReceiver) BroadcastMessage("Message", message, SendMessageOptions.RequireReceiver);
			else BroadcastMessage("Message", message, SendMessageOptions.DontRequireReceiver);
			if(showLogMessages) Debug.Log(log + "' sent Message Name ='" + message + "' to '" + OnTriggerEmptyMesssageDirection.SelfAndChildren + "''\n");
		}
		
	}
}
