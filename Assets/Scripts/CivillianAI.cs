using UnityEngine;
using System.Collections;

public class CivillianAI : MonoBehaviour {
	public GameController GameMaster;
	public float threshold;

	private float distance;
	private Transform startTrans;
	private Transform player;

	void Start () {
		player = GameObject.FindWithTag ("Player").transform;
		GetComponent<Rigidbody>().velocity = new Vector3(
				0.0f, 
				0.0f,
				-this.transform.position.z
			) * GameMaster.worldRot; 
	}
	
	void LateUpdate () {
		distance = Vector3.Distance (this.transform.position, player.position);
		if (distance < threshold ) {
			RunAway();
		}
	}

	public void RunAway() {
		startTrans = transform;

		// Turn away from player
		transform.rotation = Quaternion.LookRotation (transform.position - player.position);

		// Where are we running to?
		Vector3 runTo = transform.position + transform.forward * Random.Range (5, 25);
		GetComponent<Rigidbody> ().velocity = 0.25f * runTo * GameMaster.worldRot;
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Player") {
			player.GetComponent<PlayerLogic> ().InvolveInFight ();
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "Building") {
			Destroy (this.gameObject);
		}
	}
}
