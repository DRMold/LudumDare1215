using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private bool gameOver;
	private bool paused;

	public int buildCount;
	public float spawnWait, startWait, waveWait;
	public GameObject prefab;

	public short playerState;
	public float globalCurvature = 0.05f;
	public float worldRot;

	protected GameController () {}
	
	void Awake () {
//				if (instance == null) {
//					instance = this.gameObject;
//				} else if (instance != this) {
//					Destroy(gameObject);
//				}

		Shader.SetGlobalFloat("_GlobalCurvature", globalCurvature);
		
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
		if (paused || gameOver)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;

//		var chance = Random.value * 100;
//		if (chance<20) {
//			for (int i=0; i < chance; i++) {
//				Instantiate(prefab,this.transform.position +(new Vector3(Mathf.Pow(-1, i)*7, 1.0f, i*5.0f)), Quaternion.identity);
//			}
//
//		}
	}

	public void Pause()
	{ this.paused = !this.paused; }
	
	public void GameOver()
	{ gameOver = true; } 
	
	public bool getGameOver()
	{ return gameOver; }


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

}
