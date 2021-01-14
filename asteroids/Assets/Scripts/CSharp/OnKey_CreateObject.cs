using UnityEngine;
using System.Collections;

public class OnKey_CreateObject : MonoBehaviour {

	public string comment = "(Usage comment)";
	
	public KeyCode spawnKey  = KeyCode.Space;
	public Transform createObject = null;
	public Transform createPosition = null;
	public Transform setAsChildOf = null;
	public string setAsChildOfTag = "";

	public bool showLogMessages = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(spawnKey)) {
			if(createObject!=null) { 
				GameObject obj = (GameObject) Instantiate(createObject.gameObject);

				if(createPosition!=null) {
					obj.transform.position = createPosition.transform.position;
					obj.transform.rotation = createPosition.transform.rotation;
				}
				else {
					obj.transform.position = Vector3.zero;
					obj.transform.Rotate(Vector3.zero);
				}

				if(setAsChildOf!=null) {
					obj.transform.parent = setAsChildOf;
				}
				else {
					if(string.IsNullOrEmpty(setAsChildOfTag)==false) {
						GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(setAsChildOfTag);
						
						if(taggedObjects.Length > 0) {
							obj.transform.parent = taggedObjects[0].transform;
							if(taggedObjects.Length > 1) Debug.LogWarning("Found MORE than 1 object with Tag '" + setAsChildOfTag + "'\n");
						}
						else {
							Debug.LogError("Could NOT find object with Tag '" + setAsChildOfTag + "'\n");
						}
					}
				}
			}
			else Debug.LogError("The field 'Launch Object' is null on object '" + name + "'\n" );
		}
	}
}
