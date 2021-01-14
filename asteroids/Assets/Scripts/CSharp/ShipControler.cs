using UnityEngine;
using System.Collections;

public class ShipControler : MonoBehaviour {

	public string comment = "(Usage comment)";

	public float movementMaxSpeed = 1f;
	public float movementAcceleration = .2f;
	
	public float turnMaxSpeed = 1f;
	public float turnAcceleration = .5f;

	public Transform spawnPoint = null;

	public bool showLogMessages = false;

	private Vector3 currenMovementVelocity;
	private float currentTurnVelocity = 0f;
	

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable() {
		transform.position = spawnPoint.position;
		transform.rotation = spawnPoint.rotation;

		currenMovementVelocity = Vector3.zero;
		currentTurnVelocity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		// calc acceleration
		float vertInput = Input.GetAxis("Vertical");
		if(vertInput > 0f) {
  			currenMovementVelocity += transform.forward * vertInput * Time.deltaTime * movementAcceleration;
		}

		float hozInput = Input.GetAxis("Horizontal") * Time.deltaTime * turnAcceleration;
		if(Mathf.Abs(hozInput) > 0.01f ) {
			currentTurnVelocity += hozInput;
		}
		else {
			currentTurnVelocity = 0f;
		}
		
		// cap acceleration
		if(currenMovementVelocity.magnitude > movementMaxSpeed) {
			currenMovementVelocity = currenMovementVelocity.normalized * movementMaxSpeed;
		}
		
		currentTurnVelocity = Mathf.Min(currentTurnVelocity, turnMaxSpeed);
		currentTurnVelocity = Mathf.Max(currentTurnVelocity, -turnMaxSpeed);
		
		// move
		transform.position += currenMovementVelocity * Time.deltaTime;
		transform.Rotate(0,Time.deltaTime * currentTurnVelocity, 0);

	}
}
