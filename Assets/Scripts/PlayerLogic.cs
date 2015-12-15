using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Player Boundaries
[System.Serializable] 
public class Boundary 
{ public float xMin, xMax, zMin, zMax; }

public class PlayerLogic : MonoBehaviour {
	public GameObject frontCloudPlane;
	public Camera mainCam;
	public bool autoInit = false;

	// parameters
	public Boundary boundary;
	public float rageLimit;
	public float speed, rollSpeed, maxSpeed;
	public float cloudSwitchDelay = 0.25f;
	public float axisSwitchDelay = 0.5f;
	public float xCloudSpawnRange = 0.8f;
	public float yCloudSpawnRange = 0.3f;
	public float greebleSpawnRate = 0.5f;
	public float xPositionThreshold = 2.0f;


	// Sprites
	public Sprite sprite0;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite[] baseClouds;

	public List<GameObject> cloudGreebles = new List<GameObject>();
	private Image healthUI;


	// private vars
	private Rigidbody myBody;
	private GameController gameController;
	private Sprite baseSprite;
	private Renderer rageBar;
	private short currentAxis;
	private float rage;
	private int health;

	void SwitchBaseSprite () {
		if (gameController.playerState == 1) {
			if (frontCloudPlane.GetComponent<SpriteRenderer>().sprite == baseClouds[0]) {
				frontCloudPlane.GetComponent<SpriteRenderer>().sprite = baseClouds[1];
			} else {
				frontCloudPlane.GetComponent<SpriteRenderer>().sprite = baseClouds[0];
			}
		}
	}

	void SwitchAxis () {
		if (gameController.playerState == 1) {
			if (currentAxis == 0) {
				currentAxis = 1;
			} else {
				currentAxis = 0;
			}
		}
	}

	IEnumerator Fade (GameObject go, float time, float alpha) {
		float a = go.GetComponent<Renderer>().material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) {
			Color c = new Color(1,1,1, Mathf.Lerp(a,alpha,t));
			go.GetComponent<Renderer>().material.color = c;
			yield return null;
		}
	}

	void SpawnCloudGreebles () {
		float chance = Random.value;
		if (chance <= 0.70f) {
			GameObject greeble = Instantiate(cloudGreebles[Random.Range(0,cloudGreebles.Count-1)]);
			greeble.transform.parent = this.gameObject.transform;
			greeble.transform.localPosition = new Vector3 (Random.Range (-xCloudSpawnRange,xCloudSpawnRange),Random.Range(-yCloudSpawnRange, yCloudSpawnRange), -0.1f);
			greeble.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f,0.0f,Random.Range(-20f, 20f)));
			greeble.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.3f,0.3f),Random.Range(0.2f, 2.0f),0.0f));
			StartCoroutine(Fade (greeble,1.0f,0.0f));
			Destroy(greeble,1.5f);
		}
	}


	void AnimateOnStart() {
		InvokeRepeating("SwitchBaseSprite",cloudSwitchDelay,cloudSwitchDelay);
		InvokeRepeating("SwitchAxis", axisSwitchDelay, axisSwitchDelay);
		InvokeRepeating("SpawnCloudGreebles", 0.0f, greebleSpawnRate);
	}

	void AnimateOnUpdate() {
		Vector3 newFrontScale = frontCloudPlane.transform.localScale;
		float pingPong = Mathf.PingPong(Time.time / 2, 0.2f) + 0.4f;
		if (currentAxis == 0) {
			newFrontScale.x = pingPong;
		} else {
			newFrontScale.y = pingPong;
		}
		frontCloudPlane.transform.localScale = newFrontScale;
	}

	void Awake () {
		// Initialize as default camera?
		if (autoInit == true) {
			mainCam = Camera.main;
		}

	}

	public void TakeDamage() {
		health = health - 1;
		if (health == 2) {
			healthUI.sprite = sprite1;
		} else if (health == 1) {
			healthUI.sprite = sprite2;
		} else if (health == 0) {
			healthUI.sprite = sprite3;
			gameController.GameOver();
		}
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Civillian") {
			InvolveInFight ();
			Destroy (coll.gameObject);
		} else if (coll.gameObject.tag == "Vehicle") {
			rage = Mathf.Max (0, rage - 1);
			TakeDamage (); 
		}
	}

	public void InvolveInFight() {
		rage++;
		gameController.AddScore();
	}

	// Use this for initialization
	void Start () {
		// init variables
		rage = 0.5f;
		health = 3;
		myBody = GetComponent<Rigidbody> ();
		rageBar = GameObject.FindGameObjectWithTag ("Rage").GetComponent<Renderer> ();
		frontCloudPlane = transform.Find("frontCloudPlane").gameObject;
		gameController = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
		healthUI = GameObject.FindGameObjectWithTag ("HealthUI").GetComponent<Image> ();
		currentAxis = 0;
		healthUI.sprite = sprite0;
		// set children to parents

		// Run funcs
		AnimateOnStart ();
	}

	void Update() {
		//Increase player size as a function of rage
		gameObject.transform.localScale = new Vector3 
			(
				1.0f, 
				1.0f,
				1.0f
			) *(0.5f + (rage/rageLimit)*4);
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3
			(
				moveHorizontal*speed,
				0.0f,
				0.001f * rollSpeed
			);
		myBody.velocity = movement;
		//myBody.AddForce (movement * (rage/rageLimit));
		if(myBody.velocity.magnitude > maxSpeed) {
			myBody.velocity = myBody.velocity.normalized * maxSpeed;
		}
		
		
		GetComponent<Rigidbody> ().position = new Vector3
			(
				Mathf.Clamp (myBody.position.x, boundary.xMin, boundary.xMax), 
				myBody.position.y,
				Mathf.Clamp (myBody.position.z, boundary.zMin, boundary.zMax)
			);
	}

	void LateUpdate () {
		// Adjust rage bar
		float width = rageBar.material.GetFloat ("_Width");
		rageBar.material.SetFloat ("_Width", rage / rageLimit);
		width = rageBar.material.GetFloat ("_Width");

		// Rotate to camera orientation
		frontCloudPlane.transform.rotation = Quaternion.identity;
		frontCloudPlane.transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
		AnimateOnUpdate();
		// clamp bottom to top of ground`
	}
}
