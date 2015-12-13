using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Player Boundaries
[System.Serializable] 
public class Boundary 
{ public float xMin, xMax, zMin, zMax; }

public class PlayerLogic : MonoBehaviour {
	public Boundary boundary;
	public float speed, rollSpeed, maxSpeed;
	
	private Rigidbody myBody;

	public float rage;
	private int numPpl;


	public GameObject cloudPlane;
	public GameObject gameMaster;
	public Camera mainCam;
	public bool autoInit = false;

	// parameters
	public float cloudSwitchDelay = 0.25f;
	public float axisSwitchDelay = 0.5f;
	public float xCloudSpawnRange = 0.8f;
	public float yCloudSpawnRange = 0.3f;
	public float greebleSpawnRate = 0.5f;
	public float xPositionThreshold = 2.0f;


	// Sprites
	public Sprite baseCloud1;
	public Sprite baseCloud2;
	public List<GameObject> cloudGreebles = new List<GameObject>();


	// private vars
	private GameController gameController;
	private Sprite baseSprite;
	private short currentAxis;

	void SwitchBaseSprite () {
		if (gameController.playerState == 1) {
			if (cloudPlane.GetComponent<SpriteRenderer>().sprite == baseCloud1) {
				cloudPlane.GetComponent<SpriteRenderer>().sprite = baseCloud2;
			} else {
				cloudPlane.GetComponent<SpriteRenderer>().sprite = baseCloud1;
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
		if (chance <= 0.66f) {
			GameObject greeble = Instantiate(cloudGreebles[Random.Range(0,cloudGreebles.Count-1)]);
			greeble.transform.parent = this.gameObject.transform;
			greeble.transform.localPosition = new Vector3 (Random.Range (-xCloudSpawnRange,xCloudSpawnRange),Random.Range(-yCloudSpawnRange, yCloudSpawnRange), -0.1f);
			greeble.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f,0.0f,Random.Range(-100f, 100f)));
			greeble.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.3f,0.3f),Random.Range(2.0f, 10.0f),0.0f));
			StartCoroutine(Fade (greeble,1.5f,0.0f));
			Destroy(greeble,1.5f);
		}
	}


	void AnimateOnStart() {
		InvokeRepeating("SwitchBaseSprite",cloudSwitchDelay,cloudSwitchDelay);
		InvokeRepeating("SwitchAxis", axisSwitchDelay, axisSwitchDelay);
		InvokeRepeating("SpawnCloudGreebles", 0.0f, greebleSpawnRate);
	}

	void AnimateOnUpdate() {
		Vector3 newScale = cloudPlane.transform.localScale;
		float pingPong = Mathf.PingPong(Time.time / 2, 0.2f) + 0.4f;
		if (currentAxis == 0) {
			newScale.x = pingPong;
		} else {
			newScale.y = pingPong;
		}
		cloudPlane.transform.localScale = newScale;
	}

	void Awake () {
		// Initialize as default camera?
		if (autoInit == true) {
			mainCam = Camera.main;
		}

	}

	public void InvolveInFight() {
		numPpl++;
		gameMaster.GetComponent<GameController>().AddScore();
	}

	// Use this for initialization
	void Start () {
		// init variables
		myBody = GetComponent<Rigidbody> ();
		cloudPlane = transform.Find("CloudPlane").gameObject;
		gameController = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
		baseSprite = cloudPlane.GetComponent<SpriteRenderer>().sprite;
		currentAxis = 0;
		// set children to parents

		// Run funcs
		AnimateOnStart ();
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		myBody.velocity = movement * speed;
		//myBody.AddForce (movement * speed);
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
		// Rotate to camera orientation
		cloudPlane.transform.rotation = Quaternion.identity;
		cloudPlane.transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
		AnimateOnUpdate();
		// clamp bottom to top of ground`
	}
}
