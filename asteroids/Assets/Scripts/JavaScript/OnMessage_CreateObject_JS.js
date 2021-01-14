#pragma strict

var comment : String = "(Usage comment)";

var createObject : GameObject = null;
var createPosition : Transform = null;
var setAsChildOfObject : Transform = null;
var setAsChildOfTag : String = "";

var randomPositionMinimum : float = 1;
var randomPositionMaximum : float = 10;

var messageName : String = "";

var showLogMessages : boolean = false;

// Use this for initialization
function Start () {

}

// Update is called once per frame
function Update () {

}

function Message(Name : String) {
	if(showLogMessages) Debug.Log("Got Message: Name='" + Name + "' on object '" + name + "'\n");
	if(String.IsNullOrEmpty(messageName)==false && messageName != Name) return;

	if(createObject && createPosition) {
		if(showLogMessages) Debug.Log("Creating Object On Message: Name='" + Name + "' on object '" + name + "'\n");

		if(createObject!=null) {
			var obj = Instantiate(createObject.gameObject);

			if(createPosition!=null) {
				obj.transform.position = createPosition.transform.position;
				obj.transform.rotation = createPosition.transform.rotation;
			}
			else {
				obj.transform.position = Vector3.zero;
				obj.transform.Rotate(Vector3.zero);
			}

			// set random position offset
			var randomPointOnCircle = Random.insideUnitCircle;
			randomPointOnCircle.Normalize();
			randomPointOnCircle *= Random.Range(randomPositionMinimum, randomPositionMaximum);
			var newPosition = new Vector3(randomPointOnCircle.x, 0f, randomPointOnCircle.y);
			obj.transform.position += newPosition;

			// set parent if requested
			if(setAsChildOfObject!=null) {
				obj.transform.parent = setAsChildOfObject;
			}
			else {
				if(String.IsNullOrEmpty(setAsChildOfTag)==false) {
					var taggedObjects = GameObject.FindGameObjectsWithTag(setAsChildOfTag);

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