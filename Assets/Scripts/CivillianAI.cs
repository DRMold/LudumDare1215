using UnityEngine;
using System.Collections;

public class CivillianAI : MonoBehaviour {
	public GameController GameMaster;
	public float threshold;

	private float distance;
	private Transform startTrans;
	private Transform player;
	private Animation anim;

	void Start () {
		anim = GetComponent<Animation>();
		anim["Run"].speed = 10.0f; 

		player = GameObject.FindWithTag ("Player").transform;
		GetComponent<Rigidbody>().velocity = new Vector3(
				0.0f, 
				0.0f,
				-this.transform.position.z
			) * GameMaster.worldRot * 0.8f; 
	}

	void Update () {
		transform.rotation = Quaternion.LookRotation (transform.forward);

		if (anim.isPlaying == false) {
			if (true) {
				anim.Play("Run");
			}
		}
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
		Vector3 runTo = Vector3.MoveTowards
			(
				this.transform.position, 
				GameObject.FindGameObjectWithTag ("Building").transform.position,
				GameMaster.worldRot
			);
		GetComponent<Rigidbody> ().velocity = new Vector3
			(
				-runTo.x,
				0.0f,
				-runTo.z
			) * 0.75f;
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Player") {
			player.GetComponent<PlayerLogic> ().InvolveInFight ();
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "Building") {
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "Vehicle") {
			Destroy (this.gameObject, 5);
		}
	}
}
