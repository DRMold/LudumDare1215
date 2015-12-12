using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	protected GameController () {}

	public short playerState;
	
	void Awake () {
//				if (instance == null) {
//					instance = this.gameObject;
//				} else if (instance != this) {
//					Destroy(gameObject);
//				}
		
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {

		if (playerState == null || playerState == 0) {
			playerState = 1;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Ground") {
			other.transform.position = new Vector3(
					0.0f,
					0.0f,
					other.transform.position.z + 370
				);
		}
	}
}
