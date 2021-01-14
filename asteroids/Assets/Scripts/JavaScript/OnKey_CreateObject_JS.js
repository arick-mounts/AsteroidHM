#pragma strict

var comment : String = "(Usage comment)";

var spawnKey : KeyCode = KeyCode.Space;
var createObject : Transform;
var createPosition : Transform;
var setAsChildOf : Transform;
var setAsChildOfTag : String = "";

var showLogMessages : boolean = false;

// Update is called once per frame
function Update () {
	if (Input.GetKeyDown(spawnKey)) {
		if(createObject!=null) {
			var obj : GameObject = Instantiate(createObject.gameObject);

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
				if(String.IsNullOrEmpty(setAsChildOfTag)==false) {
					var taggedObjects : GameObject[] = GameObject.FindGameObjectsWithTag(setAsChildOfTag);
					
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