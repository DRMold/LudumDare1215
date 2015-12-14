using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private bool gameOver;
	private bool restart;
	private bool paused;
	
	public int buildCount;
	public float houseStartWait;
	public GameObject housePrefab;
	
	public int civilCount;
	public float civilStartWait;
	public GameObject civilPrefab;
	
	public int carCount;
	public float carStartWait;
	public GameObject carPrefab;
	
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
		
		StartCoroutine (GenerateVehicles ());
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
			{ Application.LoadLevel (0); }
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
	
	IEnumerator GenerateVehicles() {
		yield return new WaitForSeconds (carStartWait);
		while (true) {
			yield return new WaitForSeconds(Random.Range(1, 10));
			for (int i=0; i<carCount; i++) {
				Vector3 position = new Vector3
					(
						Mathf.Pow(-1, i)*4, 
						1.0f,
						300 + this.transform.position.z
						);
				Instantiate (carPrefab, position, Quaternion.identity);
				yield return new WaitForSeconds(Random.Range (1, 5));
			}
			
			if (gameOver)
			{ 
				//				restartText.text = "Press 'R' to restart.";
				restart = true;
				break;
			}
		}
		
	}
	
	IEnumerator GenerateBuildings() {
		Vector3 leftPos = new Vector3(-17.0f, 1.0f, 300 + this.transform.position.z);
		Vector3 rightPos = new Vector3(17.0f, 1.0f, 300 + this.transform.position.z);
		yield return new WaitForSeconds (houseStartWait);
		while (true) {
			yield return new WaitForSeconds(Random.Range (0.5f, 5.0f));
			for (int i=0; i<=buildCount; i++) {
				Random.seed = i;
				if (Random.value < 0.5)
					Instantiate (housePrefab, rightPos, Quaternion.Euler (270.0f, 0.0f, 0.0f));
				if (Random.value > 0.5)
					Instantiate (housePrefab, leftPos, Quaternion.Euler (270.0f, 0.0f, 180.0f));
				yield return new WaitForSeconds(Random.Range (1,3));
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
			yield return new WaitForSeconds(Random.Range (0.0f, 4.0f));
			for (int i=0; i<=civilCount; i++) {
				Vector3 position = new Vector3
					(
						Mathf.Pow(-1, i)*6, 
						1.0f, 
						300 + this.transform.position.z
						);
				Instantiate (civilPrefab, position, Quaternion.identity);
				yield return new WaitForSeconds(Random.Range (0.5f, 1.5f));
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
