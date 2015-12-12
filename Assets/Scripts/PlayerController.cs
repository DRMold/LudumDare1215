using UnityEngine;
using System.Collections;

// Player Boundaries
[System.Serializable] 
public class Boundary 
{ public float xMin, xMax, yMin, yMax, zMin, zMax; }


public class PlayerController : MonoBehaviour {
	public Boundary boundary;
	public float speed, rollSpeed, maxSpeed;

	private Rigidbody myBody;

	void Start () {
		myBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, rollSpeed);

		myBody.AddForce (movement * speed);
		if(myBody.velocity.magnitude > maxSpeed)
		{
			myBody.velocity = myBody.velocity.normalized * maxSpeed;
		}


		GetComponent<Rigidbody> ().position = new Vector3
			(
				Mathf.Clamp (myBody.position.x, boundary.xMin, boundary.xMax), 
				myBody.position.y,
				myBody.position.z
			);
	}
}