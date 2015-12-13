using UnityEngine;
using System.Collections;

public class CivillianAI : MonoBehaviour {
	public GameController GameMaster;
	public GameObject player;
	public float threshold;

	private float distance;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		GetComponent<Rigidbody>().velocity = new Vector3(
				0.0f, 
				0.0f,
				-this.transform.position.z
			) * GameMaster.worldRot; 
	}
	
	void LateUpdate () {
		distance = Vector3.Distance (this.transform.position, player.transform.position);
		if (distance < threshold) {
			this.transform.position = Vector3.MoveTowards(
				this.transform.position, 
				GameObject.FindGameObjectWithTag("Building").transform.position, 
				GameMaster.worldRot);
		}
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Player") {
			player.GetComponent<PlayerLogic>().InvolveInFight();
			Destroy (this.gameObject);
		}
	}
}
