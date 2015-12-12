using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {
	public float speed;

	private Rigidbody myBody;

	void Start () {
		myBody = GetComponent<Rigidbody> ();
	}

	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

		myBody.AddForce (movement * speed);
	}
}