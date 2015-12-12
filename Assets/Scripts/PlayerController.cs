using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	public float speed;
	public float rollSpeed;

	private Rigidbody myBody;

	void Start () {
		myBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, rollSpeed);

		myBody.AddForce (movement * speed);
	} Instantiate
}