using UnityEngine;
using System.Collections;

// Player Boundaries
[System.Serializable] 
public class Boundary 
{ public float xMin, xMax, zMin, zMax; }


public class PlayerController : MonoBehaviour {
	public Boundary boundary;
	public float speed, rollSpeed, maxSpeed;

	private Rigidbody myBody;

	void Start () {
		myBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		myBody.velocity = movement * speed;
		//myBody.AddForce (movement * speed);
		if(myBody.velocity.magnitude > maxSpeed) {
			myBody.velocity = myBody.velocity.normalized * maxSpeed;
		}


		GetComponent<Rigidbody> ().position = new Vector3
			(
				Mathf.Clamp (myBody.position.x, boundary.xMin, boundary.xMax), 
				myBody.position.y,
				Mathf.Clamp (myBody.position.z, boundary.zMin, boundary.zMax)
			);
	}
}