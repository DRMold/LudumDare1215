using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private bool gameOver;
	private bool paused;

	public int buildCount;
	public float spawnWait, startWait, waveWait;
	public GameObject prefab;

	public short playerState;

	protected GameController () {}
	
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
		gameOver = false;
		paused = false;

		if (playerState == null || playerState == 0) {
			playerState = 1;
		}
	
		StartCoroutine (GenerateBuildings ());
	}

	// Update is called once per frame
	void Update () {
//		var chance = Random.value * 100;
//		if (chance<20) {
//			for (int i=0; i < chance; i++) {
//				Instantiate(prefab,this.transform.position +(new Vector3(Mathf.Pow(-1, i)*7, 1.0f, i*5.0f)), Quaternion.identity);
//			}
//
//		}
	}

	IEnumerator GenerateBuildings() {
		float z = 0.0f;
		yield return new WaitForSeconds (startWait);
		while (true) {
			yield return new WaitForSeconds(waveWait);
			for (int i=0; i<buildCount; i++) {
				Vector3 position = new Vector3(Mathf.Pow(-1, i)*7, 1.0f, 300 + this.transform.position.z);
				if (z == 180) {z=0.0f;} else {z=180.0f;}
				Instantiate (prefab, position, Quaternion.Euler (270.0f, 0.0f, 180.0f + z));
				yield return new WaitForSeconds(spawnWait);
			}
			
			if (gameOver) {
				break;
			}
		}
		
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Ground") {
			other.transform.position = new Vector3(
					0.0f,
					0.0f,
					other.transform.position.z + 370
				);
		} else if(other.tag == "Building") {
			Destroy(other.gameObject);
		}
	}
}
