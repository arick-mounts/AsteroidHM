using UnityEngine;
using System.Collections;

public class MissileControler : MonoBehaviour {

	public string comment = "(Usage comment)";

	public float moveSpeed = 50f;
	public float lifeSpan = 2f;

	public bool showLogMessages = false;

	// Use this for initialization
	void Start () {
		if(lifeSpan > .001f) {
			Destroy(gameObject, lifeSpan);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, Time.deltaTime * moveSpeed);
	}
}
