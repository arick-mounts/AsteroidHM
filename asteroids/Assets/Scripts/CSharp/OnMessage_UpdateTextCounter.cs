using UnityEngine;
using System.Collections;

public class OnMessage_UpdateTextCounter : MonoBehaviour {

	public string comment = "(Usage comment)";

	public GUIText counterComponent = null;

	public string[] addMessage;
	public int[] addValue;
	public string[] setMessage;
	public int[] setValue;

	public string prefixText = "";
	public string suffixText = "";

	public bool showLogMessages = false;

	private int counterValue;

	// Use this for initialization
	void Start () {
		counterValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Message(string messageName) {
		if(showLogMessages) Debug.Log("Got Message: Name='" + messageName + "' on object '" + name + "'\n");

		if(counterComponent == null) {
			Debug.LogError("No GUIText component on object '" + name + "'\n");
			return;
		}

		// check to see if messageName is one of the add messages
		for( int i = 0; i<addMessage.Length; i++) {
			// check
			if(string.IsNullOrEmpty(addMessage[i])==false && addMessage[i] != messageName){
				if(showLogMessages) Debug.Log("Found Add Message: '" + addMessage[i] + "' on object '" + name + "'\n");
				if(i < addValue.Length) {
					counterValue += addValue[i];
					counterComponent.text = prefixText + counterValue.ToString() + suffixText;
					if(showLogMessages) Debug.Log("Found Add Value: '" + addValue[i] + "' on object '" + name + "'\n");
				}
				else {
					Debug.LogError("Did NOT find matching Add Value on object '" + name + "'\n");
					return;
				}
			}
		}

		// check to see if messageName is one of the set messages
		for( int i = 0; i<setMessage.Length; i++) {
			// check
			if(string.IsNullOrEmpty(setMessage[i])==false && setMessage[i] != messageName){
				if(showLogMessages) Debug.Log("Found Set Message: '" + setMessage[i] + "' on object '" + name + "'\n");
				if(i < setValue.Length) {
					counterValue = setValue[i];
					counterComponent.text = prefixText + counterValue.ToString() + suffixText;
					if(showLogMessages) Debug.Log("Found Set Value: '" + setValue[i] + "' on object '" + name + "'\n");
				}
				else {
					Debug.LogError("Did NOT find matching Set Value on object '" + name + "'\n");
					return;
				}
			}
		}
	}

}
