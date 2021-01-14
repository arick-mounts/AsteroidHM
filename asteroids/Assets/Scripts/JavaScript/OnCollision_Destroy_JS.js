#pragma strict

var comment :String = "(Usage comment)";

var destroyOther : boolean = true;
var destroySelf : boolean = true;

var doCollideWith : String[];

var showLogMessages : boolean = false;
	
function OnCollisionEnter(collision : Collision) {			
	for(var objName : String in doCollideWith) {
		if(String.IsNullOrEmpty(objName)==false && collision.gameObject.name!=objName) {
			if(showLogMessages) Debug.Log("Object '"+ this.gameObject.name + "' collided with '" + collision.gameObject.name + "'\n");
			if(destroyOther) Destroy(collision.gameObject);
			if(destroySelf) Destroy(this.gameObject);
		}
	}
}