using UnityEngine;
using System.Collections;

public class OnCollision_Destroy : MonoBehaviour {

	public string comment = "(Usage comment)";

	public bool destroyOther = true;
	public bool destroySelf = true;
	
	public string[] doCollideWith;

	public bool showLogMessages = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnCollisionEnter(Collision collision) {
						
		foreach(string objName in doCollideWith) {
			if(string.IsNullOrEmpty(objName)==false && collision.gameObject.name!=objName) {
				if(showLogMessages) Debug.Log("Object '"+ this.gameObject.name + "' collided with '" + collision.gameObject.name + "'\n");
				if(destroyOther) Destroy(collision.gameObject);
				if(destroySelf) Destroy(this.gameObject);
			}
		}
    }
}
