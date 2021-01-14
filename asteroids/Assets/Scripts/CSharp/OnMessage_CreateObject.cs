using UnityEngine;
using System.Collections;

public class OnMessage_CreateObject : MonoBehaviour {
	
	public string comment = "(Usage comment)";

	public GameObject createObject = null;
	public Transform createPosition = null;
	public Transform setAsChildOfObject = null;
	public string setAsChildOfTag = "";
	
	public float randomPositionMinimum = 1;
	public float randomPositionMaximum = 10;
	
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

		if(createObject && createPosition) {
			if(showLogMessages) Debug.Log("Creating Object On Message: Name='" + Name + "' on object '" + name + "'\n");

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

				// set random position offset
				Vector2 randomPointOnCircle = Random.insideUnitCircle;
				randomPointOnCircle.Normalize();
				randomPointOnCircle *= Random.Range(randomPositionMinimum, randomPositionMaximum);
				Vector3 newPosition = new Vector3(randomPointOnCircle.x, 0f, randomPointOnCircle.y);
				obj.transform.position += newPosition;

				// set parent if requested
				if(setAsChildOfObject!=null) {
					obj.transform.parent = setAsChildOfObject;
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
		}
		else {
			Debug.LogError("Could NOT Create Object On Message: Name='" + Name + "' on object '" + name + "'\n");
		}
	}
}
