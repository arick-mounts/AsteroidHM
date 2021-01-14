using UnityEngine;
using System.Collections;

public class RockControler : MonoBehaviour {

	public string comment = "(Usage comment)";

	public float driftSpeed = 10f;
	public float rollSpeed = 10f;
	
	public bool showLogMessages = false;

	private Vector3 acceleration;
	
	// Use this for initialization
	void Start () {
	
		// Set random position (not in center)
		acceleration = Random.onUnitSphere * driftSpeed;
		acceleration.y = 0;
		acceleration = acceleration.normalized * driftSpeed;
	
		// Todo - Set random roll direction and speed
		if(Random.value > .5) rollSpeed *= -1f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += acceleration * Time.deltaTime;
		transform.Rotate(0,Time.deltaTime * rollSpeed, 0);
	}
}
