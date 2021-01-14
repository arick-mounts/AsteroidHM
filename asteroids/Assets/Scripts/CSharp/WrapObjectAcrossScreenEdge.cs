using UnityEngine;
using System.Collections;

public class WrapObjectAcrossScreenEdge : MonoBehaviour {

	public string comment = "(Usage comment)";

	public bool showLogMessages = false;

	private Camera gameCamera = null;
	
	private Rect rectCamLocal;
	private Vector3 camRecBottomLeft;
	private Vector3 camRecTopRight;
		
	// Use this for initialization
	void Start () {
		
		gameCamera = Camera.main;
		
		//rectCamLocal = new Rect();
		//camEdgePosition = new Vector3();
	
	}
	
	// Update is called once per frame
	
	void Update () {

	}
	
	void LateUpdate () {
		
		Vector3 pos = transform.position;
		
		//Todo - Wrap code 
		// When object move off edge of screen set to enter opposite side of sreen
				
		rectCamLocal = gameCamera.pixelRect;
		camRecBottomLeft = gameCamera.ScreenToWorldPoint(new Vector3(rectCamLocal.x,rectCamLocal.y, gameCamera.nearClipPlane));
		camRecTopRight = gameCamera.ScreenToWorldPoint(new Vector3(rectCamLocal.x+rectCamLocal.width,rectCamLocal.y+rectCamLocal.height, gameCamera.nearClipPlane));
				
		// Test right wall
		if(transform.position.x > camRecTopRight.x) {
			pos.x = camRecBottomLeft.x + 0.1f;
			if(showLogMessages) Debug.Log("Outside Camera Right");
		}
	
		// Test left wall
		if(transform.position.x < camRecBottomLeft.x) {
			pos.x = camRecTopRight.x - 0.1f;
			if(showLogMessages) Debug.Log("Outside Camera Left");
		}

		// Test top wall
		if(transform.position.z > camRecTopRight.z) {
			pos.z = camRecBottomLeft.z + 0.1f;
			if(showLogMessages) Debug.Log("Outside Camera Top");
		}
	
		// Test bottom wall
		if(transform.position.z < camRecBottomLeft.z) {
			pos.z = camRecTopRight.z - 0.1f;
			if(showLogMessages) Debug.Log("Outside Camera Bottom");
		}
		
		transform.position = pos;
	}
}
