using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLogic : MonoBehaviour {

	public GameObject cloudPlane;
	public GameObject gameMaster;
	public Camera mainCam;
	public bool autoInit = false;

	// parameters
	public float cloudSwitchDelay = 0.25f;
	public float axisSwitchDelay = 0.5f;
	public float xCloudSpawnRange = 1.0f;
	public float yCloudSpawnRange = 1.0f;
	public float greebleSpawnRate = 1.5f;


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

	void SpawnCloudGreebles () {
		float chance = Random.value;
		if (chance <= 0.66f) {
			if (cloudGreebles.Count <= 4) {
				GameObject greeble = Instantiate(cloudGreebles[Random.Range(0,cloudGreebles.Count-1)]) ;
				greeble.transform.parent = this.gameObject.transform;
				greeble.transform.localPosition = new Vector3 (Random.Range (-xCloudSpawnRange,xCloudSpawnRange),Random.Range(-yCloudSpawnRange, yCloudSpawnRange), -0.1f);
				Destroy(greeble,3.0f);
			}
		}
	}


	void AnimateOnStart() {
		InvokeRepeating("SwitchBaseSprite",cloudSwitchDelay,cloudSwitchDelay);
		InvokeRepeating("SwitchAxis", axisSwitchDelay, axisSwitchDelay);
		InvokeRepeating("SpawnCloudGreebles", greebleSpawnRate, greebleSpawnRate);
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

	// Use this for initialization
	void Start () {
		// init variables
		cloudPlane = transform.Find("CloudPlane").gameObject;
		gameController = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
		baseSprite = cloudPlane.GetComponent<SpriteRenderer>().sprite;
		currentAxis = 0;
		// set children to parents
		foreach (GameObject g in cloudGreebles) {
			// CODE HERE
		}

		// Run funcs
		AnimateOnStart ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		// Rotate to camera orientation
		cloudPlane.transform.rotation = Quaternion.identity;
		cloudPlane.transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
		AnimateOnUpdate();
		// clamp bottom to top of ground

	}
}
