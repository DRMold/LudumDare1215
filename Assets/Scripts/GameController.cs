using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private bool gameOver;
	private bool restart;
	private bool paused;

	public int buildCount;
	public float houseSpawnWait, houseStartWait, houseWaveWait;
	public GameObject housePrefab;

	public int civilCount;
	public float civilSpawnWait, civilStartWait, civilWaveWait;
	public GameObject civilPrefab;

	private float score;
//	public GUIText scoreText; 
//	public GUIText restartText;
//	public GUIText gameOverText;

	public short playerState; 
	public float worldRot;
	public float globalCurvature = 0.05f;

	protected GameController () {}
	
	void Awake () {
		Shader.SetGlobalFloat("_GlobalCurvature", globalCurvature);
		
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		score = 0;
		gameOver = false;
		restart = false;
		paused = false;

		if (playerState == null || playerState == 0) {
			playerState = 1;
		}
	
		StartCoroutine (GenerateBuildings ());
		StartCoroutine (GenerateCivillians ());
	}

	// Update is called once per frame
	void Update () {
		if (paused || gameOver)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;

		if (Input.GetKeyDown (KeyCode.P))
		{ Pause (); }

		if (Input.GetKeyDown (KeyCode.Q))
		{ GameOver(); }

		if (restart) {
			if (Input.GetKeyDown (KeyCode.R))
			{ Application.LoadLevel (Application.loadedLevel); }
		}
	}

	public void Pause()
	{ this.paused = !this.paused; }
	
	public void GameOver(){
		gameOver = true; 
		restart = true;
//		gameOverText.text = "Game Over!"; 
	} 
	
	public bool getGameOver()
	{ return gameOver; }

	public void AddScore()
	{ score += 100; }


	IEnumerator GenerateBuildings() {
		float z = 0.0f;
		yield return new WaitForSeconds (houseStartWait);
		while (true) {
			yield return new WaitForSeconds(houseWaveWait);
			for (int i=0; i<buildCount; i++) {
				Vector3 position = new Vector3(Mathf.Pow(-1, i)*10, 1.0f, 300 + this.transform.position.z);
				if (z == 180) {z=0.0f;} else {z=180.0f;}
				Instantiate (housePrefab, position, Quaternion.Euler (270.0f, 0.0f, 180.0f + z));
				yield return new WaitForSeconds(houseSpawnWait);
			}

			if (gameOver)
			{ 
//				restartText.text = "Press 'R' to restart.";
				restart = true;
				break;
			}
		}
		
	}

	IEnumerator GenerateCivillians() {
		yield return new WaitForSeconds (civilStartWait);
		while (true) {
			yield return new WaitForSeconds(civilWaveWait);
			for (int i=0; i<buildCount; i++) {
				Vector3 position = new Vector3(Mathf.Pow(-1, i)*6, 1.0f, 300 + this.transform.position.z);
				Instantiate (civilPrefab, position, Quaternion.identity);
				yield return new WaitForSeconds(civilSpawnWait);
			}
			
			if (gameOver)
			{ 
//				restartText.text = "Press 'R' to restart.";
				restart = true;
				break;
			}
		}
		
	}
}
