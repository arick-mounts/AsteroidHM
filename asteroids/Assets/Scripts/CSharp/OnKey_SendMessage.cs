using UnityEngine;
using System.Collections;

public class OnKey_SendMessage : MonoBehaviour {

	public enum OnKey_MesssageDirection { SelfAndAncestor, SelfOnly, SelfAndChildren };

	public string comment = "(Usage comment)";
	
	public KeyCode sendOnKey  = KeyCode.Space;
	public string messageName = "";
	
	public OnKey_MesssageDirection sendMessageTo = OnKey_MesssageDirection.SelfOnly;
		
	public bool errorIfNoReceiver = true;
	
	public bool showLogMessages = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(sendOnKey)) {
			
			if(sendMessageTo == OnKey_MesssageDirection.SelfAndAncestor) {
				if(errorIfNoReceiver) SendMessageUpwards("Message", messageName, SendMessageOptions.RequireReceiver);
				else SendMessageUpwards("Message", messageName, SendMessageOptions.DontRequireReceiver);
			}
			
			if(sendMessageTo == OnKey_MesssageDirection.SelfOnly) {
				if(errorIfNoReceiver) SendMessage("Message", messageName, SendMessageOptions.RequireReceiver);
				else SendMessage("Message", messageName, SendMessageOptions.DontRequireReceiver);
			}
			
			if(sendMessageTo == OnKey_MesssageDirection.SelfAndChildren) {
				if(errorIfNoReceiver) BroadcastMessage("Message", messageName, SendMessageOptions.RequireReceiver);
				else BroadcastMessage("Message", messageName, SendMessageOptions.DontRequireReceiver);
			}
			
			
			if(showLogMessages) Debug.Log("Sent Message: Name='" + messageName + "' to '" + sendMessageTo + "' on object '" + name + "'\n");
		}	
	}
}
